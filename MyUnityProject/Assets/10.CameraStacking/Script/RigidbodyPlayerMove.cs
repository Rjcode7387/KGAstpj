using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyPlayerMove : MonoBehaviour
{
    public float moveSpeed;
    public float TurnSpeed;

    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private Vector2 mousePosCache;
    private void Update()
    {
        float inputx = Input.GetAxis("Horizontal");
        float inputy = Input.GetAxis("Vertical");

        Move(inputy * Time.fixedDeltaTime *moveSpeed);
        Turn(inputx * Time.fixedDeltaTime *TurnSpeed);


    }

    private void Move(float speed)
    {
        rb.MovePosition(rb.position + (transform.forward *speed));
    }
    private void Turn(float angle)
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0, angle, 0));
    }

    
}
