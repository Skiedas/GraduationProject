using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CanvasGroup))]
public class RewardPanel : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private AllItemsList _itemsList;
    [SerializeField] private ItemView _template;
    [SerializeField] private Transform _container;
    [SerializeField] int _iconScale;
    [SerializeField] private int _numberOfRewardItems;

    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _skipButton;

    [SerializeField] private TMP_Text _itemLabelSlot;
    [SerializeField] private TMP_Text _itemAbillityDescriptionSlot;

    [SerializeField] private List<Item> _rewardItems;

    private CanvasGroup _canvasGroup;
    private Item _selectedItem;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        _spawner.AllEnemyDied += OnAllEnemyDied;
        _confirmButton.onClick.AddListener(OnConfirmButtonClick);
        _skipButton.onClick.AddListener(OnSkipButtonClick);
    }

    private void OnDisable()
    {
        _confirmButton.onClick.RemoveListener(OnConfirmButtonClick);
        _skipButton.onClick.RemoveListener(OnSkipButtonClick);
    }

    public void IncreaseRewardItemsNumber()
    {
        _numberOfRewardItems++;
    }

    private void OnAllEnemyDied()
    {
        _spawner.AllEnemyDied -= OnAllEnemyDied;

        for (int i = 0; i < _numberOfRewardItems; i++)
        {
            GenerateRewardItem();
        }

        Time.timeScale = 0;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.alpha = 1;
    }

    private void GenerateRewardItem()
    {
        Item item = _itemsList.GetRandomAvalibaleItem();
        _rewardItems.Add(item);
        InitRewardItemView(item);
    }

    private void InitRewardItemView(Item item)
    {
        var view = Instantiate(_template, _container);
        view.SelectItem += OnSelectItem;
        view.Init(item, _iconScale, _itemLabelSlot, _itemAbillityDescriptionSlot);
        view.RenderIcon();
    }

    private void OnSelectItem(Item item, ItemView view)
    {
        view.SelectItem -= OnSelectItem;

        _selectedItem = item;
    }

    private void OnConfirmButtonClick()
    {
        _player.AddItem(_selectedItem);

        OnButtonCLick();
    }

    private void OnSkipButtonClick()
    {
        OnButtonCLick();
    }

    private void OnButtonCLick()
    {
        _selectedItem = null;
        _rewardItems.Clear();

        for (int i = 0; i < _container.childCount; i++)
        {
            var child = _container.GetChild(i);
            Destroy(child.gameObject);

        }

        Time.timeScale = 1;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.alpha = 0;
        _spawner.NextWave();

        _spawner.AllEnemyDied += OnAllEnemyDied;
    }
}
