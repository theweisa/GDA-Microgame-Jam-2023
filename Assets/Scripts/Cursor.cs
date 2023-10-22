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

    private List<Character> overlappedCharacters = new List<Character>();

    // Start is called before the first frame update
    void Start()
    {
        select.action.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        movementInput = movement.action.ReadValue<Vector2>();
        if (select.action.triggered)
        {
            Debug.Log("action performed");
            OnSelect();
        }
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

    void OnSelect()
    {
        Debug.Log("Pressed Select");

        if (overlappedCharacters.Count > 0)
        {
            foreach(Character character in overlappedCharacters)
            {
                if (character.impostor == true)
                {
                    GameManager.Instance.Win();
                    return;
                }
            }

            GameManager.Instance.Lose();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Character c = collider.GetComponent<Character>();
        if (c)
        {
            overlappedCharacters.Add(c);
            Debug.Log("added char");
        }
        else
        {
            Debug.Log("Not character");
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        Character c = collider.GetComponent<Character>();
        if (c)
        {
            overlappedCharacters.Remove(c);
            Debug.Log("Removed char");
        }
        else
        {
            Debug.Log("Not character");
        }
    }
}
