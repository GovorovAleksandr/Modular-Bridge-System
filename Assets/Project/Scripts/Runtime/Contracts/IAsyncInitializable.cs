using System.Threading.Tasks;

namespace GovorovAleksandr.BridgeBuilding.Contracts
{
	public interface IAsyncInitializable
	{
		Task InitializeAsync();
	}
}