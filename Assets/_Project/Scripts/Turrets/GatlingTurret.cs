using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingTurret : AbstractTurret
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Transform _turret;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _rangeOfShoot = 10f;
    [SerializeField] private float _rotationSpeed = 5f;
    private float _distanceFromPlayer = 0f;
    private Transform _playerTrasform;

    // Start is called before the first frame update
    void Start()
    {
        if (_spawnPoints == null || _spawnPoints.Length == 0)
        {
            _spawnPoints = new Transform[6];
            for (int i = 0; i < 6; i++)
            {
                _spawnPoints[i] = transform.Find($"FirePoint{i + 1}");
            }
        }
        if (_turret == null)
        {
            _turret = transform.Find("Torretta");
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

        foreach (var cannon in _spawnPoints)
        {
            Fire(cannon.position, cannon.forward);
        }
    }

    private void Fire(Vector3 origin, Vector3 direction)
    {
        float spreadAngle = 3f;
        Vector2 randomSpread = Random.insideUnitCircle * Mathf.Tan(spreadAngle * Mathf.Deg2Rad);
        Vector3 deviatedDirection = (direction
        + _turret.right * randomSpread.x
        + _turret.up * randomSpread.y).normalized;

        Bullet b = Instantiate(_bulletPrefab);
        b.Init(_bulletDamage, _bulletSpeed, _bulletLifeSpan);
        b.Shoot(origin, deviatedDirection);
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
