using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float _speed = 3;
    [SerializeField] private Vector2 _verticalAngle;
    [SerializeField] private Vector2 _horizontalAngle;

    private Vector2 _rotation = Vector2.zero;
    private const string MouseX = "Mouse X";
    private const string MouseY = "Mouse Y";


    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _rotation.y += Input.GetAxis(MouseX);
            _rotation.x += -Input.GetAxis(MouseY);
            _rotation.x = Mathf.Clamp(_rotation.x, _horizontalAngle.x, _horizontalAngle.y);
            _rotation.y = Mathf.Clamp(_rotation.y, _verticalAngle.x, _verticalAngle.y);
            transform.eulerAngles = new Vector2(_rotation.x, _rotation.y) * _speed;
        }
    }
}
