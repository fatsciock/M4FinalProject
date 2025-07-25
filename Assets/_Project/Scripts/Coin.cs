using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 90f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Qui potresti notificare un GameManager, ad esempio:
            // GameManager.Instance.AddCoins(_value);

            // Distruggi la moneta
            Destroy(gameObject);
        }
    }
}
