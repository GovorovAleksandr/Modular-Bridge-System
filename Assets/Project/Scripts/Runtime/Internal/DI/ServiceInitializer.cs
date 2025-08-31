using System;
using System.Linq;
using System.Threading.Tasks;
using GovorovAleksandr.BridgeBuilding.Contracts;

namespace GovorovAleksandr.BridgeBuilding.Internal
{
	internal sealed class ServiceInitializer
	{
		private readonly IServiceLocator _serviceLocator;
		
		private bool _isInitialized;

		public ServiceInitializer(IServiceLocator serviceLocator)
		{
			_serviceLocator = serviceLocator;
		}

		public async Task InitializeAsync()
		{
			if (_isInitialized) throw new InvalidOperationException("Services has already been initialized.");

			InitializeAllSync();
			await InitializeAllAsync();
			
			_isInitialized = true;
		}

		private void InitializeAllSync()
		{
			var items = _serviceLocator.ResolveMany<IInitializable>().ToList();
			items.Sort(Compare);
			foreach (var initializable in items) initializable.Initialize();
		}
		
		private async Task InitializeAllAsync()
		{
			var items = _serviceLocator.ResolveMany<IAsyncInitializable>().ToList();
			items.Sort(Compare);
			foreach (var initializable in items) await initializable.InitializeAsync();
		}

		private static int Compare(object a, object b)
		{
			var aPriority = GetPriority(a);
			var bPriority = GetPriority(b);
			var primalComparer = aPriority.CompareTo(bPriority);
			return primalComparer != 0 ? primalComparer : string.Compare(a.GetType().Name, b.GetType().Name, StringComparison.Ordinal);
		}
		
		private static int GetPriority(object initializable) => (initializable as IPrioritized)?.Priority ?? 0;
	}
}