using System;
using System.Threading;
using System.Threading.Tasks;
using GovorovAleksandr.BridgeBuilding.Contracts;

namespace GovorovAleksandr.BridgeBuilding.UnityAdapters
{
	public sealed class UnityBridgeSectionBatchInstantiator
	{
		private readonly IServiceLocator _serviceLocator;
		private readonly UnityBridgeSectionInstantiator _sectionInstantiator;
		
		private IBridgeSectionFactoryConfig _config;
		private BatchScheduler _batchScheduler;

		public UnityBridgeSectionBatchInstantiator(IServiceLocator serviceLocator, UnityBridgeSectionInstantiator sectionInstantiator)
		{
			_serviceLocator = serviceLocator;
			_sectionInstantiator = sectionInstantiator;
		}

		public void Initialize()
		{
			_config = _serviceLocator.Resolve<IBridgeSectionFactoryConfig>();
			_batchScheduler = _serviceLocator.Resolve<BatchScheduler>();
		}

		public async Task<IBridgeSection[]> InstantiateBatch(BridgeSectionType sectionType, int count, Action<IBridgeSection> handler, CancellationToken cancellationToken)
		{
			if (count <= 0) return Array.Empty<IBridgeSection>();
			
			var result = new IBridgeSection[count];
			var createdCount = 0;
			
			await _batchScheduler.Run(batchSize =>
			{
				for (var i = 0; i < batchSize; i++)
				{
					if (cancellationToken.IsCancellationRequested) return Task.CompletedTask;
					var instance = _sectionInstantiator.Instantiate(sectionType);
					result[createdCount++] = instance;
					handler?.Invoke(instance);
				}
				return Task.CompletedTask;
			}, count, _config.MaxInstantiatePerFrame, cancellationToken);
			
			return result;
		}
	}
}