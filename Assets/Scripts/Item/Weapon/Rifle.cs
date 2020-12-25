using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    [SerializeField] private float _bulletSpeed;

    private bool IsShooting;

    public override void Shoot()
    {
        if (AmmoInClip > 0 && !IsShooting)
        {
            StartCoroutine(InstantiateBullets());
        }
    }

    private IEnumerator InstantiateBullets()
    {
        IsShooting = true;

        for (int i = 0; i < 3; i++)
        {
            AmmoInClip--;

            var bullet = Instantiate(BulletTemplate, ShootPosition.position, ShootPosition.rotation);

            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.Player.MousePosition.ReadValue<Vector2>()) - transform.position;
            direction.Normalize();

            bullet.GetComponent<Rigidbody2D>().AddForce(direction * _bulletSpeed, ForceMode2D.Impulse);

            yield return new WaitForSeconds(0.1f);
        }

        IsShooting = false;
    }
}
