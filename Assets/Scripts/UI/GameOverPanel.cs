using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CanvasGroup))]
public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _button;

    private CanvasGroup _group;

    private void Start()
    {
        _group = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
        _player.Died += OnPLayerDied;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
        _player.Died -= OnPLayerDied;
    }

    public void Show()
    {
        if (_player.Health > 0)
            _text.text = "Уровень пройден!";
        else
            _text.text = "Вы проиграли";

        _group.interactable = true;
        _group.blocksRaycasts = true;
        _group.alpha = 1;
    }

    private void OnPLayerDied()
    {
        Show();
    }

    private void OnButtonClick()
    {
        SceneManager.LoadScene(0);
    }
}
