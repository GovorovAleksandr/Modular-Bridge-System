using System.Collections.Generic;
using GovorovAleksandr.BridgeBuilding.Contracts;

namespace GovorovAleksandr.BridgeBuilding.Internal
{
	internal sealed class BridgeSectionPlacementDispatcher : IInitializable
	{
		private readonly IServiceLocator _serviceLocator;
		private readonly List<IBridgeSectionPlacedListener> _placedListener = new();

		public BridgeSectionPlacementDispatcher(IServiceLocator serviceLocator)
		{
			_serviceLocator = serviceLocator;
		}

		public void Initialize()
		{
			_placedListener.Clear();
			_placedListener.AddRange(_serviceLocator.ResolveMany<IBridgeSectionPlacedListener>());
		}

		public void Dispatch(IBridgeSection start, IEnumerable<IBridgeSection> middles, IBridgeSection end)
		{
			HandlePlaced(start);
			foreach (var middle in middles) HandlePlaced(middle);
			HandlePlaced(end);
		}

		private void HandlePlaced(IBridgeSection section)
		{
			foreach (var listener in _placedListener)
			{
				listener.HandlePlaced(section);
			}
		}
	}
}