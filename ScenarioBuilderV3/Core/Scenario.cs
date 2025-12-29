using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using ScenarioBuilder.Core.Interfaces;
using System.Reflection;

namespace ScenarioBuilder.Core
{
    public abstract class Scenario : IScenarioBuilder
    {
        private readonly ScenarioContext _context;
        private readonly List<ScenarioStep> _steps = new();
        private readonly HashSet<Type> _executedSteps = new();   // <-- NEW
        private ScenarioExecutionOptions _lastOptions = ScenarioExecutionOptions.Default;
        private readonly Mapper mapper;

        private IServiceProvider _scenarioProvider;

        protected Scenario()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ScenarioToContextProfile>();
            });

            mapper = new Mapper(config);

            _context = new ScenarioContext();
            _scenarioProvider = new ServiceCollection().BuildServiceProvider();
        }

        /// <summary>
        /// Add a step to the scenario pipeline.
        /// </summary>
        public IScenarioBuilder AddStep<TEvent>()
    where TEvent : IScenarioEvent
        {
            var stepType = typeof(TEvent);

            // Prevent duplicates
            if (_steps.Any(s => s.StepId == stepType))
                return this;

            _steps.Add(new ScenarioStep(stepType, () => _scenarioProvider.GetRequiredService<TEvent>()));
            return this;
        }

        /// <summary>
        /// Builder-driven execution (fluent configuration).
        /// </summary>
        public async Task<Scenario> BuildAsync<TBuilder>(
            Func<TBuilder, TBuilder>? configure = null,
            CancellationToken ct = default)
            where TBuilder : class, IScenarioOptionsBuilder, new()
        {
            var builder = new TBuilder();

            var configured = configure?.Invoke(builder) ?? builder;

            _scenarioProvider = configured.Services;

            var attributeBuilder = new AttributeScenarioBuilder(this);
            attributeBuilder.Build(this);

            MapBuilderToContext(configured);

            await RunAsync(configured.Options, ct);

            return this;
        }

        /// <summary>
        /// Execute the scenario with optional execution options.
        /// </summary>
        public async Task<ScenarioContext> RunAsync(
        ScenarioExecutionOptions? options = null,
        CancellationToken ct = default)
        {
            mapper.Map(this, _context);

            if (options != null)
            {
                _lastOptions = _lastOptions.MergeWith(options);
            }

            foreach (var step in _steps)
            {
                if (_lastOptions.ShouldStopBefore(step.StepId))
                    break;

                if (_executedSteps.Contains(step.StepId))
                    continue;

                if (_lastOptions.TryGetOverride(step.StepId, out var overrideEvent))
                {
                    await overrideEvent.ExecuteAsync(_context, ct);
                }
                else
                {
                    await step.Factory().ExecuteAsync(_context, ct);
                }

                _executedSteps.Add(step.StepId);
            }

            return _context;
        }

        public ScenarioContext GetContext()
        {
            return _context;
        }

        private void MapBuilderToContext(object builder)
        {
            var properties = builder.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.CanRead && p.GetIndexParameters().Length == 0);

            foreach (var prop in properties)
            {
                var value = prop.GetValue(builder);

                if (value != null)
                {
                    _context.Set(prop.Name, value);
                }
            }
        }

        private sealed record ScenarioStep(Type StepId, Func<IScenarioEvent> Factory);
    }
}