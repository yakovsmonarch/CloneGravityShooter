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
                if(Input.GetMouseButton(0) == false)
                {
                    firingState = FiringState.AntiGravity;
                }
                break;
            case FiringState.AntiGravity:
                Debug.Log("Shoot");
                firingState = FiringState.None;
                break;
        }
    }
}
