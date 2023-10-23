using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Character
{

    public float ventTimer = 2.5f;

    public GameObject explosion;
    // Start is called before the first frame update
    protected override void Start()
    {
        this.impostor = true;
        base.Start();
        if (GameManager.Instance.difficulty >= 3) {
            SetAccessories();
            StartCoroutine(VentRoutine());
        }
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    IEnumerator VentRoutine()
    {
        yield return new WaitForSeconds(ventTimer);
        // play vent out animation
        anim.SetBool("vent", true);
        mov.Stop();

        yield return new WaitForSeconds(1f);
        transform.position = GameManager.Instance.getRandomPosition();

        // play vent in animation
        anim.SetBool("vent", false);
        
        yield return new WaitForSeconds(1f);
        mov.Move();
    }

    override public void OnDie()
    {
        StartCoroutine(Explode());
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
        CameraManager.Instance.StartShake(10, 0.5f, 5);
        GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Destroy(exp);
    }
}
