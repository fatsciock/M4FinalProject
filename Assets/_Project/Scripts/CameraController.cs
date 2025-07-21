using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _sensitivity = 1f;
    [SerializeField] private float _minY = -30f, _maxY = 60f;

    private Vector3 offset;
    private float yaw = 0f;
    private float pitch = 10f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        offset = transform.position - _player.position;
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }

    private void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * _sensitivity;
        pitch -= Input.GetAxis("Mouse Y") * _sensitivity;
        pitch = Mathf.Clamp(pitch, _minY, _maxY);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 targetPosition = _player.position + rotation * offset;

        transform.position = targetPosition;
        transform.LookAt(_player.position + Vector3.up * 1.5f);
    }
}
