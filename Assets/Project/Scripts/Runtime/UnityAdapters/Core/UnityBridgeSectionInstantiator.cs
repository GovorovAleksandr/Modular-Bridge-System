using System.Collections.Generic;
using GovorovAleksandr.BridgeBuilding.Contracts;
using UnityEngine;

namespace GovorovAleksandr.BridgeBuilding.UnityAdapters
{
	public sealed class UnityBridgeSectionInstantiator
	{
		private readonly Dictionary<BridgeSectionType, UnityBridgeSection> _prefabs;

		public UnityBridgeSectionInstantiator(IReadOnlyDictionary<BridgeSectionType, UnityBridgeSection> prefabs)
		{
			_prefabs = new Dictionary<BridgeSectionType, UnityBridgeSection>(prefabs);
		}
		
		public IBridgeSection Instantiate(BridgeSectionType sectionType)
		{
			var prefab = GetPrefabOrThrow(sectionType);
			return Instantiate(prefab);
		}
		
		private static IBridgeSection Instantiate(UnityBridgeSection prefab)
		{
			return Object.Instantiate(prefab);
		}
		
		private UnityBridgeSection GetPrefabOrThrow(BridgeSectionType sectionType)
		{
			return _prefabs.TryGetValue(sectionType, out var prefab)
				? prefab
				: throw new KeyNotFoundException($"Prefab for section type \"{sectionType}\" was not found.");
		}
	}
}