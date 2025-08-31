using UnityEngine;

namespace GovorovAleksandr.BridgeBuilding.Contracts
{
	public interface IBridgePlacementRule
	{
		bool IsValid(Vector3 startPosition, Vector3 endPosition);
	}
}