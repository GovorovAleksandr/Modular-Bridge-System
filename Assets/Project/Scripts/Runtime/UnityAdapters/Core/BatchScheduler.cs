using System;
using System.Threading;
using System.Threading.Tasks;
using GovorovAleksandr.BridgeBuilding.Contracts;

namespace GovorovAleksandr.BridgeBuilding.UnityAdapters
{
	public sealed class BatchScheduler : IPrioritizedInitializable
	{
		private readonly IServiceLocator _serviceLocator;
		
		private IFrameAwaiter _frameAwaiter;
		
		public BatchScheduler(IServiceLocator serviceLocator)
		{
			_serviceLocator = serviceLocator;
		}

		public int Priority => -125;

		public void Initialize()
		{
			_frameAwaiter = _serviceLocator.Resolve<IFrameAwaiter>();
		}

		public async Task Run(Func<int, Task> handler, int totalCount, int maxPerFrame, CancellationToken cancellationToken)
		{
			if (totalCount <= 0) return;
			
			var processed = 0;
			
			while (processed < totalCount)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return;
				}
				
				var batchSize = Math.Min(maxPerFrame, totalCount - processed);
				
				try
				{
					await handler.Invoke(batchSize);
				}
				catch (Exception ex)
				{
					throw ex;
				}

				processed += batchSize;
				
				await _frameAwaiter.WaitNextFrame();
			}
		}
	}
}