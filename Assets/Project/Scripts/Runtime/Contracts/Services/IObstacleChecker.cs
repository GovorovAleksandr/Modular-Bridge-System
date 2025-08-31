using UnityEngine;

namespace GovorovAleksandr.BridgeBuilding.Contracts
{
	public interface IObstacleChecker
	{
		Vector3 GetSafePosition(Vector3 start, Vector3 target);
	}
}