using System.Collections.Generic;
using GovorovAleksandr.BridgeBuilding.Contracts;

namespace GovorovAleksandr.BridgeBuilding.UnityAdapters
{
	public sealed class UnityBridgeSectionDescriptorProvider : IBridgeSectionDescriptorProvider
	{
		private readonly IReadOnlyDictionary<BridgeSectionType, UnityBridgeSection> _prefabs;
		
		private readonly Dictionary<BridgeSectionType, float> _lengthMap = new ();
		
		public UnityBridgeSectionDescriptorProvider(IReadOnlyDictionary<BridgeSectionType, UnityBridgeSection> prefabs)
		{
			_prefabs = prefabs;
		}

		public float GetLength(BridgeSectionType type)
		{
			if (_lengthMap.TryGetValue(type, out var length)) return length;
			
			length = _lengthMap[type] = _prefabs[type].Length;
			
			return length;
		}
	}
}