using GovorovAleksandr.BridgeBuilding.Contracts;
using UnityEngine;

namespace GovorovAleksandr.BridgeBuilding.UnityAdapters
{
	public sealed class UnityBridgeSectionPlacedListener : IBridgeSectionPlacedListener, IInitializable
	{
		private readonly IServiceLocator _serviceLocator;
		
		private readonly Material _defaultMaterial;
		private readonly float _animationDuration;
		private readonly AnimationCurve _curve;
		
		private PureAnimation _animation;

		public UnityBridgeSectionPlacedListener(IServiceLocator serviceLocator, Material defaultMaterial, float animationDuration, AnimationCurve curve)
		{
			_serviceLocator = serviceLocator;
			_defaultMaterial = defaultMaterial;
			_animationDuration = animationDuration;
			_curve = curve;
		}

		public void Initialize()
		{
			var coroutineRunner = _serviceLocator.Resolve<CoroutineRunner>();
			_animation = new PureAnimation(coroutineRunner);
		}

		public void HandlePlaced(IBridgeSection section)
		{
			var castedSection = UnityBridgeSectionConverter.CastToUnitySection(section);
			castedSection.Renderer.sharedMaterial = _defaultMaterial;
			
			var defaultScale = castedSection.transform.localScale;

			_animation.Run(_animationDuration, progress =>
			{
				var scale = defaultScale * _curve.Evaluate(progress);
				castedSection.transform.localScale = scale;
			});
		}
	}
}