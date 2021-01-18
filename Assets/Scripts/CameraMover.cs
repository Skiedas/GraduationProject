using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _offsetX;
    [SerializeField] private float _offsetY;

    private PlayerInput _input;
    private Vector3 _targetPosition;

    private void Awake()
    {
        _input = new PlayerInput();
        _input.Enable();
    }

    private void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(_input.Player.MousePosition.ReadValue<Vector2>());

        SetTargetPosition(mousePosition); 
        Move();
    }

    private void Move()
    {
        transform.position = Vector3.Lerp(transform.position, _targetPosition, .015f);
    }

    private void SetTargetPosition(Vector2 mousePosition)
    {
        float targetXOffset = (mousePosition.x - _player.transform.position.x) / _offsetX;
        float targetYOffset = (mousePosition.y - _player.transform.position.y) / _offsetY;

        _targetPosition = new Vector3(_player.transform.position.x + targetXOffset, _player.transform.position.y + targetYOffset, transform.position.z);
    }
}
