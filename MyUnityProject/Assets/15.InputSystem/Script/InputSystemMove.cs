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
    public float smoothTime;

    Vector2 inputValue;
    Vector2 smoothValue;
    Vector2 currentVelocity;

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
        print($"OnMoveEvent ȣ�� �� . context : {context.ReadValue<Vector2>()}");
        inputValue = context.ReadValue<Vector2>();
    }

    private void OnMove(InputValue value)
    {
        //value.isPressed

        inputValue = value.Get<Vector2>();
        print($"OnMove ȣ�� �� . �Ķ���� : {inputValue}");     
    }

    private void Update()
    {
        // �ε巯�� �̵��� ���
        smoothValue = Vector2.SmoothDamp(
            smoothValue,           // ���� ��
            inputValue,           // ��ǥ ��
            ref currentVelocity,  // ���� �ӵ�
            smoothTime            // ���� �ð�
        );

        Vector3 smoothMoveDir = new Vector3(smoothValue.x, 0, smoothValue.y) * walkSpeed;
        Vector3 actualmoveDir = transform.TransformDirection(smoothMoveDir);

        charCtrl.Move(actualmoveDir*Time.deltaTime);

        animator.SetFloat("Xdir", inputValue.x);
        animator.SetFloat("Ydir", inputValue.y);
        animator.SetFloat("Speed", inputValue.magnitude);
    }
}
