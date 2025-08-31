using System.Collections.Generic;

namespace GovorovAleksandr.BridgeBuilding.Contracts
{
	public interface IBridgePipelineConfig
	{
		IEnumerable<BridgeSectionType> GetBeforeMiddle();
		IEnumerable<BridgeSectionType> GetAfterMiddle();
	}
}