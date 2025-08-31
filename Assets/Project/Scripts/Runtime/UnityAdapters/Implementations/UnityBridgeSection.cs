using GovorovAleksandr.BridgeBuilding.Contracts;
using UnityEngine;

namespace GovorovAleksandr.BridgeBuilding.UnityAdapters
{
	public sealed class UnityBridgeSection : MonoBehaviour, IBridgeSection
	{
		[SerializeField] private BridgeSectionType _sectionType;
		[SerializeField] private MeshFilter _meshFilter;
		[SerializeField] private MeshRenderer _renderer;
		
		public BridgeSectionType Type => _sectionType;
		public Vector3 Position => transform.position;
		public float Length => _meshFilter.sharedMesh.bounds.size.x * transform.lossyScale.x;
		public MeshRenderer Renderer => _renderer;
		
		public void Enable() => gameObject.SetActive(true);
		public void Disable() => gameObject.SetActive(false);
		public void SetRotation(Quaternion rotation) => transform.localRotation = rotation;
		public void SetPosition(Vector3 position) => transform.position = position;
	}
}