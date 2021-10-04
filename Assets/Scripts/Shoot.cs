using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private enum FiringState
    {
        None,
        Gravity,
        AntiGravity
    }

    private FiringState firingState = FiringState.None;
    private float _speed = 5f;
    private Transform _currentEnemy;

    private void Update()
    {
        switch (firingState)
        {
            case FiringState.None:
                if (Input.GetMouseButtonDown(0))
                {
                    firingState = FiringState.Gravity;
                }
                break;
            case FiringState.Gravity:
                if (Input.GetMouseButton(0) == false)
                {
                    firingState = FiringState.AntiGravity;
                }
                else
                {
                    BeamEmission();
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
        
    }

    private void BeamEmission()
    {
        RaycastHit hitInfo;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, transform.forward * 10, Color.red);

        if(Physics.Raycast(transform.position, fwd, out hitInfo, 100))
        {
            _currentEnemy = hitInfo.collider.gameObject.transform;
            
            if (_currentEnemy.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            {
                _currentEnemy.position = Vector3.MoveTowards(hitInfo.collider.gameObject.transform.position, transform.position, _speed * Time.deltaTime);
            }
        }
    }
}
