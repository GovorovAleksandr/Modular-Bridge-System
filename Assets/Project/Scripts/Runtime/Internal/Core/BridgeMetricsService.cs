using System.Collections.Generic;
using GovorovAleksandr.BridgeBuilding.Contracts;

namespace GovorovAleksandr.BridgeBuilding.Internal
{
	internal sealed class BridgeMetricsService : IInitializable
	{
		private readonly Dictionary<BridgeSectionType, float> _sectionLengthMap = new();
		
		private readonly IServiceLocator _serviceLocator;
		
		private IBridgeSectionDescriptorProvider _sectionDescriptorProvider;

		public BridgeMetricsService(IServiceLocator serviceLocator)
		{
			_serviceLocator = serviceLocator;
		}

		public void Initialize()
		{
			_sectionDescriptorProvider = _serviceLocator.Resolve<IBridgeSectionDescriptorProvider>();
		}
		
		public float GetLength(BridgeSectionType sectionType)
		{
			if (_sectionLengthMap.TryGetValue(sectionType, out var sectionLength)) return sectionLength;

			_sectionLengthMap[sectionType] = sectionLength = _sectionDescriptorProvider.GetLength(sectionType);

			return sectionLength;
		}
	}
}