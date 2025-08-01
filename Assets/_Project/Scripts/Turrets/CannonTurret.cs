using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTurret : AbstractTurret
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _turret;
    [SerializeField] private float _rangeOfShoot = 10f;
    [SerializeField] private float _rotationSpeed = 5f;
    private float _distanceFromPlayer = 0f;
    private Transform _playerTrasform;
    private BulletSpawner _bulletSpawner;

    void Start()
    {
        if (_spawnPoint == null)
        {
            _spawnPoint = transform.Find("FirePoint");
        }
        if (_turret == null)
        {
            _turret = transform.Find("Torretta");
        }
        if (_bulletSpawner == null)
        {
            _bulletSpawner = GetComponent<BulletSpawner>();
        }

        GameObject _player = GameObject.FindGameObjectWithTag("Player");
        if (_player != null)
        {
            _playerTrasform = _player.transform;
        }
    }

    protected override void Shoot()
    {
        _distanceFromPlayer = Vector3.Distance(_playerTrasform.position, transform.position);
        if (_distanceFromPlayer > _rangeOfShoot) return;

        Vector3 direction = (_playerTrasform.position - transform.position).normalized;
        Fire(direction);
    }

    private void Fire(Vector3 direction)
    {
        Bullet b = _bulletSpawner.GetBullet();
        b.Init(_bulletDamage, _bulletSpeed, _bulletLifeSpan);
        b.Shoot(_spawnPoint.position, direction);
    }

    protected override void RotateTowardsPlayer()
    {
        if (_playerTrasform == null) return;
        Vector3 direction = (_playerTrasform.position - _turret.position).normalized;
        direction.y = 0;

        if (direction.sqrMagnitude < 0.01f) return;

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        _turret.rotation = Quaternion.Slerp(_turret.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
    }
}
