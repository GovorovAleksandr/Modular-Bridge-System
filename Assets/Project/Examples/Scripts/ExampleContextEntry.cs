using GovorovAleksandr.BridgeBuilding.UnityAdapters;
using UnityEngine;

namespace GovorovAleksandr.ModularBridgeSystem.Examples
{
	internal sealed class ExampleContextEntry : MonoBehaviour
	{
		[SerializeField] private CameraMoverSettings _cameraMoverSettings;
		[SerializeField] private UnityBridgeConfigSo _unityBridgeConfigSo;
		
		private ICameraInputLifecycle _cameraInput;
		private ICameraMover _cameraMover;
		
		private CursorController _cursorController;
		
		private IBridgeBuildingInputLifecycle _bridgeBuildingInput;
		private IBridgeBuilder _bridgeBuilder;
		
		private void Awake()
		{
			_cameraInput = new CameraInput();
			
			_cameraMover = new CameraMover(_cameraMoverSettings, _cameraInput);
			
			_cursorController = new CursorController();
			
			_bridgeBuildingInput = new BridgeBuildingInput();
			_bridgeBuilder = new BridgeBuilder(_unityBridgeConfigSo, this, _bridgeBuildingInput);
		}

		private void Start()
		{
			_cameraInput.Initialize();
			_bridgeBuildingInput.Initialize();
			
			_cameraMover.Initialize();
			_bridgeBuilder.Initialize();
			
			_cursorController.Initialize();
		}

		private void Update()
		{
			_cameraInput.Update();
		}

		private void LateUpdate()
		{
			_cameraInput.LateUpdate();
		}

		private void OnDestroy()
		{
			_bridgeBuilder.Dispose();
			_cameraMover.Dispose();
			
			_bridgeBuildingInput.Dispose();
			_cameraInput.Dispose();
		}
	}
}