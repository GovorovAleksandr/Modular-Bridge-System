using System.Collections.Generic;
using System.Linq;
using GovorovAleksandr.BridgeBuilding.Contracts;
using UnityEngine;

namespace GovorovAleksandr.BridgeBuilding.Internal
{
	internal sealed class MiddleSectionEnsurer : IInitializable
	{
		private readonly IServiceLocator _serviceLocator;
		
		private MiddleSectionPlanner _planner;
		private BridgeSectionPool _sectionPool;

		public MiddleSectionEnsurer(IServiceLocator serviceLocator)
		{
			_serviceLocator = serviceLocator;
		}

		public void Initialize()
		{
			_planner = _serviceLocator.Resolve<MiddleSectionPlanner>();
			_sectionPool = _serviceLocator.Resolve<BridgeSectionPool>();
		}

		public void EnsureMiddleSections(Vector3 startPosition, List<IBridgeSection> middleSections, Vector3 endPosition)
		{
			var pipeline = _planner.Plan(startPosition, endPosition);
			
			foreach (var section in middleSections) _sectionPool.Release(section);
			
			middleSections.Clear();
			middleSections.AddRange(pipeline.Select(sectionType => _sectionPool.Get(sectionType)));
		}
	}
}