using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PassiveItem : Item
{
    public abstract void ActivateItemEffect();

    protected abstract void ItemEffect();
}
