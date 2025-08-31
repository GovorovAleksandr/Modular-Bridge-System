using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GovorovAleksandr.BridgeBuilding.Contracts;

namespace GovorovAleksandr.BridgeBuilding.Internal
{
	internal sealed class BridgeBuilder : IBridgeBuilder, IInitializable
	{
		private readonly IServiceLocator _serviceLocator;
		
		private BridgeSectionPool _sectionPool;
		private BridgeBuildOrchestrator _buildOrchestrator;

		private bool _isBuilding;

		public BridgeBuilder(IServiceLocator serviceLocator)
		{
			_serviceLocator = serviceLocator;
		}

		public void Initialize()
		{
			_sectionPool = _serviceLocator.Resolve<BridgeSectionPool>();
			_buildOrchestrator = _serviceLocator.Resolve<BridgeBuildOrchestrator>();
		}
		
		public async Task CreateAsync(CancellationToken cancellationToken)
		{
			if (_isBuilding) return;
			if (cancellationToken.IsCancellationRequested) return;
			
			_isBuilding = true;
			
			var start = _sectionPool.Get(BridgeSectionType.Start);
			await _buildOrchestrator.InstallStartSection(start, cancellationToken);
			
			if (cancellationToken.IsCancellationRequested)
			{
				_sectionPool.Release(start);
				_isBuilding = false;
				return;
			}
			
			var middles = new List<IBridgeSection>();
			
			var end = _sectionPool.Get(BridgeSectionType.End);
			await _buildOrchestrator.InstallEndSection(start, middles, end, cancellationToken);
			
			_isBuilding = false;
			
			if (cancellationToken.IsCancellationRequested)
			{
				_sectionPool.Release(start);
				foreach (var middleSection in middles) _sectionPool.Release(middleSection);
				_sectionPool.Release(end);
			}
		}
	}
}