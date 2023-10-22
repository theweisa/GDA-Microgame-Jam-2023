using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cursor : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private float maxSpeed = 2, acceleration = 50, deceleration = 100;
    private float currentSpeed = 0;

    private Vector2 movementInput;

    [SerializeField]
    private InputActionReference movement, select;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movementInput = movement.action.ReadValue<Vector2>();
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (movementInput.magnitude > 0 && currentSpeed >= 0)
        {
            currentSpeed += acceleration * maxSpeed * Time.deltaTime;
        }
        else
        {
            currentSpeed -= deceleration * maxSpeed * Time.deltaTime;
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        rb.velocity = movementInput * currentSpeed;
    }
}
