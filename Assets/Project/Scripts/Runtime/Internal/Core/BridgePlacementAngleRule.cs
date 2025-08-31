using GovorovAleksandr.BridgeBuilding.Contracts;
using UnityEngine;

namespace GovorovAleksandr.BridgeBuilding.Internal
{
	internal sealed class BridgePlacementAngleRule : IBridgePlacementRule, IInitializable
	{
		private readonly IServiceLocator _serviceLocator;
		
		private IBridgeConstraintsConfig _constraintsConfig;

		public BridgePlacementAngleRule(IServiceLocator serviceLocator)
		{
			_serviceLocator = serviceLocator;
		}

		public void Initialize()
		{
			_constraintsConfig = _serviceLocator.Resolve<IBridgeConstraintsConfig>();
		}

		public bool IsValid(Vector3 startPosition, Vector3 endPosition)
		{
			var delta = endPosition - startPosition;
			var horizontalDistance = new Vector2(delta.x, delta.z).magnitude;
			var verticalDistance = delta.y;
			var angle = Mathf.Atan2(verticalDistance, horizontalDistance) * Mathf.Rad2Deg;
			return Mathf.Abs(angle) <= _constraintsConfig.MaxSlopeAngle;
		}
	}
}