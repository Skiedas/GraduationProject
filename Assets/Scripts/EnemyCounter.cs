using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _spawner.EnemyCountChanged += OnEnemyCountChanged;
    }

    private void OnDisable()
    {
        _spawner.EnemyCountChanged -= OnEnemyCountChanged;
    }

    private void OnEnemyCountChanged(int killedEnemies, int spawnedEnemies)
    {
        _text.text = $"Enemies: {killedEnemies} / {spawnedEnemies}";
    }
}
