using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(CharacterController),typeof(Animator),typeof(PlayerInput))]
public class InputSystemMove : MonoBehaviour
{
    CharacterController charCtrl;
    Animator animator;

    public float walkSpeed;
    public float runSpeed;

    Vector2 inputValue;

    public InputActionAsset controlDeFine;

    InputAction moveAction;

    private void Awake()
    {
        charCtrl = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        controlDeFine = GetComponent<PlayerInput>().actions;
        moveAction = controlDeFine.FindAction("Move");
    }

    private void OnEnable()
    {
        moveAction.performed += OnMoveEvent;
        moveAction.canceled += OnMoveEvent;
    }
    private void OnDisable()
    {
        moveAction.performed -= OnMoveEvent;
        moveAction.canceled -= OnMoveEvent;
    }
    public void OnMoveEvent(InputAction.CallbackContext context)
    {
        print($"OnMoveEvent »£√‚ µ  . context : {context.ReadValue<Vector2>()}");
        inputValue = context.ReadValue<Vector2>();
    }

    private void OnMove(InputValue value)
    {
        //value.isPressed

        inputValue = value.Get<Vector2>();
        print($"OnMove »£√‚ µ  . ∆ƒ∂ÛπÃ≈Õ : {inputValue}");     
    }

    private void Update()
    {
        Vector3 inputmoveDir = new Vector3(inputValue.x, 0, inputValue.y)*walkSpeed;
        Vector3 actualmoveDir = transform.TransformDirection(inputmoveDir);

        charCtrl.Move(actualmoveDir*Time.deltaTime);

        animator.SetFloat("Xdir", inputValue.x);
        animator.SetFloat("Ydir", inputValue.y);
        animator.SetFloat("Speed", inputValue.magnitude);
    }
}
