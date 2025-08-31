using UnityEngine;

namespace GovorovAleksandr.BridgeBuilding.Contracts
{
	public interface IBridgeSection
	{
		BridgeSectionType Type { get; }
		Vector3 Position { get; }
		void Enable();
		void Disable();
		void SetPosition(Vector3 position);
		void SetRotation(Quaternion rotation);
	}
}
