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
    private bool stopped = false;
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
        if (!stopped) CheckFlip(rb.velocity.x > 0);
        prevVelocity = rb.velocity;
    }

    protected virtual void SetAccessories() {
        int randIndex = Random.Range(0, accessories.childCount-1);
        int i = 0;
        foreach (Transform acc in accessories) {
            if (i == randIndex) {
                acc.gameObject.SetActive(true);
            }
            else {
                acc.gameObject.SetActive(false);
            }
            i++;
        }
    }

    private Vector2 RandomMovement()
    {
        return new Vector2(Random.Range(-5, 5), Random.Range(-1, 1));
    }

    public void Pause()
    {
        stopped = true;
        // this.pause = true;
        rb.velocity = Vector2.zero;
        // rb.bodyType = RigidbodyType2D.Static;
        mov.Stop();
    }

    virtual public void OnDie() {
        GameManager.Instance.deadChar = this;
        foreach (Transform acc in accessories) {
            acc.gameObject.SetActive(false);
        }
        stopped = true;
        anim.SetBool("die", true);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = Vector2.Reflect(prevVelocity, collision.contacts[0].normal);
    }

    public void CheckFlip(bool xFlip) {
        bool prev = false;
        if (GetComponent<SpriteRenderer>()) GetComponent<SpriteRenderer>().flipX = xFlip;
        foreach (SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>()) {
            prev = sprite.flipX;
            sprite.flipX = xFlip;
        }
        if (prev != xFlip) {
            Debug.Log("flip bro cmon");
            mov.Flip();
        }
    }
}
