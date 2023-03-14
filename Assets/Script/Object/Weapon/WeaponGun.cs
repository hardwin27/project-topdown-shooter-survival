using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGun : Weapon
{
    [SerializeField] protected GameObject _projectilePrefab;
    [SerializeField] protected float _projectileForce;
    [SerializeField] protected float _shootInterval;
    [SerializeField] protected float _shootIntervalTimer = 0f;
    [SerializeField] protected float _maxAmmo;
    [SerializeField] protected float _currentAmmo;

    protected override void Update()
    {
        base.Update();
        AutoShoot();
        ShootIntervalTimerHandler();
    }

    protected virtual void AutoShoot()
    {
        if (_isOnAction)
        {
            Shoot();
        }
    }

    protected virtual void ShootIntervalTimerHandler()
    {
        if (_shootIntervalTimer > 0f)
        {
            _shootIntervalTimer -= Time.deltaTime;
        }
    }

    public override void ActionStart()
    {
        base.ActionStart();
    }

    public override void ActionEnd()
    {
        base.ActionEnd();
    }

    protected virtual void Shoot()
    {
        if (_shootIntervalTimer > 0f)
        {
            return;
        }

        GameObject projectileObject = Instantiate(_projectilePrefab, transform.position, transform.rotation);
        if (projectileObject.TryGetComponent(out Projectile projectile))
        {
            projectile.HitBox.HitResponder = OwnerHitResponder;
            projectile.Body.AddForce(_projectileForce * transform.right, ForceMode2D.Impulse);
            projectile.ProjectileDamage = OwnerHitResponder.Damage;
        }

        _shootIntervalTimer = _shootInterval;
    }
}
