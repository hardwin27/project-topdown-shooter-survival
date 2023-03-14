using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpreadGun : WeaponGun
{
    [SerializeField] protected int _spreadAmount;
    [SerializeField] protected float _spreadAngle;

    protected override void Update()
    {
        base.Update();
    }

    protected override void AutoShoot()
    {
        base.AutoShoot();
    }

    protected override void ShootIntervalTimerHandler()
    {
        base.ShootIntervalTimerHandler();
    }

    protected override void Shoot()
    {
        if (_shootIntervalTimer > 0f)
        {
            return;
        }

        for (int projectileInd = 0; projectileInd < _spreadAmount; projectileInd++)
        {
            float launchAngle = projectileInd * _spreadAngle;
            if ((projectileInd + 1) % 2 == 1 && projectileInd > 0)
            {
                launchAngle -= _spreadAngle;
                launchAngle *= -1;
            }

            launchAngle += transform.rotation.eulerAngles.z;

            GameObject projectileObject = Instantiate(_projectilePrefab, transform.position, Quaternion.Euler(0f, 0f, launchAngle));
            Vector2 launchDirection = (Vector2)(Quaternion.Euler(0f, 0f, launchAngle) * Vector2.right);
            
            if (projectileObject.TryGetComponent(out Projectile projectile))
            {
                projectile.HitBox.HitResponder = OwnerHitResponder;
                projectile.ProjectileDamage = OwnerHitResponder.Damage;
                projectile.Body.AddForce(launchDirection * _projectileForce, ForceMode2D.Impulse);
            }
        }

        _shootIntervalTimer = _shootInterval;
    }
}
