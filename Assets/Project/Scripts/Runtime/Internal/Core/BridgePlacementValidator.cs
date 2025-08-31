using System.Collections.Generic;
using System.Linq;
using GovorovAleksandr.BridgeBuilding.Contracts;
using UnityEngine;

namespace GovorovAleksandr.BridgeBuilding.Internal
{
	internal sealed class BridgePlacementValidator : IInitializable
	{
		private readonly IServiceLocator _serviceLocator;
		
		private readonly List<IBridgePlacementRule> _rules = new();
		
		public BridgePlacementValidator(IServiceLocator serviceLocator)
		{
			_serviceLocator = serviceLocator;
		}
		
		public void Initialize()
		{
			_rules.Clear();
			_rules.AddRange(_serviceLocator.ResolveMany<IBridgePlacementRule>());
		}

		public bool IsValid(Vector3 startPosition, Vector3 endPosition)
		{
			return _rules.All(rule => rule.IsValid(startPosition, endPosition));
		}
	}
}