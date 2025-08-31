using GovorovAleksandr.BridgeBuilding.Contracts;

namespace GovorovAleksandr.BridgeBuilding.UnityAdapters
{
	public sealed class UnityBridgeSectionPlacementRule : IBridgeSectionPlacementRule, IInitializable
	{
		private readonly IServiceLocator _serviceLocator;
		
		private InputProvider _inputProvider;

		public UnityBridgeSectionPlacementRule(IServiceLocator serviceLocator)
		{
			_serviceLocator = serviceLocator;
		}

		public bool IsValid => _inputProvider.WasMousePressedThisFrame;
		
		public void Initialize()
		{
			_inputProvider = _serviceLocator.Resolve<InputProvider>();
		}
	}
}