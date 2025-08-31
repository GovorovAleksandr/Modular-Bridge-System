using System;
using GovorovAleksandr.BridgeBuilding.Contracts;

namespace GovorovAleksandr.BridgeBuilding.UnityAdapters
{
	public static class UnityBridgeSectionConverter
	{
		public static UnityBridgeSection CastToUnitySection(IBridgeSection section)
		{
			if (section is not UnityBridgeSection unitySection)
				throw new InvalidCastException(
					"UnityBridgeSectionFeedback works only with UnityBridgeSection implementation of IBridgeSection.");
			
			return unitySection;
		}
	}
}