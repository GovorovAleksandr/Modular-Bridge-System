using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GovorovAleksandr.BridgeBuilding.Contracts;
using UnityEngine;

namespace GovorovAleksandr.BridgeBuilding.UnityAdapters
{
	public sealed class UnityBridgeSectionFactory : IBridgeSectionFactory, IPrioritizedInitializable
	{
		private readonly UnityBridgeSectionInstantiator _sectionInstantiator;
		private readonly UnityBridgeSectionBatchInstantiator _sectionBatchInstantiator;
		
		public UnityBridgeSectionFactory(IServiceLocator serviceLocator, IReadOnlyDictionary<BridgeSectionType, UnityBridgeSection> prefabs)
		{
			_sectionInstantiator = new UnityBridgeSectionInstantiator(prefabs);
			_sectionBatchInstantiator = new UnityBridgeSectionBatchInstantiator(serviceLocator, _sectionInstantiator);
		}

		public int Priority => -100;

		public void Initialize()
		{
			_sectionBatchInstantiator.Initialize();
		}

		public IBridgeSection Instantiate(BridgeSectionType sectionType)
		{
			return _sectionInstantiator.Instantiate(sectionType);
		}

		public async Task<IBridgeSection[]> InstantiateBatch(BridgeSectionType sectionType, int count, Action<IBridgeSection> handler = null, CancellationToken cancellationToken = default)
		{
			return await _sectionBatchInstantiator.InstantiateBatch(sectionType, count, handler, cancellationToken);
		}
	}
}