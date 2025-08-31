using System;
using UnityEngine;

namespace GovorovAleksandr.ModularBridgeSystem.Examples
{
	internal interface ICameraInput
	{
		event Action<Vector3> Moved;
		event Action<Vector2> Rotated;
	}
}