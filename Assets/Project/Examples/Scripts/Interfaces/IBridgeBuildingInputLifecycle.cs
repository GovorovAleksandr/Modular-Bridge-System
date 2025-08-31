using System;

namespace GovorovAleksandr.ModularBridgeSystem.Examples
{
	internal interface IBridgeBuildingInputLifecycle : IBridgeBuildingInput, IInitializable, IDisposable {}
}