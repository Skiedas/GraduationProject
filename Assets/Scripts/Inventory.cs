using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform _activeItemsContainer;
    [SerializeField] private Transform _passiveItemsContainer;
    [SerializeField] private Transform _weaponsContainer;
    [SerializeField] private ItemView _template;
    [SerializeField] private int _iconScale;

    [SerializeField] private List<PassiveItem> _passiveItems;
    [SerializeField] private List<ActiveItem> _activeItems;
    [SerializeField] private List<Weapon> _weapons;

    [SerializeField] private Image _itemIconSlot;
    [SerializeField] private TMP_Text _itemLabelSlot;
    [SerializeField] private TMP_Text _itemAbilityDescriptionSlot;
    [SerializeField] private TMP_Text _itemDescriptionSlot;

    private CanvasGroup _canvasGroup;
    private Transform _container;
    private bool _isOpen;
    private int _currentActiveItemNumber;
    private int _currentWeaponNumber;
    private ActiveItem _currentActiveItem;
    private Weapon _currentWeapon;

    public bool IsOpen => _isOpen;
    public Weapon CurrentWeapon => _currentWeapon;

    public event UnityAction<ActiveItem> ActiveItemChanged;
    public event UnityAction<Weapon> WeaponChanged;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        if (_weapons != null)
        {
            _currentWeaponNumber = 0;

            for (int i = 0; i < (_weapons.Count); i++)
            {
                AddItem(_weapons[i]);
            }
        }

        _currentWeapon = _weapons[_currentWeaponNumber];

        WeaponChanged?.Invoke(_currentWeapon);
    }

    public void AddItem(Item item)
    {
        if (item.TryGetComponent(out ActiveItem activeItem))
        {
            _activeItems.Add(activeItem);
            _container = _activeItemsContainer;

            _currentActiveItemNumber = _activeItems.Count - 1;
            _currentActiveItem = _activeItems[_currentActiveItemNumber];
            ActiveItemChanged?.Invoke(activeItem);
        }

        if (item.TryGetComponent(out PassiveItem passiveItem))
        {
            _passiveItems.Add(passiveItem);
            _container = _passiveItemsContainer;
            passiveItem.ActivateItemEffect();
        }

        if (item.TryGetComponent(out Weapon weapon))
        {
            //_weapons.Add(weapon);
            _container = _weaponsContainer;
        }

        var view = Instantiate(_template, _container);
        view.Init(item, _iconScale, _itemLabelSlot, _itemAbilityDescriptionSlot, _itemIconSlot, _itemDescriptionSlot);
        view.RenderIcon();
    }

    public void UseActiveItem()
    {
        _currentActiveItem.UseItemAbility();
    }

    public void SwitchActiveItem()
    {
        if (_currentActiveItemNumber < _activeItems.Count - 1)
            _currentActiveItemNumber++;
        else if (_currentActiveItemNumber == _activeItems.Count - 1)
            _currentActiveItemNumber = 0;

        _currentActiveItem = _activeItems[_currentActiveItemNumber];
        ActiveItemChanged?.Invoke(_currentActiveItem);
    }

    public void SwitchWeapon()
    {
        SpriteRenderer renderer;

        if (_currentWeaponNumber < _weapons.Count - 1)
            _currentWeaponNumber++;
        else if (_currentWeaponNumber == _weapons.Count - 1)
            _currentWeaponNumber = 0;

        _currentWeapon.enabled = false;
        renderer = _currentWeapon.GetComponent<SpriteRenderer>();
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0);

        _currentWeapon = _weapons[_currentWeaponNumber];

        _currentWeapon.enabled = true;
        renderer = _currentWeapon.GetComponent<SpriteRenderer>();
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1);

        WeaponChanged?.Invoke(_currentWeapon);
    }

    public void Open()
    {
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.alpha = 1;
        Time.timeScale = 0;

        _isOpen = true;
    }

    public void Close()
    {
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.alpha = 0;
        Time.timeScale = 1;

        _isOpen = false;
    }
}
