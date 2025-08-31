using GovorovAleksandr.BridgeBuilding.Contracts;
using UnityEngine;

namespace GovorovAleksandr.BridgeBuilding.UnityAdapters
{
	public sealed class UnityGroundHeightProvider : IGroundHeightProvider
	{
		private readonly LayerMask _groundMask;
		private readonly float _maxDistance;
		private readonly float _rayStartHeight;

		public UnityGroundHeightProvider(LayerMask groundMask, float maxDistance, float rayStartHeight)
		{
			_groundMask = groundMask;
			_maxDistance = maxDistance;
			_rayStartHeight = rayStartHeight;
		}

		public float? GetGroundHeight(Vector3 position)
		{
			var ray = new Ray(new Vector3(position.x, _rayStartHeight, position.z), Vector3.down);
			return Physics.Raycast(ray, out var hit, _maxDistance, _groundMask) ? hit.point.y : null;
		}
	}
}