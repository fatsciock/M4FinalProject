using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    private readonly Queue<Bullet> _bulletPool = new Queue<Bullet>();

    public Bullet GetBullet()
    {
        Bullet bullet;
        if (_bulletPool.Count > 0)
        {
            bullet = _bulletPool.Dequeue();
            bullet.gameObject.SetActive(true);
        }
        else
        {
            bullet = Instantiate(_bulletPrefab);
            bullet.SetBulletSpawner(this);
        }
        return bullet;
    }

    public void ReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        _bulletPool.Enqueue(bullet);
    }
}
