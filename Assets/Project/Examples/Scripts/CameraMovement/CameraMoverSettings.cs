using System;
using Unity.Cinemachine;
using UnityEngine;

namespace GovorovAleksandr.ModularBridgeSystem.Examples
{
	[Serializable]
	internal sealed class CameraMoverSettings
	{
		[Header("Cinemachine")]
		[SerializeField] private CinemachineCamera _cinemachineCamera;
        
		[Header("Movement Settings")]
		[SerializeField] [Min(1f)] private float _movementSpeed = 10f;
        
		[Header("Rotation Settings")]
		[SerializeField] [Min(10f)] private float _sensitivity = 45f;
		[SerializeField] [Min(10f)] private float _verticalAngleLimit = 90f;
		
		public CinemachineCamera CinemachineCamera => _cinemachineCamera;
		public float MovementSpeed => _movementSpeed;
		public float Sensitivity => _sensitivity;
		public float VerticalAngleLimit => _verticalAngleLimit;
	}
}