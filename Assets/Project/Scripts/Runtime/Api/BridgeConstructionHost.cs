using GovorovAleksandr.BridgeBuilding.Contracts;

namespace GovorovAleksandr.BridgeBuilding.Api
{
	public static class BridgeConstructionHost
	{
		public static IBridgeConstructionContext Create(params IServiceRegistrar[] registrars)
		{
			return BridgeConstructionFactory.CreateContext(registrars);
		}
	}
}