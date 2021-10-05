using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private Transform _targetPoint;

    private enum FiringState
    {
        None,
        Gravity,
        AntiGravity
    }

    private FiringState firingState = FiringState.None;
    private float _speed = 20f;
    private Transform _currentEnemy;
    private float _thrust = 2500f;
    private RaycastHit _hitInfo;

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 100000, Color.red);

        switch (firingState)
        {
            case FiringState.None:
                if (Input.GetMouseButton(0))
                {
                    Vector3 fwd = transform.TransformDirection(Vector3.forward);
                    
                    if (Physics.Raycast(transform.position, fwd, out _hitInfo, 1000000))
                    {
                        _currentEnemy = _hitInfo.collider.gameObject.transform;
                        if (_currentEnemy.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
                        {
                            _currentEnemy.GetComponent<Rigidbody>().useGravity = false;
                            firingState = FiringState.Gravity;
                        }
                    }

                }
                break;
            case FiringState.Gravity:
                if (Input.GetMouseButton(0) == false)
                {
                    firingState = FiringState.AntiGravity;
                }
                else
                {
                    BeamEmission(_currentEnemy, _hitInfo);
                }
                break;
            case FiringState.AntiGravity:
                PushGun();
                firingState = FiringState.None;
                break;
        }
    }

    private void PushGun()
    {
        if(_currentEnemy == null)
        {
            return;
        }

        Rigidbody body = _currentEnemy.GetComponent<Rigidbody>();
        if (body != null)
        {
            body.AddForce(transform.forward * _thrust);
            body.useGravity = true;
            _currentEnemy = null;
        }
    }

    private void BeamEmission(Transform currentEnemy, RaycastHit hitInfo)
    {
        currentEnemy.position = Vector3.MoveTowards(hitInfo.collider.gameObject.transform.position, _targetPoint.position, _speed * Time.deltaTime);
    }
}
