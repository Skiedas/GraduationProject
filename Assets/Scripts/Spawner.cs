using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Player _player;
    [SerializeField] private Transform _spawnCenter;
    [SerializeField] private float _radius;

    private Wave _currentWave;
    private int _currentWaveNumber = 0;
    private float _timeAfterLastSpawn;
    private int _currentWaveEnemyNumber;
    private int _wavesComplete;
    private int _spawned;
    private int _killed;

    public event UnityAction AllEnemyDied;
    public event UnityAction AllWavesComplete;
    public event UnityAction<int, int> EnemyCountChanged;
    public event UnityAction<int, int> WaveChanged;

    private void Start()
    {
        SetWave(_currentWaveNumber);
        EnemyCountChanged?.Invoke(_killed, _currentWave.EnemyCount);
    }

    private void Update()
    {
        if (_wavesComplete == _waves.Count)
        {
            _wavesComplete = 0;
            AllWavesComplete?.Invoke();
        }

        if (_currentWave == null)
            return;

        _timeAfterLastSpawn += Time.deltaTime;

        if (_timeAfterLastSpawn >= _currentWave.Delay)
        {
            InstantiateEnemy();
            ++_spawned;
            _timeAfterLastSpawn = 0;
            EnemyCountChanged?.Invoke(_killed, _spawned);
        }

        if (_currentWaveEnemyNumber == _spawned)
        {
            _currentWave = null;
        }
    }

    private void InstantiateEnemy()
    {
        Vector3 spawnPoint = _spawnCenter.position + new Vector3(Random.value - 0.5f, Random.value - 0.5f, Random.value - 0.5f).normalized * _radius;
        Enemy enemy = Instantiate(_currentWave.Templates[Random.Range(0, _currentWave.Templates.Length)], spawnPoint, Quaternion.identity, _spawnCenter).GetComponent<Enemy>();
        enemy.Init(_player);
        enemy.Dying += OnEnemyDying;
    }

    private void SetWave(int index)
    {
        _spawned = 0;
        _killed = 0;
        _currentWave = _waves[index];
        _currentWaveEnemyNumber = _currentWave.EnemyCount;
        WaveChanged?.Invoke(_currentWaveNumber + 1, _waves.Count);
    }

    public void NextWave()
    {
        SetWave(++_currentWaveNumber);
    }

    private void OnEnemyDying(Enemy enemy)
    {
        ++_killed;
        EnemyCountChanged?.Invoke(_killed, _spawned);

        enemy.Dying -= OnEnemyDying;

        if (_killed >= _currentWaveEnemyNumber)
        {
            _wavesComplete++;

            if (_waves.Count > _currentWaveNumber + 1)
            {
                AllEnemyDied?.Invoke();
            }
        }
    }
}

[System.Serializable]
public class Wave
{
    public GameObject[] Templates;
    public float Delay;
    public int EnemyCount;
}