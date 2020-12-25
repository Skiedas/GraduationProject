using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoCounter : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] TMP_Text _text;

    private Weapon _weapon;

    private void OnEnable()
    {
        _player.Inventory.WeaponChanged += OnWeaponChanged;
    }

    private void OnDisable()
    {
        _player.Inventory.WeaponChanged -= OnWeaponChanged;
    }

    private void OnWeaponChanged(Weapon weapon)
    {
        if (_weapon != null)
            _weapon.NumberOfAmmoChanged -= OnNumberOfAmmoChanged;

        _weapon = weapon;
        _weapon.NumberOfAmmoChanged += OnNumberOfAmmoChanged;
    }

    private void OnNumberOfAmmoChanged(int ammoInClip, int ammoLeft)
    {
        _text.text = $"{ammoInClip} / {ammoLeft}";
    }
}
