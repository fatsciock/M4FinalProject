using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int _damage;
    private float _speed;
    private float _lifeSpan;
    private Rigidbody _rb;
    private BulletSpawner _bulletSpawner;

    public void SetBulletSpawner(BulletSpawner spawner)
    {
        _bulletSpawner = spawner;
    }

    public void Init(int damage, float speed, float lifeSpan)
    {
        _damage = damage;
        _speed = speed;
        _lifeSpan = lifeSpan;

        _rb = GetComponent<Rigidbody>();
        if (_rb == null)
        {
            Debug.LogError("Rigidbody component is missing on the Bullet GameObject.");
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.layer == 11)
        {
            ReturnToPool();
        }
    }

    public void ReturnToPool()
    {
        if (!gameObject.activeInHierarchy) return;
        _bulletSpawner.ReleaseBullet(this);
    }

    public void Shoot(Vector3 origin, Vector3 direction)
    {
        transform.position = origin;
        _rb.velocity = direction.normalized * _speed;

        Invoke(nameof(ReturnToPool), _lifeSpan);
    }
}
