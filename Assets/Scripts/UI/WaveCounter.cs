using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveCounter : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _spawner.WaveChanged += OnWaveChanged;
    }

    private void OnDisable()
    {
        _spawner.WaveChanged -= OnWaveChanged;
    }

    private void OnWaveChanged(int currentWaveNumber, int allWaves)
    {
        _text.text = $"Wave: {currentWaveNumber} / {allWaves }";
    }
}
