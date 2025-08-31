using GovorovAleksandr.BridgeBuilding.Contracts;
using UnityEngine;

namespace GovorovAleksandr.BridgeBuilding.UnityAdapters
{
	public sealed class UnityBridgeSectionValidityIndicator : IBridgeSectionValidityIndicator
	{
		private readonly Material _validMaterial;
		private readonly Material _invalidMaterial;

		public UnityBridgeSectionValidityIndicator(Material validMaterial, Material invalidMaterial)
		{
			_validMaterial = validMaterial;
			_invalidMaterial = invalidMaterial;
		}

		public void MarkValid(IBridgeSection section)
		{
			ApplyMaterial(UnityBridgeSectionConverter.CastToUnitySection(section), _validMaterial);
		}

		public void MarkInvalid(IBridgeSection section)
		{
			ApplyMaterial(UnityBridgeSectionConverter.CastToUnitySection(section), _invalidMaterial);
		}

		private static void ApplyMaterial(UnityBridgeSection section, Material material)
		{
			section.Renderer.sharedMaterial = material;
		}
	}
}