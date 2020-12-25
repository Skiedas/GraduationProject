using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private ItemView _template;
    [SerializeField] private Transform _container;
    [SerializeField] private int _iconScale;

    private void OnEnable()
    {
        _inventory.ActiveItemChanged += OnActiveItemChanged;
    }

    private void OnDisable()
    {
        _inventory.ActiveItemChanged -= OnActiveItemChanged;
    }

    private void OnActiveItemChanged(ActiveItem item)
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
        view.Init(item, _iconScale);
        view.RenderIcon();
    }
}
