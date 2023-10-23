using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Character
{

    public float ventTimer = 3f;
    public float ventOffset = 0.5f;
    public SpriteRenderer shadow;

    public GameObject explosion;
    private RigidbodyConstraints2D rbConstraints;
    // Start is called before the first frame update
    protected override void Start()
    {
        this.impostor = true;
        base.Start();
        if (GameManager.Instance.difficulty >= 3) {
            SetAccessories();
            StartCoroutine(VentRoutine());
        }
        rbConstraints = rb.constraints;
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!canSelect) {
            rb.velocity = Vector2.zero;
        }
    }

    IEnumerator VentRoutine()
    {
        yield return new WaitForSeconds(ventTimer+Random.Range(-ventOffset, ventOffset));
        yield return Vent();
    }

    public IEnumerator Vent(bool unvent=true) {
        Debug.Log("vent!");
        canSelect = false;
        AudioManager.Instance.PlaySound("Vent");
        // play vent out animation
        anim.SetBool("vent", true);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        Pause();
        Vector3 shadowScale = shadow.transform.localScale;
        LeanTween.scale(shadow.gameObject, shadowScale*1.15f, 0.1f).setEaseOutExpo().setOnComplete(()=>{
            LeanTween.scale(shadow.gameObject, Vector3.zero, 0.4f).setEaseInExpo();
        });
        yield return new WaitForSeconds(0.4f);
        AudioManager.Instance.PlaySound("vent");
        yield return new WaitForSeconds(0.6f);
        if (unvent) {
            transform.position = GameManager.Instance.getRandomPosition();
            canSelect = true;
            //yield return new WaitForSeconds(0.3f);
            // play vent in animation
            anim.SetBool("vent", false);
            LeanTween.scale(shadow.gameObject, shadowScale, 0.5f).setEaseOutExpo().setDelay(0.3f);
            //sprite.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.4f);
            AudioManager.Instance.PlaySound("vent");
            yield return new WaitForSeconds(0.6f);
            rb.constraints = rbConstraints;
            if (!GameManager.Instance.gameOver)
                Resume();
            else {
                yield return Vent(false);
            }
        }
        else {
            gameObject.SetActive(false);
        }
    }

    override public void OnDie()
    {
        StopAllCoroutines();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        StartCoroutine(Explode());
        canSelect = false;
        base.OnDie();
    }
    protected override void SetAccessories() {
        int randIndex = Random.Range(1, accessories.childCount-1);
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
    IEnumerator Explode() {
        shadow.gameObject.SetActive(false);
        AudioManager.Instance.PlaySound("Explosion");
        CameraManager.Instance.StartShake(10, 0.5f, 5);
        GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);
        AudioManager.Instance.PlaySound("Explosion");
        yield return new WaitForSeconds(0.5f);
        Destroy(exp);
    }
}
