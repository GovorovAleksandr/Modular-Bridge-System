using System;
using GovorovAleksandr.BridgeBuilding.Contracts;

namespace GovorovAleksandr.BridgeBuilding.Internal
{
	internal sealed class BridgeConstructionContext : IBridgeConstructionContext
	{
		private IServiceLocator _serviceLocator;
		private ServiceInitializer _serviceInitializer;
		
		public BridgeConstructionContext(IServiceLocator serviceLocator)
		{
			_serviceLocator = serviceLocator;
			_serviceInitializer = new(serviceLocator);
		}

		public IBridgeBuilder Builder => _serviceLocator?.Resolve<IBridgeBuilder>() ?? throw new ObjectDisposedException(nameof(BridgeConstructionContext));
		
		public async void Initialize()
		{
			await _serviceInitializer.InitializeAsync();
		}

		public void Dispose()
		{
			foreach (var disposable in _serviceLocator.ResolveMany<IDisposable>())
			{
				disposable.Dispose();
			}
			
			_serviceLocator = null;
			_serviceInitializer = null;
		}
	}
}