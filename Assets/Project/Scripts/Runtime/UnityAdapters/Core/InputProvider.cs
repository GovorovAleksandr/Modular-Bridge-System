using UnityEngine;
using UnityEngine.InputSystem;

namespace GovorovAleksandr.BridgeBuilding.UnityAdapters
{
	public class InputProvider
	{
		private Camera _camera;
		private Mouse _mouse;

		private Camera CameraInstance => _camera ??= Camera.main;
		private Mouse MouseInstance => _mouse ??= Mouse.current;

		public bool WasMousePressedThisFrame => MouseInstance?.leftButton.wasPressedThisFrame ?? false;
		public Vector2? MousePosition => MouseInstance?.position.value;
		
		public Ray? GetMouseRay()
		{
			var cam = CameraInstance;
			var mousePos = MousePosition;
			
			if (mousePos == null) return null;
			
			return cam.ScreenPointToRay(mousePos.Value);
		}
	}
}