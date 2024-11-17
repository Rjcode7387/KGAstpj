using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator),typeof(RigBuilder))]
public class inputSystemAction : MonoBehaviour
{
    Animator animator;
    Rig rig;
    WaitUntil untilReload;
    WaitForSeconds waitSec;

    bool isReloading;
    bool isFireing;
    bool isthrowingGrenade;

    public AnimationClip reloadClip;
    public AnimationClip fireClip;
    public AnimationClip grenadeClip;

    public InputActionAsset controlDeFine;

    InputAction reloadAction;
    InputAction fireAction;
    InputAction grenadeAction;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rig = GetComponent<RigBuilder>().layers[0].rig;
        controlDeFine = GetComponent<PlayerInput>().actions;
        reloadAction = controlDeFine.FindAction("Reload");
        fireAction = controlDeFine.FindAction("Fire");
        grenadeAction = controlDeFine.FindAction("ThrowGrenade");
    }

    private void OnEnable()
    {
        reloadAction.performed += OnReloadEvent;
        reloadAction.canceled += OnReloadEvent;
        fireAction.performed += OnFireEvent;
        fireAction.canceled += OnFireEvent;
        grenadeAction.performed += OnGrenadeEvent;
        grenadeAction.canceled += OnGrenadeEvent;
    }
    private void OnDisable()
    {
        reloadAction.performed -= OnReloadEvent;
        reloadAction.canceled -= OnReloadEvent;
        fireAction.performed -= OnFireEvent; 
        fireAction.canceled -= OnFireEvent;
        grenadeAction.performed -= OnGrenadeEvent;
        grenadeAction.canceled -= OnGrenadeEvent;
    }
    

    private IEnumerator Start()
    {
        untilReload = new WaitUntil(() => isReloading);
        waitSec  = new WaitForSeconds(reloadClip.length);
        while (true)
        {
            yield return untilReload;
            yield return waitSec;
            isReloading = false;
            rig.weight = 1f;
        }
    }

    public void OnReloadEvent(InputAction.CallbackContext context)
    {
        if (isReloading) return;
        if (context.performed)
        {            
            rig.weight =0f;
            isReloading = true;
            animator.SetTrigger("Reload");
        }
     
    }
    public void OnReloadEnd()
    {
        isReloading = false;
    }

    public void OnGrenadeEnd()
    {
        isthrowingGrenade = false;
    }

    public void OnFireEvent(InputAction.CallbackContext context)
    {
        if (isReloading || isthrowingGrenade) return;
        if (context.performed)
        {

            isFireing = true;
            animator.SetBool("IsFiring", true);
        }
        else if (context.canceled)
        { 
            isFireing = false;
            animator.SetBool("IsFiring", false);
        }
    
    }
    public void OnGrenadeEvent(InputAction.CallbackContext context)
    {
        if (isReloading || isthrowingGrenade) return;
        if (context.performed)
        {
            rig.weight =0f;
            isReloading = true;
            animator.SetTrigger("ThrowGrenade");
        }
    }
}
