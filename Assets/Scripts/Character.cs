using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 prevVelocity;
    public bool impostor;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = RandomMovement();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        prevVelocity = rb.velocity;
    }

    private Vector2 RandomMovement()
    {
        return new Vector2(Random.Range(-5, 5), Random.Range(-1, 1));
    }

    public bool OnClick() {
        if (this.impostor)
        {
            return true;
        }

        return false;
    }

     void OnCollisionEnter2D(Collision2D collision)
     {
        rb.velocity = Vector2.Reflect(prevVelocity, collision.contacts[0].normal);
     }

    // public void OnCollisionEnter(Collision collision)
    // {
    //     this.velocity = this.velocity(-this.velocity.x, -this.velocity.y);
    // }
}
