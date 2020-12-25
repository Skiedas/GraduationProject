using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : PassiveItem
{
    public override void ActivateItemEffect()
    {
        ItemEffect();
    }

    protected override void ItemEffect()
    {
        Player.RewardPanel.IncreaseRewardItemsNumber();
    }
}
