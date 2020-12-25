using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _label;
    [SerializeField] private string _abilityDescription;
    [SerializeField] private string _description;
    [SerializeField] private Vector2 _spriteSize;

    protected Player Player;
    protected RewardPanel RewardPanel;

    public Sprite Icon => _icon;
    public string Label => _label;
    public string AbilityDescription => _abilityDescription;
    public string Description => _description;
    public Vector2 SpriteSize => _spriteSize;

    public void Init(Player player)
    {
        Player = player;
    }
}
