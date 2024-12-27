using UnityEngine.UI;

public interface IDiceTarget
{
    bool isInTargetArea {  get; }

    Image visibleTarget { get; }

    void PlaceDiceOnItem(int coinAmount);
}
