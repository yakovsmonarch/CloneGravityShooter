using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float _speed = 3;

    private Vector2 _rotation = Vector2.zero;

    private void Update()
    {
        _rotation.y += Input.GetAxis("Mouse X");
        _rotation.x += -Input.GetAxis("Mouse Y");
        _rotation.x = Mathf.Clamp(_rotation.x, -15f, 15f);
        transform.eulerAngles = new Vector2(0, _rotation.y) * _speed;
        Camera.main.transform.localRotation = Quaternion.Euler(_rotation.x * _speed, 0, 0);
    }
}
