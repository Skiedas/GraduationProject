using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : ActiveItem
{
    [SerializeField] private int _poisonValue;

    public override void UseItemAbility()
    {
        ItemAbility();
    }

    protected override void ItemAbility()
    {
        Player.ApplyDamage(_poisonValue);
    }
}
