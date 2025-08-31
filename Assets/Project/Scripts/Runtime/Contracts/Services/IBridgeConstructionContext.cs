using System;

namespace GovorovAleksandr.BridgeBuilding.Contracts
{
	public interface IBridgeConstructionContext : IInitializable, IDisposable
	{
		IBridgeBuilder Builder { get; }
	}
}