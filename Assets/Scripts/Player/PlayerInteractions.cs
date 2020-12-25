using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] private float _interactRadius;
    [SerializeField] private LayerMask _mask;

    private PlayerInput _input;

    private void Awake()
    {
        _input = new PlayerInput();
        _input.Enable();

        _input.Player.Interact.performed += ctx => TryInteract();
        _input.Player.OpenInventory.performed += ctx => TryOpenInventory();
        _input.Player.Shoot.performed += ctx => TryShoot();
        _input.Player.UseItem.performed += ctx => TryUseItem();
        _input.Player.SwitchActiveItem.performed += ctx => TrySwitchActiveItem();
        _input.Player.SwitchWeapon.performed += ctx => TrySwitchWeapon();
    }

    private void TryInteract()
    {
        Collider2D hitCollider = Physics2D.OverlapCircle(transform.position, _interactRadius, _mask);

        if (hitCollider)
        {
            if(hitCollider.TryGetComponent(out ExitDoor door))
            {
                door.Interact();
            }
        }
    }

    private void TryUseItem()
    {
        _player.UseItem();
    }

    private void TryOpenInventory()
    {
        if (_player.Inventory.IsOpen == false)
        {
            _player.Inventory.Open();
        }
        else if(_player.Inventory.IsOpen == true)
        {
            _player.Inventory.Close();
        }
    }

    private void TrySwitchActiveItem()
    {
        _player.Inventory.SwitchActiveItem();
    }

    private void TrySwitchWeapon()
    {
        _player.Inventory.SwitchWeapon();
    }

    private void TryShoot()
    {
        if (Time.timeScale != 1)
            return;

        _player.CurrentWeapon.Shoot();
    }
}
