using System;
using System.Threading;
using System.Threading.Tasks;
using GovorovAleksandr.BridgeBuilding.Contracts;
using UnityEngine;

namespace GovorovAleksandr.BridgeBuilding.Internal
{
	internal sealed class BridgeSectionPlacementAwaiter : IInitializable
	{
		private readonly IServiceLocator _serviceLocator;
		
		private IFrameAwaiter _frameAwaiter;
		private IBridgeSectionPositionProvider _positionProvider;

		public BridgeSectionPlacementAwaiter(IServiceLocator serviceLocator)
		{
			_serviceLocator = serviceLocator;
		}

		public void Initialize()
		{
			_frameAwaiter = _serviceLocator.Resolve<IFrameAwaiter>();
			_positionProvider = _serviceLocator.Resolve<IBridgeSectionPositionProvider>();
		}
		
		public async Task RunAsync(Func<Vector3, bool> condition, CancellationToken token)
		{
			while (true)
			{
				if (token.IsCancellationRequested) return;
				
				var position = _positionProvider.GetPosition();
				
				if (position != null && condition(position.Value)) break;
				
				await _frameAwaiter.WaitNextFrame();
			}

			await _frameAwaiter.WaitNextFrame();
		}
	}
}