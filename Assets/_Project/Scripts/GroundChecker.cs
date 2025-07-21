using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public enum CHECK_TYPE { RAYCAST, CHECK_SPHERE };

    [SerializeField] private float _maxDistance = 0.05f;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private CHECK_TYPE _checkType = CHECK_TYPE.RAYCAST;

    public bool IsGrounded { get; private set; }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        switch (_checkType)
        {
            case CHECK_TYPE.RAYCAST:
                Gizmos.DrawLine(transform.position, transform.position - Vector3.up * _maxDistance);
                break;
            case CHECK_TYPE.CHECK_SPHERE:
                Gizmos.DrawWireSphere(transform.position, _maxDistance);
                break;
        }
    }

    void Update()
    {
        switch(_checkType)
        {
            case CHECK_TYPE.RAYCAST:
                IsGrounded = Physics.Raycast(transform.position, -Vector3.up, _maxDistance, _whatIsGround);
                break;
            case CHECK_TYPE.CHECK_SPHERE:
                IsGrounded = Physics.CheckSphere(transform.position, _maxDistance, _whatIsGround);
                break;
        }
    }
}
