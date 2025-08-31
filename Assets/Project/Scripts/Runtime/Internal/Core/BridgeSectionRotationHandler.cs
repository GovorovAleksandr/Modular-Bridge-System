using System.Collections.Generic;
using GovorovAleksandr.BridgeBuilding.Contracts;

namespace GovorovAleksandr.BridgeBuilding.Internal
{
	internal sealed class BridgeSectionRotationHandler : IInitializable
	{
		private readonly IServiceLocator _serviceLocator;
		
		private IBridgeSectionRotationPolicy _sectionRotationProvider;

		public BridgeSectionRotationHandler(IServiceLocator serviceLocator)
		{
			_serviceLocator = serviceLocator;
		}

		public void Initialize()
		{
			_sectionRotationProvider = _serviceLocator.Resolve<IBridgeSectionRotationPolicy>();
		}

		public void RotateAll(IBridgeSection start, IEnumerable<IBridgeSection> middleSections, IBridgeSection end)
		{
			var startPosition = start.Position;
			var endPosition = end.Position;
			
			var direction = (endPosition - startPosition).normalized;
			
			var rotation = _sectionRotationProvider.Compute(direction);
			
			var allSections = new List<IBridgeSection>();
			allSections.Add(start);
			allSections.AddRange(middleSections ?? new List<IBridgeSection>());
			allSections.Add(end);
			
			foreach (var section in allSections)
			{
				section.SetRotation(rotation);
			}
		}
	}
}