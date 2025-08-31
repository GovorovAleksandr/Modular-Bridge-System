using GovorovAleksandr.BridgeBuilding.Contracts;
using GovorovAleksandr.BridgeBuilding.Internal;

namespace GovorovAleksandr.BridgeBuilding.Api
{
	internal static class BridgeConstructionFactory
	{
		public static IBridgeConstructionContext CreateContext(params IServiceRegistrar[] registrars)
		{
			var serviceLocator = new ServiceLocator();

			foreach (var registrar in registrars)
				registrar.RegisterServices(serviceLocator);
			
			var internalServiceRegistrar = new ServiceRegistrar();
			internalServiceRegistrar.RegisterServices(serviceLocator);

			return new BridgeConstructionContext(serviceLocator);
		}
	}
}