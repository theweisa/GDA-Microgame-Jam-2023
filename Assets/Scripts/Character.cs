using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform accessories; 
    public float minSpeed = 1;
    public float maxSpeed = 5;
    private Vector2 prevVelocity;
    public bool impostor;
    private float speed;
    public Animator anim;
    private AnimatedMovement mov;
    // private bool pause = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = Random.Range(minSpeed, maxSpeed);
        rb.AddForce(Random.insideUnitCircle.normalized*speed, ForceMode2D.Impulse);
        mov = Global.FindComponent<AnimatedMovement>(gameObject);
        if (mov) mov.Move();
        anim = anim ? anim : Global.FindComponent<Animator>(gameObject);
        anim.SetBool("isRobot", impostor);
        //speed = rb.velocity.magnitude;
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        // if (this.pause)
        // {
        //     rb.velocity = Vector2.zero;
        //     return;
        // }

        // rb.velocity = Vector2.ClampMagnitude(rb.velocity, speed);
        CheckFlip();
        prevVelocity = rb.velocity;
    }

    private Vector2 RandomMovement()
    {
        return new Vector2(Random.Range(-5, 5), Random.Range(-1, 1));
    }

    public void Pause()
    {
        // this.pause = true;
        rb.velocity = Vector2.zero;
        // rb.bodyType = RigidbodyType2D.Static;
        mov.Stop();
    }

    virtual public void OnDie() {
        foreach (Transform acc in accessories) {
            acc.gameObject.SetActive(false);
        }
        anim.SetBool("die", true);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = Vector2.Reflect(prevVelocity, collision.contacts[0].normal);
    }

    void CheckFlip() {
        if (GetComponent<SpriteRenderer>()) GetComponent<SpriteRenderer>().flipX = rb.velocity.x > 0;
        foreach (SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>()) {
            sprite.flipX = rb.velocity.x > 0;
        }
        if (prevVelocity.x * rb.velocity.x < 0) {
            mov.Flip();
        }
    }
}
