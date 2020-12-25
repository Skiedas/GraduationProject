using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public abstract class Bar : MonoBehaviour
{
    [SerializeField] protected Player Player;
    [SerializeField] protected Slider Slider;
}
