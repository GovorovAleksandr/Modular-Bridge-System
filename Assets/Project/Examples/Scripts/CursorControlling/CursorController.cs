using UnityEngine;

namespace GovorovAleksandr.ModularBridgeSystem.Examples
{
	internal sealed class CursorController : IInitializable
	{
		public void Initialize()
		{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
	}
}