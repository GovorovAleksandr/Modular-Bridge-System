using System.Collections.Generic;
using System.Linq;
using GovorovAleksandr.BridgeBuilding.Contracts;
using UnityEngine;

namespace GovorovAleksandr.BridgeBuilding.UnityAdapters
{
	[CreateAssetMenu(menuName = "ModularBridgeSystem/UnityBridgeConfig", fileName = "UnityBridgeConfig")]
	public sealed class UnityBridgeConfigSo : ScriptableObject,
		IBridgePipelineConfig,
		IBridgeSectionPoolConfig,
		IBridgeSectionFactoryConfig,
		IBridgeConstraintsConfig
	{
		[Header("Core Bridge Settings")]
		[SerializeField] [Min(0f)] private float _maxSlopeAngle = 5.625f;
		[SerializeField] private List<BridgeSectionType> _sectionsBeforeMiddle = new() { BridgeSectionType.Filler, BridgeSectionType.Filler, };
		[SerializeField] private List<BridgeSectionType> _sectionsAfterMiddle =  new() { BridgeSectionType.Filler, BridgeSectionType.Filler, };
		
		[Header("Unity-Specific Settings")]
		[SerializeField] [Min(1f)] private float _maxBuildingDistance = 250f;
		[SerializeField] [Min(0)] private int _minPoolObjectsCount = 50;
		[SerializeField] [Min(1)] private int _maxInstantiatePerFrame = 10;
		[SerializeField] private LayerMask _groundLayerMask;
		[SerializeField] private LayerMask _obstacleIgnoreLayerMask;
		[SerializeField] private float _groundSearchStartHeight = 100f;
		[SerializeField] private float _maxObstacleHeight = 2f;
		[SerializeField] [Range(0f, 360f)] private float _bridgeSectionYRotationOffset = 90f;
		[SerializeField] private Material _sectionValidMaterial;
		[SerializeField] private Material _sectionInvalidMaterial;
		[SerializeField] private Material _defaultInvalidMaterial;
		[SerializeField] [Min(0.1f)] private float _spawnAnimationDuration = 1f;
		[SerializeField] private AnimationCurve _spawnAnimationCurve;
		[SerializeField] private List<UnityBridgeSection> _prefabs;

		public float MaxSlopeAngle => _maxSlopeAngle;
		
		public int MinPoolObjectsCount => _minPoolObjectsCount;
		public int MaxInstantiatePerFrame => _maxInstantiatePerFrame;
		
		public float MaxBuildingDistance => _maxBuildingDistance;
		public LayerMask GroundLayerMask => _groundLayerMask;
		public LayerMask ObstacleIgnoreLayerMask => ~_obstacleIgnoreLayerMask;
		public float GroundSearchStartHeight => _groundSearchStartHeight;
		public float MaxObstacleHeight => _maxObstacleHeight;
		public float BridgeSectionYRotationOffset => _bridgeSectionYRotationOffset;
		public Material SectionValidMaterial => _sectionValidMaterial;
		public Material SectionInvalidMaterial => _sectionInvalidMaterial;
		public Material DefaultMaterial => _defaultInvalidMaterial;
		public float SpawnAnimationDuration => _spawnAnimationDuration;
		public AnimationCurve SpawnAnimationCurve => _spawnAnimationCurve;
		public IReadOnlyDictionary<BridgeSectionType, UnityBridgeSection> Prefabs =>
			_prefabs.ToDictionary(prefab => prefab.Type, prefab => prefab);

		public IEnumerable<BridgeSectionType> GetBeforeMiddle() => _sectionsBeforeMiddle;
		public IEnumerable<BridgeSectionType> GetAfterMiddle() => _sectionsAfterMiddle;
	}
}