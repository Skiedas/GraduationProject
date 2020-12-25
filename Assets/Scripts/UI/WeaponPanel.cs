using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPanel : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private ItemView _template;
    [SerializeField] private Transform _container;
    [SerializeField] private int _iconScale;

    private void OnEnable()
    {
        _inventory.WeaponChanged += OnWeaponChanged;
    }

    private void OnDisable()
    {
        _inventory.WeaponChanged -= OnWeaponChanged;
    }

    private void OnWeaponChanged(Weapon weapon)
    {
        if (_container.childCount > 0)
        {
            for (int i = 0; i < _container.childCount; i++)
            {
                var child = _container.GetChild(i);
                Destroy(child.gameObject);
            }
        }

        var view = Instantiate(_template, _container);
        view.Init(weapon, _iconScale);
        view.RenderIcon();
    }
}
