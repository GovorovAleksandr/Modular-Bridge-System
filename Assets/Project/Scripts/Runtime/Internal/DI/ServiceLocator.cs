using System;
using System.Collections.Generic;
using GovorovAleksandr.BridgeBuilding.Contracts;

namespace GovorovAleksandr.BridgeBuilding.Internal
{
	internal sealed class ServiceLocator : IServiceLocator
	{
		private readonly Dictionary<Type, ServiceContainer> _services = new();
		
		public void RegisterSingle<TService>(TService instance)
		{
			if (instance == null) throw new ArgumentNullException(nameof(instance));
			
			if (TryGetContainer<TService>(out _))
				throw new InvalidOperationException($"Service '{typeof(TService)}' already registered as single.");
			
			var serviceType = typeof(TService);
			_services[serviceType] = ServiceContainer.CreateAsSingle(instance);
		}

		public void RegisterMany<TService>(TService instance)
		{
			if (instance == null) throw new ArgumentNullException(nameof(instance));
			
			var serviceType = typeof(TService);
			
			if (!TryGetContainer<TService>(out var container))
			{
				_services[serviceType] = ServiceContainer.CreateAsMany(instance);
				return;
			}
			
			if (container.IsSingle) throw new InvalidOperationException($"Service '{serviceType}' is registered as single and cannot be registered as many.");
			
			container.Add(instance);
		}

		public TService Resolve<TService>()
		{
			if (!TryGetContainer<TService>(out var container))
				throw new InvalidOperationException($"Service '{typeof(TService)}' not registered.");
			
			if (!container.IsSingle) throw new InvalidOperationException($"Service '{typeof(TService)}' is registered as many and cannot be resolved as single.");
			
			return (TService)container.GetAsSingle();
		}

		public IEnumerable<TService> ResolveMany<TService>()
		{
			foreach (var container in _services.Values)
			{
				if (container.IsSingle)
				{
					var service = container.GetAsSingle();
					if (service is not TService typedService) continue;
					yield return typedService;
					continue;
				}
				
				foreach (var service in container.GetAsMany())
				{
					if (service is not TService typedService) continue;
					yield return typedService;
				}
			}
		}

		public bool IsRegistered<TService>()
		{
			return TryGetContainer<TService>(out var container) && container is { IsEmpty: false };
		}

		private bool TryGetContainer<TService>(out ServiceContainer container)
		{
			return _services.TryGetValue(typeof(TService), out container);
		}
	}
}