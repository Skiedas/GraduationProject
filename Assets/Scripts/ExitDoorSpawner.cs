using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorSpawner : MonoBehaviour
{
    [SerializeField] private ExitDoor _template;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private GameOverPanel _panel; 

    private void OnEnable()
    {
        _spawner.AllWavesComplete += OnAllWavesComplete;
    }

    private void OnDisable()
    {
        _spawner.AllWavesComplete -= OnAllWavesComplete;
    }

    private void OnAllWavesComplete()
    {
        var exitDoor = Instantiate(_template, transform);
        exitDoor.Init(_panel);
    }
}
