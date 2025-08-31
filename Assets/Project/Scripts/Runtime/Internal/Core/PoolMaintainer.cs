using System.Linq;
using System.Threading.Tasks;
using GovorovAleksandr.BridgeBuilding.Contracts;

namespace GovorovAleksandr.BridgeBuilding.Internal
{
	internal sealed class PoolMaintainer : IAsyncInitializable
	{
		private readonly IServiceLocator _serviceLocator;

		private BridgeSectionPool _pool;
		private IBridgeSectionPoolConfig _poolConfig;
		private IBridgeSectionFactory _factory;
		private AvailableSectionTypesProvider _sectionTypesProvider;
		
		public PoolMaintainer(IServiceLocator serviceLocator)
		{
			_serviceLocator = serviceLocator;
		}

		public async Task InitializeAsync()
		{
			_pool = _serviceLocator.Resolve<BridgeSectionPool>();
			_poolConfig = _serviceLocator.Resolve<IBridgeSectionPoolConfig>();
			_factory = _serviceLocator.Resolve<IBridgeSectionFactory>();
			_sectionTypesProvider = _serviceLocator.Resolve<AvailableSectionTypesProvider>();

			var sectionTypes = _sectionTypesProvider.Get().ToList();
			
			var sectionTypeCount = sectionTypes.Count;
			var batchCount = _poolConfig.MinPoolObjectsCount / sectionTypeCount;

			foreach (var sectionType in sectionTypes)
			{
				await _factory.InstantiateBatch(sectionType, batchCount, _pool.Release);
			}
		}
	}
}