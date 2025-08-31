using System.Collections.Generic;
using System.Linq;
using GovorovAleksandr.BridgeBuilding.Contracts;
using UnityEngine;

namespace GovorovAleksandr.BridgeBuilding.Internal
{
	internal class BridgeSectionPlacementHandler : IInitializable
	{
		private readonly IServiceLocator _serviceLocator;
		
		private BridgeSectionPositionCalculator _positionCalculator;

		public BridgeSectionPlacementHandler(IServiceLocator serviceLocator)
		{
			_serviceLocator = serviceLocator;
		}

		public void Initialize()
		{
			_positionCalculator = _serviceLocator.Resolve<BridgeSectionPositionCalculator>();
		}

		public void Place(IBridgeSection section, Vector3 targetPosition)
		{
			section.SetPosition(targetPosition);
		}

		public void PlaceMiddleSections(IBridgeSection start, IReadOnlyList<IBridgeSection> middleSections, Vector3 endPosition)
		{
			var direction = GetDirection(start, endPosition);
			
			for (var i = 0; i < middleSections.Count; i++)
			{
				var section = middleSections[i];
				var lastSection = i > 0 ? middleSections[i - 1] : start;
				
				var position = _positionCalculator.CalculatePosition(lastSection, section, direction);
				Place(section, position);
			}
		}

		public void PlaceEnd(IBridgeSection start, IReadOnlyList<IBridgeSection> middleSections, IBridgeSection end, Vector3 endPosition)
		{
			var lastSection = middleSections.LastOrDefault() ?? start;
			var direction = GetDirection(start, endPosition);
			var position = _positionCalculator.CalculatePosition(lastSection, end, direction);
			
			Place(end, position);
		}

		private static Vector3 GetDirection(IBridgeSection start, Vector3 endPosition)
		{
			var startPosition = start.Position;
			return endPosition - startPosition;
		}
	}
}