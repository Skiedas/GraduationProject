using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : ActiveItem
{
    [SerializeField] private int _healValue;

    public override void UseItemAbility()
    {
        ItemAbility();
    }

    protected override void ItemAbility()
    {
        if(Player.Health < Player.MaxHealth)
            Player.ApplyHealing(_healValue);
    }
}
