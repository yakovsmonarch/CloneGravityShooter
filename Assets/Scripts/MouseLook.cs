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
        _rotation.x = Mathf.Clamp(_rotation.x, -10f, 5f);
        _rotation.y = Mathf.Clamp(_rotation.y, -10, 10f);
        transform.eulerAngles = new Vector2(_rotation.x, _rotation.y) * _speed;
    }
}
