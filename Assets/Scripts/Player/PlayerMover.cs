using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private PlayerInput _input;
    private Rigidbody2D _rb;
    private Animator _animator;
    private Vector2 _direction;
    private Vector2 _mousePosition;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _input = new PlayerInput();
        _input.Enable();

    }

    private void Update()
    {
        _direction = _input.Player.Move.ReadValue<Vector2>();
        _mousePosition = _input.Player.MousePosition.ReadValue<Vector2>();

        Move(_direction);
        SetLookDirection(_mousePosition);
    }

    private void Move(Vector2 direction)
    {
        _rb.velocity = _direction * _moveSpeed;

        if (_direction != Vector2.zero)
            _animator.SetBool("Move", true);
        else
            _animator.SetBool("Move", false);
    }

    private void SetLookDirection(Vector2 mousePosition)
    {
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        if (mousePosition.x > transform.position.x)
            transform.DOLocalRotate(new Vector3(0, 0, 0), 0.2f).Play();
        else if (mousePosition.x < transform.position.x)
            transform.DOLocalRotate(new Vector3(0, 180, 0), 0.2f).Play();
    }

}
