using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class NewBehaviourScript : MonoBehaviour
{
    public float moveSpeed;
    public float turnSpeed;
    
   

    private CharacterController cc;
    private Vector3 moveDir;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float inputx = Input.GetAxis("Horizontal");
        float inputy = Input.GetAxis("Vertical");

        Move(inputy * moveSpeed * Time.deltaTime);
        Turn(inputx * turnSpeed * Time.deltaTime);


    }

    private void Move(float speed)
    {
        moveDir= transform.forward *speed;
        moveDir.y -=  9.81f *Time.deltaTime;
        cc.Move(moveDir);
    }
    private void Turn(float angle)
    {
        transform.Rotate(Vector3.up * angle);
    }

}
