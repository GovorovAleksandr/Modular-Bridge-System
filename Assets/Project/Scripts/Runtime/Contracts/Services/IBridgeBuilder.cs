using System.Threading;
using System.Threading.Tasks;

namespace GovorovAleksandr.BridgeBuilding.Contracts
{
	public interface IBridgeBuilder
	{
		Task CreateAsync(CancellationToken cancellationToken);
	}
}