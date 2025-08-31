using GovorovAleksandr.BridgeBuilding.Contracts;
using UnityEngine;

namespace GovorovAleksandr.BridgeBuilding.UnityAdapters
{
	public sealed class ServiceRegistrar : IServiceRegistrar
	{
		private readonly UnityBridgeConfigSo _config;
		private readonly MonoBehaviour _coroutineContext;

		public ServiceRegistrar(UnityBridgeConfigSo config, MonoBehaviour coroutineContext)
		{
			_config = config;
			_coroutineContext = coroutineContext;
		}

		public void RegisterServices(IServiceLocator serviceLocator)
		{
			serviceLocator.RegisterSingle<IBridgePipelineConfig>(_config);
			serviceLocator.RegisterSingle<IBridgeSectionPoolConfig>(_config);
			serviceLocator.RegisterSingle<IBridgeSectionFactoryConfig>(_config);
			serviceLocator.RegisterSingle<IBridgeConstraintsConfig>(_config);
			
			serviceLocator.RegisterSingle(new BatchScheduler(serviceLocator));
			serviceLocator.RegisterSingle(new CoroutineRunner(_coroutineContext));
			serviceLocator.RegisterSingle(new InputProvider());
			
			serviceLocator.RegisterSingle<IFrameAwaiter>(new UnityFrameAwaiter(serviceLocator));
			serviceLocator.RegisterSingle<IBridgeSectionFactory>(new UnityBridgeSectionFactory(serviceLocator, _config.Prefabs));
			serviceLocator.RegisterSingle<IBridgeSectionPositionProvider>(new UnityBridgeSectionPositionProvider(serviceLocator, _config.GroundLayerMask, _config.MaxBuildingDistance));
			serviceLocator.RegisterSingle<IBridgeSectionRotationPolicy>(new UnityBridgeSectionRotationPolicy(_config.BridgeSectionYRotationOffset));
			serviceLocator.RegisterSingle<IGroundHeightProvider>(new UnityGroundHeightProvider(_config.GroundLayerMask, _config.MaxBuildingDistance, _config.GroundSearchStartHeight));
			serviceLocator.RegisterMany<IBridgePlacementRule>(new UnityObstacleBridgePlacementRule(_config.ObstacleIgnoreLayerMask, _config.MaxObstacleHeight));
			serviceLocator.RegisterSingle<IBridgeSectionDescriptorProvider>(new UnityBridgeSectionDescriptorProvider(_config.Prefabs));
			serviceLocator.RegisterSingle<IBridgeSectionPlacementRule>(new UnityBridgeSectionPlacementRule(serviceLocator));
			serviceLocator.RegisterSingle<IBridgeSectionValidityIndicator>(new UnityBridgeSectionValidityIndicator(_config.SectionValidMaterial, _config.SectionInvalidMaterial));
			serviceLocator.RegisterMany<IBridgeSectionPlacedListener>(new UnityBridgeSectionPlacedListener(serviceLocator, _config.DefaultMaterial, _config.SpawnAnimationDuration, _config.SpawnAnimationCurve));
		}
	}
}