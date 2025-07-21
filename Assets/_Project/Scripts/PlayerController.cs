using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 4;
    [SerializeField] private float _jumpHeight = 5;
    private Rigidbody _rb;
    private GroundChecker _groundChecker;
    private Camera _mainCamera;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        if ( _groundChecker == null ) _groundChecker = GetComponentInChildren<GroundChecker>();
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 camForward = _mainCamera.transform.forward;
        Vector3 camRight = _mainCamera.transform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDirection = camForward * v + camRight * h;

        if (moveDirection.sqrMagnitude > 0.05f)
        {
            transform.forward = moveDirection;
        }

        Vector3 velocity = _rb.velocity;
        velocity.x = moveDirection.x * _speed;
        velocity.z = moveDirection.z * _speed;

        if (Input.GetButtonDown("Jump") && _groundChecker.IsGrounded)
        {
            velocity.y = Mathf.Sqrt(_jumpHeight * -1 * Physics.gravity.y);

        }

        _rb.velocity = velocity;
    }
}
