using GovorovAleksandr.BridgeBuilding.Contracts;

namespace GovorovAleksandr.BridgeBuilding.Internal
{
	internal sealed class ServiceRegistrar : IServiceRegistrar
	{
		public void RegisterServices(IServiceLocator serviceLocator)
		{
			serviceLocator.RegisterSingle(new PoolMaintainer(serviceLocator));
			serviceLocator.RegisterSingle(new BridgeSectionPool(serviceLocator));
			serviceLocator.RegisterSingle(new BridgeSectionPlacementAwaiter(serviceLocator));
			serviceLocator.RegisterSingle(new BridgeSectionRotationHandler(serviceLocator));
			serviceLocator.RegisterSingle(new MiddleSectionEnsurer(serviceLocator));
			serviceLocator.RegisterSingle(new BridgeMetricsService(serviceLocator));
			serviceLocator.RegisterSingle(new BridgeBuildOrchestrator(serviceLocator));
			serviceLocator.RegisterSingle(new BridgeSectionPlacementHandler(serviceLocator));
			serviceLocator.RegisterSingle(new BridgeSectionPositionCalculator(serviceLocator));
			serviceLocator.RegisterSingle(new AvailableSectionTypesProvider());
			serviceLocator.RegisterSingle(new MiddleSectionPlanner(serviceLocator));
			serviceLocator.RegisterSingle(new BridgeSectionValidityApplier(serviceLocator));
			serviceLocator.RegisterSingle(new BridgeSectionPlacementDispatcher(serviceLocator));
			
			serviceLocator.RegisterSingle(new BridgePlacementValidator(serviceLocator));
			serviceLocator.RegisterMany<IBridgePlacementRule>(new BridgePlacementAngleRule(serviceLocator));
			
			serviceLocator.RegisterSingle<IBridgeBuilder>(new BridgeBuilder(serviceLocator));
		}
	}
}