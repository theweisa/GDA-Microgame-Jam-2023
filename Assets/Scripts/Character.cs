using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody2D rb;
    public float minSpeed = 5;
    public float maxSpeed = 15;
    private Vector2 prevVelocity;
    public bool impostor;
    private float speed;
    public Animator anim;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = Random.Range(minSpeed, maxSpeed);
        rb.AddForce(Random.insideUnitCircle.normalized*speed, ForceMode2D.Impulse);
        AnimatedMovement mov = Global.FindComponent<AnimatedMovement>(gameObject);
        if (mov) mov.Move();
        anim = anim ? anim : Global.FindComponent<Animator>(gameObject);
        anim.SetBool("isRobot", impostor);
        //speed = rb.velocity.magnitude;
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        // rb.velocity = Vector2.ClampMagnitude(rb.velocity, speed);
        prevVelocity = rb.velocity;
    }

    private Vector2 RandomMovement()
    {
        return new Vector2(Random.Range(-5, 5), Random.Range(-1, 1));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = Vector2.Reflect(prevVelocity, collision.contacts[0].normal);
    }
}
