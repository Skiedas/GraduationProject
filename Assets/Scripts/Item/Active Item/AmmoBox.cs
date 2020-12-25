using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : ActiveItem
{
    public override void UseItemAbility()
    {
        ItemAbility();
    }

    protected override void ItemAbility()
    {
        Player.CurrentWeapon.RecoverAmmo();
    }
}
