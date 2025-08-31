using System;

namespace GovorovAleksandr.ModularBridgeSystem.Examples
{
	internal interface ICameraInputLifecycle : ICameraInput, IInitializable, IUpdatable, ILateUpdatable, IDisposable { }
}