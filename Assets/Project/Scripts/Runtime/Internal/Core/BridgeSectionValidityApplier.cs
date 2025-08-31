using System.Collections.Generic;
using GovorovAleksandr.BridgeBuilding.Contracts;

namespace GovorovAleksandr.BridgeBuilding.Internal
{
	internal sealed class BridgeSectionValidityApplier : IInitializable
	{
		private readonly IServiceLocator _serviceLocator;

		private IBridgeSectionValidityIndicator _validityIndicator;

		public BridgeSectionValidityApplier(IServiceLocator serviceLocator)
		{
			_serviceLocator = serviceLocator;
		}

		public void Initialize()
		{
			_validityIndicator = _serviceLocator.Resolve<IBridgeSectionValidityIndicator>();
		}

		public void ApplyValidity(IBridgeSection start, IEnumerable<IBridgeSection> middles, IBridgeSection end, bool isValid)
		{
			ApplyValidity(start, isValid);
			foreach (var middle in middles) ApplyValidity(middle, isValid);
			ApplyValidity(end, isValid);
		}

		public void ApplyValidity(IBridgeSection section, bool isValid)
		{
			if (isValid) _validityIndicator.MarkValid(section);
			else _validityIndicator.MarkInvalid(section);
		}
	}
}