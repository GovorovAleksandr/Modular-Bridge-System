using System;
using System.Collections.Generic;
using System.Linq;
using GovorovAleksandr.BridgeBuilding.Contracts;

namespace GovorovAleksandr.BridgeBuilding.Internal
{
	internal class AvailableSectionTypesProvider
	{
		private List<BridgeSectionType> _types;
		
		public IEnumerable<BridgeSectionType> Get()
		{
			_types ??= Enum.GetValues(typeof(BridgeSectionType))
				.OfType<BridgeSectionType>()
				.Where(sectionType => sectionType != BridgeSectionType.None)
				.ToList();;
			
			return _types;
		}
	}
}