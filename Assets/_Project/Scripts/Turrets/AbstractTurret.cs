using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTurret : MonoBehaviour
{
    [SerializeField] protected float _shotInterval = 0.5f;
    [SerializeField] protected int _bulletDamage = 1;
    [SerializeField] protected float _bulletSpeed = 5f;
    [SerializeField] protected float _bulletLifeSpan = 5;

    protected float _lastShotTime = 0;

    void Update()
    {
        RotateTowardsPlayer();
        if (!CanShoot()) return;

        Shoot();
        _lastShotTime = Time.time;
    }

    protected abstract void Shoot();

    protected abstract void RotateTowardsPlayer();

    protected virtual bool CanShoot()
    {
        return Time.time - _lastShotTime >= _shotInterval;
    }
}
