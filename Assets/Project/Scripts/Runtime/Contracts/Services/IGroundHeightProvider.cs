using UnityEngine;

namespace GovorovAleksandr.BridgeBuilding.Contracts
{
	public interface IGroundHeightProvider
	{
		float? GetGroundHeight(Vector3 position);
	}
}