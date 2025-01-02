using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ControllerAnimation : MonoBehaviour
{
    public Transform thumStick;
    public Transform trigger;
    public Transform bumper;
    public float thumStickRange = 30f;
    public float triggerRange = 10f;
    private Vector3 startBumperPos;
    private Vector3 animBumperPos;
    private ActionBasedController ABC;


    private void Awake()
    {
        startBumperPos = bumper.localPosition;
        animBumperPos = bumper.localPosition;
        animBumperPos.x = -0.01f;
        ABC = GetComponentInParent<ActionBasedController>();
       

    }

    private void OnEnable()
    {
        ABC.selectAction.reference.action.performed += BumperAction;
        ABC.selectAction.reference.action.canceled += BumperAction;
        ABC.rotateAnchorAction.action.performed += ThumXAction;
        ABC.rotateAnchorAction.action.canceled += ThumXAction;
        ABC.translateAnchorAction.action.performed += ThumYAction;
        ABC.translateAnchorAction.action.canceled += ThumYAction;
        
    }
    private void OnDisable()
    {
        ABC.selectAction.reference.action.performed -= BumperAction;
        ABC.selectAction.reference.action.canceled -= BumperAction;
        ABC.rotateAnchorAction.action.performed -= ThumXAction;
        ABC.rotateAnchorAction.action.canceled -= ThumXAction;
        ABC.translateAnchorAction.action.performed -= ThumYAction;
        ABC.translateAnchorAction.action.canceled -= ThumYAction;
    }

    //범퍼 버튼 작동
    private void BumperAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            bumper.localPosition = animBumperPos;
        }
        else
        {
            bumper.localPosition = startBumperPos;
        }
    }
    //엄지 트리거 작동
    private void ThumXAction(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<Vector2>().x;
        Vector3 euler = thumStick.eulerAngles;
        euler.z = value * thumStickRange;
        thumStick.eulerAngles = euler;
    }
    private void ThumYAction(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<Vector2>().y;
        Vector3 euler = thumStick.eulerAngles;
        euler.x = value * thumStickRange;
        thumStick.eulerAngles= euler;
    }

}
