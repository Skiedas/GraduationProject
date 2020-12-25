using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _maxShield;
    [SerializeField] private float _timeBeforeStartShieldRegen;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private RewardPanel _rewardPanel;

    [SerializeField] private ActiveItem _defaultActiveItem;

    private int _health;
    private int _shield;
    private float _elapsedTime;

    public int Health => _health;
    public int MaxHealth => _maxHealth;
    public Inventory Inventory => _inventory;
    public RewardPanel RewardPanel => _rewardPanel;
    public Weapon CurrentWeapon => _inventory.CurrentWeapon;

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int, int> ShieldChanged;
    public event UnityAction Died;

    private event UnityAction DamageApplied;


    private void OnEnable()
    {
        DamageApplied += OnDamageApplied;
    }

    private void OnDisable()
    {
        DamageApplied -= OnDamageApplied;
    }

    private void Awake()
    {
        AddItem(_defaultActiveItem);
    }

    private void Start()
    {
        _health = _maxHealth;
        _shield = _maxShield;
        HealthChanged?.Invoke(_health, _maxHealth);
        ShieldChanged?.Invoke(_shield, _maxShield);
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _timeBeforeStartShieldRegen && _shield < _maxShield)
        {
            StartShieldRegen();
        }

    }

    public void ApplyDamage(int damage)
    {
        if (_shield > 0)
        {
            _shield -= damage;
            ShieldChanged?.Invoke(_shield, _maxShield);
        }
        else
        {
            _health -= damage;
            HealthChanged?.Invoke(_health, _maxHealth);
        }

        DamageApplied?.Invoke();

        if (_health <= 0)
            Die();
    }

    public void ApplyHealing(int healValue)
    {
        _health += healValue;
        HealthChanged?.Invoke(_health, _maxHealth);
    }

    public void AddItem(Item item)
    {
        item.Init(this);
        Inventory.AddItem(item);
    }

    public void UseItem()
    {
        _inventory.UseActiveItem();
    }

    public void IncreaseMaxHealth(int health)
    {
        _maxHealth += health;
        _health += health;
        HealthChanged?.Invoke(_health, _maxHealth);
    }

    private void OnDamageApplied()
    {
        StopShieldRegen();
    }

    private void StartShieldRegen()
    {
        _shield++;
        _elapsedTime = 0;
        ShieldChanged?.Invoke(_shield,_maxShield);
    }

    private void StopShieldRegen()
    {
        _elapsedTime = -5;
    }

    private void Die()
    {
        Died?.Invoke();
    }
}
