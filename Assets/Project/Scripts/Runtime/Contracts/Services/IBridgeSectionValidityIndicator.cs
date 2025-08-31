namespace GovorovAleksandr.BridgeBuilding.Contracts
{
	public interface IBridgeSectionValidityIndicator
	{
		void MarkValid(IBridgeSection section);
		void MarkInvalid(IBridgeSection section);
	}
}