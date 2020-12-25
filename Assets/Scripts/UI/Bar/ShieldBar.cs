using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShieldBar : Bar
{
    private void OnEnable()
    {
        Player.ShieldChanged += OnValueChanged;
        Slider.value = 1;
    }

    private void OnDisable()
    {
        Player.ShieldChanged -= OnValueChanged;
    }

    private void OnValueChanged(int value, int maxValue)
    {
        Slider.DOValue((float)value / maxValue, .5f).Play();
    }
}