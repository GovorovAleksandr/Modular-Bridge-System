using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GovorovAleksandr.ModularBridgeSystem.Examples
{
	internal sealed class CameraInput : ICameraInputLifecycle
	{
		public event Action<Vector3> Moved;
		public event Action<Vector2> Rotated;

		private readonly CameraControls _controls = new();
		
		private Vector3 _movementDirection;
		private Vector3 _lastMovementDirection;
		
		private Vector2 _rotateDirection;
		private Vector2 _lastRotateDirection;
		private Vector2 _rotationVelocity;
		
		public void Initialize() => _controls.Enable();
		
		public void Update()
		{
			ListenMoveInput();

			if (_movementDirection == Vector3.zero && _lastMovementDirection == Vector3.zero) return;
			Moved?.Invoke(_movementDirection);
		}

		public void LateUpdate()
		{
			ListenRotateInput();
			
			if (_rotateDirection == Vector2.zero && _lastRotateDirection == Vector2.zero) return;
			Rotated?.Invoke(_rotateDirection);
		}

		public void Dispose() => _controls.Dispose();

		private void ListenMoveInput()
		{
			var rawInput = _controls.Camera.Move.ReadValue<Vector3>();
			
			var normalizedX = rawInput.x;
			var normalizedY = rawInput.y;
			var normalizedZ = rawInput.z;
			
			_lastMovementDirection = _movementDirection;
			_movementDirection = new Vector3(normalizedX, normalizedY, normalizedZ);
		}
		
		private void ListenRotateInput()
		{
			var rawInput = Mouse.current.delta.ReadValue();
			
			var normalizedX = rawInput.x;
			var normalizedY = rawInput.y;

			_lastRotateDirection = _rotateDirection;
			_rotateDirection = new Vector3(normalizedX, normalizedY);
		}
	}
}