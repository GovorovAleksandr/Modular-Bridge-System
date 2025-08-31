using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GovorovAleksandr.BridgeBuilding.Contracts;

namespace GovorovAleksandr.BridgeBuilding.Internal
{
	internal sealed class BridgeBuildOrchestrator : IInitializable
	{
		private readonly IServiceLocator _serviceLocator;
		
		private MiddleSectionEnsurer _middleSectionEnsurer;
		private BridgeSectionPlacementAwaiter _placementAwaiter;
		private IBridgeSectionPlacementRule _placementRule;
		private BridgeSectionPlacementHandler _placementHandler;
		private BridgeSectionRotationHandler _rotationHandler;

		private BridgePlacementValidator _bridgePlacementValidator;
		private BridgeSectionValidityApplier _validityApplier;
		private BridgeSectionPlacementDispatcher _placementDispatcher;

		public BridgeBuildOrchestrator(IServiceLocator serviceLocator)
		{
			_serviceLocator = serviceLocator;
		}

		public void Initialize()
		{
			_middleSectionEnsurer = _serviceLocator.Resolve<MiddleSectionEnsurer>();
			_placementAwaiter = _serviceLocator.Resolve<BridgeSectionPlacementAwaiter>();
			_placementRule = _serviceLocator.Resolve<IBridgeSectionPlacementRule>();
			_placementHandler = _serviceLocator.Resolve<BridgeSectionPlacementHandler>();
			_rotationHandler = _serviceLocator.Resolve<BridgeSectionRotationHandler>();
			
			_bridgePlacementValidator = _serviceLocator.Resolve<BridgePlacementValidator>();
			_validityApplier = _serviceLocator.Resolve<BridgeSectionValidityApplier>();
			_placementDispatcher = _serviceLocator.Resolve<BridgeSectionPlacementDispatcher>();
		}

		public async Task InstallStartSection(IBridgeSection start, CancellationToken cancellationToken)
		{
			_validityApplier.ApplyValidity(start, true);
			await _placementAwaiter.RunAsync(selectedPosition =>
			{
				_placementHandler.Place(start, selectedPosition);
				return _placementRule.IsValid;
			}, cancellationToken);
		}
		
		public async Task InstallEndSection(IBridgeSection start, List<IBridgeSection> middles, IBridgeSection end, CancellationToken cancellationToken)
		{
			var startPosition = start.Position;
			
			await _placementAwaiter.RunAsync(selectedPosition =>
			{
				_middleSectionEnsurer.EnsureMiddleSections(startPosition, middles, selectedPosition);
				_placementHandler.PlaceMiddleSections(start, middles, selectedPosition);
				_placementHandler.PlaceEnd(start, middles, end, selectedPosition);
				_rotationHandler.RotateAll(start, middles, end);
				
				var endPosition = end.Position;

				var canPlaceBridge = _bridgePlacementValidator.IsValid(startPosition, endPosition);
				_validityApplier.ApplyValidity(start, middles, end, canPlaceBridge);
				
				return canPlaceBridge && _placementRule.IsValid;
			}, cancellationToken);
			
			_placementDispatcher.Dispatch(start, middles, end);
		}
	}
}