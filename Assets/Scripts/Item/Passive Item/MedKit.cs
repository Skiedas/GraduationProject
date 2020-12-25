using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : PassiveItem
{
    [SerializeField] private int _healthNumber;

    public override void ActivateItemEffect()
    {
        ItemEffect();
    }

    protected override void ItemEffect()
    {
        Player.IncreaseMaxHealth(_healthNumber);
    }
}
