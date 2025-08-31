using System;
using System.Collections.Generic;

namespace GovorovAleksandr.BridgeBuilding.Internal
{
	internal sealed class ServiceContainer
	{
		private object _service;
		private List<object> _services;
		
		public bool IsSingle { get; private set; }
		public bool IsEmpty => IsSingle ? _service == null : _services == null || _services.Count == 0;
		
		public static ServiceContainer CreateAsSingle(object service) => new()
		{
			_service = service,
			IsSingle = true
		};
		
		public static ServiceContainer CreateAsMany(object service) => new()
		{
			_services = new List<object> { service },
			IsSingle = false
		};

		public object GetAsSingle()
		{
			ThrowIfIsNotSingle();
			return _service;
		}

		public IEnumerable<object> GetAsMany()
		{
			ThrowIfIsSingle();
			return _services;
		}

		public void Add(object service)
		{
			if (service == null) throw new ArgumentNullException(nameof(service));
			ThrowIfIsSingle();
			_services.Add(service);
		}

		private void ThrowIfIsSingle()
		{
			if (!IsSingle) return;
			throw new InvalidOperationException("Container is single; operation requires multi.");
		}
		
		private void ThrowIfIsNotSingle()
		{
			if (IsSingle) return;
			throw new InvalidOperationException("Container is multi; operation requires single.");
		}
	}
}