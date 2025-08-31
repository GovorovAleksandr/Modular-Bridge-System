using System;
using System.Collections.Generic;
using GovorovAleksandr.BridgeBuilding.Contracts;

namespace GovorovAleksandr.BridgeBuilding.Internal
{
	internal sealed class BridgeSectionPool : IInitializable
	{
		private readonly IServiceLocator _serviceLocator;
		
		private readonly Dictionary<BridgeSectionType, Stack<IBridgeSection>> _pool = new();

		private IBridgeSectionFactory _sectionFactory;

		public BridgeSectionPool(IServiceLocator serviceLocator)
		{
			_serviceLocator = serviceLocator;
		}

		public void Initialize()
		{
			_sectionFactory = _serviceLocator.Resolve<IBridgeSectionFactory>();
		}

		public int GetCount(BridgeSectionType sectionType)
		{
			var stack = GetStack(sectionType);
			return stack.Count;
		}
		
		public IBridgeSection Get(BridgeSectionType sectionType)
		{
			var stack = GetStack(sectionType);

			if (!stack.TryPop(out var instance))
			{
				instance = _sectionFactory.Instantiate(sectionType);
			}
			
			instance.Enable();
			return instance;
		}

		public void Release(IBridgeSection instance)
		{
			var sectionType = instance.Type;
			var stack = GetStack(sectionType);

			if (stack.Contains(instance))
				throw new InvalidOperationException($"Section of type {sectionType} is already released into the pool.");
			
			instance.Disable();
			
			stack.Push(instance);
		}

		private Stack<IBridgeSection> GetStack(BridgeSectionType sectionType)
		{
			if (!_pool.TryGetValue(sectionType, out var stack))
			{
				_pool[sectionType] = stack = new Stack<IBridgeSection>();
			}

			return stack;
		}
	}
}