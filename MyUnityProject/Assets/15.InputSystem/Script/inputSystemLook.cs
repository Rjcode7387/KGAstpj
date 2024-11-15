using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class inputSystemLook : MonoBehaviour
{
    public Transform cameraRig;
    public float mouseSensivity;
    private float rigAngle = 0f;

    public InputActionAsset controlDeFine;
    InputAction lookAction;

    private void Awake()
    {
        controlDeFine = GetComponent<PlayerInput>().actions;
        lookAction = controlDeFine.FindAction("Look");
    }


    private void OnEnable()
    {
        lookAction.performed += OnLookEvent;
        lookAction.canceled += OnLookEvent;
    }

    private void OnDisable()
    {
        lookAction.performed -= OnLookEvent;
        lookAction.canceled -= OnLookEvent;
    }

    public void OnLookEvent(InputAction.CallbackContext context)
    {
        //if (false == SimpleMouseControl.isFocusing) return;
        Look(context.ReadValue<Vector2>());
       
    }
    private void OnLook(InputValue value)
    {
        print($"OnLook 호출. 값 : {value.Get<Vector2>()}");

        //if (false == SimpleMouseControl.isFocusing) return;
        //esc 누르거나 포커스 벗어나면 마웃 인식안함

        Vector2 mouseDelta = value.Get<Vector2>();

       
    }
    private void Look(Vector2 mouseDelta)
    {
        transform.Rotate(0, mouseDelta.x * mouseSensivity *Time.deltaTime, 0);
        rigAngle -= mouseDelta.y * mouseSensivity * Time.deltaTime;
        rigAngle = Mathf.Clamp(rigAngle, -90f, 90f);
        cameraRig.localEulerAngles = new Vector3(rigAngle, 0, 0);
    }
}
