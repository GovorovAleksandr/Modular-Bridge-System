using UnityEngine;

namespace GovorovAleksandr.BridgeBuilding.Contracts
{
	public interface IBridgeSectionRotationPolicy
	{
		Quaternion Compute(Vector3 direction);
	}
}