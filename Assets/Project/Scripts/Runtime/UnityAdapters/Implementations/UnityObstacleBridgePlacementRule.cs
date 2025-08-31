using GovorovAleksandr.BridgeBuilding.Contracts;
using UnityEngine;

namespace GovorovAleksandr.BridgeBuilding.UnityAdapters
{
	public sealed class UnityObstacleBridgePlacementRule : IBridgePlacementRule
	{
		private readonly LayerMask _ignoreLayerMask;
		private readonly float _maxObstacleHeight;

		public UnityObstacleBridgePlacementRule(LayerMask ignoreLayerMask, float maxObstacleHeight)
		{
			_ignoreLayerMask = ignoreLayerMask;
			_maxObstacleHeight = maxObstacleHeight;
		}

		public bool IsValid(Vector3 startPosition, Vector3 endPosition)
		{
			var direction = (endPosition - startPosition).normalized;
			var distance = Vector3.Distance(startPosition, endPosition);
			
			return !Physics.Raycast(startPosition + Vector3.up * _maxObstacleHeight, direction, distance, _ignoreLayerMask);
		}
	}
}