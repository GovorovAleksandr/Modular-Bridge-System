using System.Collections.Generic;

namespace GovorovAleksandr.BridgeBuilding.Contracts
{
	public interface IServiceLocator
	{
		void RegisterSingle<TService>(TService instance);
		void RegisterMany<TService>(TService instance);
		TService Resolve<TService>();
		IEnumerable<TService> ResolveMany<TService>();
		bool IsRegistered<TService>();
	}
}