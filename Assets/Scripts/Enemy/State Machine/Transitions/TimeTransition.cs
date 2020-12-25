using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTransition : Transition
{
    [SerializeField] private float _timeAfterSpawn;

    private float _elapsedTime;

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _timeAfterSpawn)
        {
            NeedTransit = true;
        }
    }
}