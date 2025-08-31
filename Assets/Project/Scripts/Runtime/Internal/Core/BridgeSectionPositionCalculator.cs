using GovorovAleksandr.BridgeBuilding.Contracts;
using UnityEngine;

namespace GovorovAleksandr.BridgeBuilding.Internal
{
	internal class BridgeSectionPositionCalculator : IInitializable
	{
		private readonly IServiceLocator _serviceLocator;

		private IBridgeSectionDescriptorProvider _sectionDescriptorProvider;

		public BridgeSectionPositionCalculator(IServiceLocator serviceLocator)
		{
			_serviceLocator = serviceLocator;
		}

		public void Initialize()
		{
			_sectionDescriptorProvider = _serviceLocator.Resolve<IBridgeSectionDescriptorProvider>();
		}

		public Vector3 CalculatePosition(IBridgeSection lastSection, IBridgeSection nextSection, Vector3 direction)
		{
			var lastPosition = lastSection.Position;
			var lastLength = _sectionDescriptorProvider.GetLength(lastSection.Type);
			var nextLength = _sectionDescriptorProvider.GetLength(nextSection.Type);
			
			var nextPosition = lastPosition + direction.normalized * (lastLength * 0.5f + nextLength * 0.5f);

			return nextPosition;
		}
	}
}