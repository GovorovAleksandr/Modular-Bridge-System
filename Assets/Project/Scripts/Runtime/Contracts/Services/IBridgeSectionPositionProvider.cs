using UnityEngine;

namespace GovorovAleksandr.BridgeBuilding.Contracts
{
	public interface IBridgeSectionPositionProvider
	{
		Vector3? GetPosition();
	}
}