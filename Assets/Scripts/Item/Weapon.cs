using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Weapon : Item
{
    [SerializeField] protected Bullet BulletTemplate;
    [SerializeField] protected Transform ShootPosition;
    [SerializeField] protected int ClipCapacity;
    [SerializeField] protected int NumberOfAmmo;
    [SerializeField] protected float ReloadTime;

    protected PlayerInput Input;
    protected int AmmoInClip;
    protected int AmmoLeft;
    protected bool IsReloading;

    private Vector2 _mousePosition;

    public event UnityAction<int, int> NumberOfAmmoChanged;

    private void OnEnable()
    {
        NumberOfAmmoChanged?.Invoke(AmmoInClip, AmmoLeft);
    }

    private void Awake()
    {
        Input = new PlayerInput();
        Input.Enable();
    }

    private void Start()
    {
        AmmoInClip = ClipCapacity;
        AmmoLeft = NumberOfAmmo;
    }

    private void Update()
    {
        _mousePosition = Input.Player.MousePosition.ReadValue<Vector2>();

        SetWeaponDirection(_mousePosition);

        if (AmmoInClip == 0 && NumberOfAmmo > 0 && !IsReloading)
        {
            StartCoroutine(Reload());
        }

        NumberOfAmmoChanged?.Invoke(AmmoInClip, AmmoLeft);
    }

    public void RecoverAmmo()
    {
        AmmoLeft = NumberOfAmmo;
    }

    private void SetWeaponDirection(Vector2 mousePosition)
    {
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 objectPosition = transform.position;

        Vector2 direction = mousePosition - objectPosition;
        direction.Normalize();

        float rotationX = transform.position.x > mousePosition.x ? 180 : 0;
        float rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(rotationX, 0, (transform.position.x > mousePosition.x ? -rotationZ : rotationZ));
    }

    protected IEnumerator Reload()
    {
        IsReloading = true;

        yield return new WaitForSeconds(ReloadTime);

        AmmoInClip = AmmoLeft > ClipCapacity ? ClipCapacity : AmmoLeft;
        AmmoLeft -= AmmoInClip;
        IsReloading = false;
    } 

    public abstract void Shoot();
}
