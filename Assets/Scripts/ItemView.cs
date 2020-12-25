using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(RectTransform))]
public class ItemView : MonoBehaviour
{
    [SerializeField] private Button _itemButton;
    [SerializeField] private Image _itemIcon;

    private Image _itemIconSlot;
    private TMP_Text _labelSlot;
    private TMP_Text _abilityDescriptionSlot;
    private TMP_Text _itemDescriptionSlot;
    private Vector2 _spriteSize;

    private Item _item;
    private RectTransform _rectTransform;

    public event UnityAction<Item, ItemView> SelectItem;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        _itemButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _itemButton.onClick.RemoveListener(OnButtonClick);
    }

    public void Init(Item item, int iconScale, TMP_Text labelSlot = null, TMP_Text abilityDescriptionSlot = null, Image itemIconSlot = null, TMP_Text itemDescriptionSlot = null)
    {
        _item = item;
        _itemIconSlot = itemIconSlot;
        _labelSlot = labelSlot;
        _abilityDescriptionSlot = abilityDescriptionSlot;
        _itemDescriptionSlot = itemDescriptionSlot;

        _spriteSize = item.SpriteSize * iconScale;
        _rectTransform.sizeDelta = _spriteSize;
    }

    public void RenderIcon()
    {
        _itemIcon.sprite = _item.Icon;
    }

    public void RenderInfo()
    {
        if (_itemIconSlot != null)
            _itemIconSlot.sprite = _item.Icon;

        if (_itemDescriptionSlot != null)
            _itemDescriptionSlot.text = _item.Description;

        if (_labelSlot != null)
            _labelSlot.text = _item.Label;

        if (_abilityDescriptionSlot != null)
            _abilityDescriptionSlot.text = _item.AbilityDescription;
    }

    private void OnButtonClick()
    {
        SelectItem?.Invoke(_item, this);
        RenderInfo();
    }
}
