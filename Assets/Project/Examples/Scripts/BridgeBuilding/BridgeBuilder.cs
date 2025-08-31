using System.Threading;
using GovorovAleksandr.BridgeBuilding.Api;
using GovorovAleksandr.BridgeBuilding.Contracts;
using GovorovAleksandr.BridgeBuilding.UnityAdapters;
using UnityEngine;

namespace GovorovAleksandr.ModularBridgeSystem.Examples
{
	internal sealed class BridgeBuilder : IBridgeBuilder
	{
		private readonly UnityBridgeConfigSo _unityBridgeConfig;
		private readonly MonoBehaviour _coroutineContext;
		private readonly IBridgeBuildingInput _input;

		private IBridgeConstructionContext _bridgeConstructionContext;
		private CancellationTokenSource _buildingCancellationTokenSource = new();

		public BridgeBuilder(UnityBridgeConfigSo unityBridgeConfig, MonoBehaviour coroutineContext, IBridgeBuildingInput input)
		{
			_unityBridgeConfig = unityBridgeConfig;
			_coroutineContext = coroutineContext;
			_input = input;
		}
		
		public void Initialize()
		{
			var servicesRegistrar = new ServiceRegistrar(_unityBridgeConfig, _coroutineContext);
			
			_bridgeConstructionContext = BridgeConstructionHost.Create(servicesRegistrar);
			
			_bridgeConstructionContext.Initialize();

			_input.BuildBridgeButtonPressed += BuildBridge;
			_input.CancelBuildingButtonPressed += CancelBuilding;
		}

		public void Dispose()
		{
			_buildingCancellationTokenSource?.Cancel();
			_buildingCancellationTokenSource?.Dispose();
			
			_bridgeConstructionContext.Dispose();
			
			_input.BuildBridgeButtonPressed -= BuildBridge;
			_input.CancelBuildingButtonPressed -= CancelBuilding;
		}

		private async void BuildBridge()
		{
			var builder = _bridgeConstructionContext.Builder;
			
			await builder.CreateAsync(_buildingCancellationTokenSource.Token);
		}

		private void CancelBuilding()
		{
			_buildingCancellationTokenSource?.Cancel();
			_buildingCancellationTokenSource = new();
		}
	}
}