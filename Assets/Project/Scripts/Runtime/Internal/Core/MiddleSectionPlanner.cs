using System.Collections.Generic;
using System.Linq;
using GovorovAleksandr.BridgeBuilding.Contracts;
using UnityEngine;

namespace GovorovAleksandr.BridgeBuilding.Internal
{
	internal sealed class MiddleSectionPlanner : IInitializable
	{
		private readonly IServiceLocator _serviceLocator;
		
		private BridgeMetricsService _metricsService;
		private IBridgePipelineConfig _pipelineConfig;

		public MiddleSectionPlanner(IServiceLocator serviceLocator)
		{
			_serviceLocator = serviceLocator;
		}

		public void Initialize()
		{
			_metricsService =  _serviceLocator.Resolve<BridgeMetricsService>();
			_pipelineConfig = _serviceLocator.Resolve<IBridgePipelineConfig>();
		}
		
		public IEnumerable<BridgeSectionType> Plan(Vector3 start, Vector3 end)
		{
			var before = _pipelineConfig.GetBeforeMiddle().ToList();
			var after = _pipelineConfig.GetAfterMiddle().ToList();
			
			var fullDistance = Vector3.Distance(start, end);
			var beforeDistance = before.Sum(t => _metricsService.GetLength(t));
			var afterDistance = after.Sum(t => _metricsService.GetLength(t));
			var middleDistance = fullDistance - beforeDistance - afterDistance;
			var middleLength = _metricsService.GetLength(BridgeSectionType.Middle);
			var middleCount = Mathf.FloorToInt(middleDistance / middleLength);
			var middleTypes = Enumerable.Repeat(BridgeSectionType.Middle, middleCount > 0 ? middleCount : 1);
			
			return before.Concat(middleTypes).Concat(after);
		}
	}
}