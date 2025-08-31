using System.Threading.Tasks;

namespace GovorovAleksandr.BridgeBuilding.Contracts
{
	public interface IFrameAwaiter
	{
		Task WaitNextFrame();
	}
}