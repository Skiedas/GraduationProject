using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllItemsList : MonoBehaviour
{
    [SerializeField] private List<Item> _avalibaleItems;

    public Item GetRandomAvalibaleItem()
    {
        if (_avalibaleItems.Count > 0)
            return _avalibaleItems[Random.Range(0, _avalibaleItems.Count)];
        else
            return null;
    }
}
