using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingToPoint : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private Transform _player;
    [SerializeField] private float _speed = 10f;

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
                if(_player.rotation.y >= 90f)
                {
                    _stateMoving = StateMoving.None;
                    Debug.Log("stop rotation");
                }
                break;
        }
    }

    private void RotationToTarget()
    {
        Vector3 rotation = _targetPoint.position - _player.position;
        _player.forward = Vector3.MoveTowards(_player.forward, rotation, 5 * Time.deltaTime);
        
    }
}
