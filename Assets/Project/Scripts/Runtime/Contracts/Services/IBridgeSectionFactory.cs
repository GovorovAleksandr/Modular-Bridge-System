using System;
using System.Threading;
using System.Threading.Tasks;

namespace GovorovAleksandr.BridgeBuilding.Contracts
{
	public interface IBridgeSectionFactory
	{
		IBridgeSection Instantiate(BridgeSectionType sectionType);
		Task<IBridgeSection[]> InstantiateBatch(BridgeSectionType sectionType, int count, Action<IBridgeSection> handler = null, CancellationToken cancellationToken = default);
	}
}