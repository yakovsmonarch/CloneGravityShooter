using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingToPoint : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private Transform _player;
    [SerializeField] private float _speed = 5.0f;

    private Transform _targetPoint;
    private int _currentPoint = 0;
    private enum StateMoving { None, Move, Rotation}
    private StateMoving _stateMoving = StateMoving.Move;

    private void Start()
    {
        _targetPoint = _points[_currentPoint];
    }

    private void Update()
    {
        switch (_stateMoving)
        {
            case StateMoving.None:
                _stateMoving = StateMoving.Move;
                enabled = false;
                break;
            case StateMoving.Move:
                _player.position = Vector3.MoveTowards(_player.position, _targetPoint.position, _speed * Time.deltaTime);
                if(_player.position == _targetPoint.position)
                {
                    _targetPoint = _points[++_currentPoint];
                    _stateMoving = StateMoving.Rotation;
                }
                break;
            case StateMoving.Rotation:
                RotationToTarget();
                
                //_stateMoving = StateMoving.None;
                break;
        }
    }

    private void RotationToTarget()
    {
        Vector3 rotation = _targetPoint.position - _player.position;
        Vector3 newDirection = Vector3.RotateTowards(_player.forward, rotation, _speed * Time.deltaTime, 0.0f);
        _player.rotation = Quaternion.LookRotation(newDirection);

        Debug.Log($"{newDirection.x}, {newDirection.y}, {newDirection.z}");
    }
}
