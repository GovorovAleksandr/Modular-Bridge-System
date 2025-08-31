using System;
using UnityEngine.InputSystem;

namespace GovorovAleksandr.ModularBridgeSystem.Examples
{
	internal sealed class BridgeBuildingInput : IBridgeBuildingInputLifecycle
	{
		private readonly BridgeBuildingControls _controls = new();
		
		public event Action BuildBridgeButtonPressed;
		public event Action CancelBuildingButtonPressed;

		public void Initialize()
		{
			_controls.Enable();

			_controls.BridgeBuilding.Build.performed += HandleBuildBridge;
			_controls.BridgeBuilding.Cancel.performed += HandleCancelBuilding;
		}
		
		public void Dispose()
		{
			_controls.BridgeBuilding.Build.performed -= HandleBuildBridge;
			_controls.BridgeBuilding.Cancel.performed -= HandleCancelBuilding;
			
			_controls.Disable();
			_controls.Dispose();
		}

		private void HandleBuildBridge(InputAction.CallbackContext obj) => BuildBridgeButtonPressed?.Invoke();
		private void HandleCancelBuilding(InputAction.CallbackContext obj) => CancelBuildingButtonPressed?.Invoke();
	}
}