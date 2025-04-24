using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;


public class InputController : MonoBehaviour
{
    [SerializeField] private InputActionReference shotAction;
    private bool isAiming;


    private void OnEnable()
    {
        shotAction.action.started += AimStart;
        shotAction.action.canceled += Shot;
    }
    private void OnDisable()
    {
        shotAction.action.started -= AimStart;
        shotAction.action.canceled -= Shot;
    }



    public event Action OnShot;
    public event Action OnAim;




    private void AimStart(InputAction.CallbackContext context)
    {
        isAiming = true;
    }

    private void Shot(InputAction.CallbackContext context)
    {
        isAiming = false;
        OnShot?.Invoke();
    }

    private void Update()
    {
        if (isAiming)
        {
            OnAim?.Invoke();
        } 
    }
}


