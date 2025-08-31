using GovorovAleksandr.BridgeBuilding.Contracts;
using UnityEngine;

namespace GovorovAleksandr.BridgeBuilding.UnityAdapters
{
	public sealed class UnityBridgeSectionPositionProvider : IBridgeSectionPositionProvider, IInitializable
	{
		private readonly IServiceLocator _serviceLocator;
		
		private readonly LayerMask _groundLayerMask;
		private readonly float _maxPlaceDistance;
		
		private InputProvider _inputProvider;

		public UnityBridgeSectionPositionProvider(IServiceLocator serviceLocator,LayerMask groundLayerMask, float maxPlaceDistance)
		{
			_serviceLocator = serviceLocator;
			_groundLayerMask = groundLayerMask;
			_maxPlaceDistance = maxPlaceDistance;
		}
		
		public void Initialize()
		{
			_inputProvider = _serviceLocator.Resolve<InputProvider>();
		}

		public Vector3? GetPosition()
		{
			var ray = _inputProvider.GetMouseRay();
			
			if (ray == null) return null;
			
			return Physics.Raycast(ray.Value, out var hit, _maxPlaceDistance, _groundLayerMask)
				? hit.point
				: null;
		}
	}
}