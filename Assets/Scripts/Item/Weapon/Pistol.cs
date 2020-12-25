using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pistol : Weapon
{
    [SerializeField] private float _bulletSpeed;

    public override void Shoot()
    {
        if (AmmoInClip > 0)
        {
            AmmoInClip--;   

            var bullet = Instantiate(BulletTemplate, ShootPosition.position, ShootPosition.rotation);

            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.Player.MousePosition.ReadValue<Vector2>()) - transform.position;
            direction.Normalize();

            bullet.GetComponent<Rigidbody2D>().AddForce(direction * _bulletSpeed, ForceMode2D.Impulse);
        }
    }
}
