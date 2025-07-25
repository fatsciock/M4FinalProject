using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadCannonTurret : AbstractTurret
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Transform _turret;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _rotationSpeed = 5f;

    void Start()
    {
        if (_spawnPoints == null || _spawnPoints.Length == 0)
        {
            _spawnPoints = new Transform[4];
            for (int i = 0; i < 4; i++)
            {
                _spawnPoints[i] = transform.Find($"FirePoint{i + 1}");
            }
        }
        if (_turret == null)
        {
            _turret = transform.Find("Torretta");
        }
    }

    protected override void RotateTowardsPlayer()
    {
        _turret.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
    }

    protected override void Shoot()
    {
        foreach (var cannon in _spawnPoints)
        {
            Fire(cannon.position, cannon.forward);
        }
    }

    private void Fire(Vector3 origin, Vector3 direction)
    {
        Bullet b = Instantiate(_bulletPrefab);
        b.Init(_bulletDamage, _bulletSpeed, _bulletLifeSpan);
        b.Shoot(origin, direction);
    }
}
