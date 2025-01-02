using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class FireTest : MonoBehaviour
{
    private ActionBasedController ABC;
    public Transform trigger;
    private Vector3 startTriggerPos;
    private Vector3 animTriggerPos;
    public LaunchProjectile LaunchProjectile;

    private void Awake()
    {
        ABC = GetComponentInParent<ActionBasedController>();
        startTriggerPos = trigger.localPosition;
        animTriggerPos = trigger.localPosition;
        animTriggerPos.z = -0.01f;
    }

    private void OnEnable()
    {
        ABC.activateAction.reference.action.performed += TriggerAction;
        ABC.activateAction.reference.action.canceled += TriggerAction;
    }

    private void OnDisable()
    {
        ABC.activateAction.reference.action.performed -= TriggerAction;
        ABC.activateAction.reference.action.canceled -= TriggerAction;
    }

    private void TriggerAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            trigger.localPosition = animTriggerPos;
            LaunchProjectile.Fire();
        }
        else
        {
            trigger.localPosition = startTriggerPos;
        }
    }
    private void Fire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            LaunchProjectile.Fire();
        }
    }

}
