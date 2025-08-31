using GovorovAleksandr.BridgeBuilding.Contracts;
using UnityEngine;

namespace GovorovAleksandr.BridgeBuilding.UnityAdapters
{
	internal sealed class UnityBridgeSectionRotationPolicy : IBridgeSectionRotationPolicy
	{
		private readonly float _yRotationOffset;

		public UnityBridgeSectionRotationPolicy(float yRotationOffset)
		{
			_yRotationOffset = yRotationOffset;
		}

		public Quaternion Compute(Vector3 direction)
		{
			if (direction == Vector3.zero) return Quaternion.identity;
			
			return Quaternion.LookRotation(direction) * Quaternion.Euler(Vector3.up * _yRotationOffset);
		}
	}
}