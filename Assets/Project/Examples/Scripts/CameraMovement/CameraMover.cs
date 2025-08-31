using UnityEngine;

namespace GovorovAleksandr.ModularBridgeSystem.Examples
{
    internal sealed class CameraMover : ICameraMover
    {
        private readonly CameraMoverSettings _settings;
        private readonly ICameraInput _input;

        private float _pitch;
        private float _yaw;
        
        private Transform Transform => _settings.CinemachineCamera.transform;
        
        public CameraMover(CameraMoverSettings settings, ICameraInput input)
        {
            _settings = settings;
            _input = input;
        }
        
        public void Initialize()
        {
            _input.Moved += HandleMovement;
            _input.Rotated += HandleRotation;
        }

        public void Dispose()
        {
            _input.Moved -= HandleMovement;
            _input.Rotated -= HandleRotation;
        }

        private void HandleMovement(Vector3 inputDirection)
        {
            var movementVectorX = inputDirection.x * Transform.right.x + inputDirection.z * Transform.forward.x;
            var movementVectorY = inputDirection.y;
            var movementVectorZ = inputDirection.x * Transform.right.z + inputDirection.z * Transform.forward.z;
            
            var movementVector = new Vector3(movementVectorX, movementVectorY, movementVectorZ);
            
            Transform.position = Vector3.Lerp(Transform.position, Transform.position + movementVector, _settings.MovementSpeed * Time.deltaTime);
        }
        
        private void HandleRotation(Vector2 rawRotateDirection)
        {
            _pitch -= rawRotateDirection.y * _settings.Sensitivity * Time.deltaTime;
            _yaw += rawRotateDirection.x * _settings.Sensitivity * Time.deltaTime;
            
            _pitch = Mathf.Clamp(_pitch, -_settings.VerticalAngleLimit, _settings.VerticalAngleLimit);
            
            Transform.localRotation = Quaternion.Euler(_pitch, _yaw, 0f);
        }
    }
}