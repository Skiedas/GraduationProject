using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveItem : Item
{
    public abstract void UseItemAbility();

    protected abstract void ItemAbility();
}
