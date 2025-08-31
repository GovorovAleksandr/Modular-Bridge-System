using System;

namespace GovorovAleksandr.ModularBridgeSystem.Examples
{
	internal interface IBridgeBuildingInput
	{
		event Action BuildBridgeButtonPressed;
		event Action CancelBuildingButtonPressed;
	}
}