using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Character
{

    [Tooltip("Number of seconds to wait before venting, in Difficulty 3")]
    public float ventTimer = 2.5f;
    private bool hasVented = false;

    public GameObject explosion;
    // Start is called before the first frame update
    protected override void Start()
    {
        this.impostor = true;
        base.Start();
        if (GameManager.Instance.difficulty >= 3) {
            SetAccessories();
        }
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (GameManager.Instance.difficulty >= 2)
        {
            if ((Time.deltaTime >= (ventTimer*1000)) && !hasVented)
            {
                Debug.Log("Vent");
                Vent();
            }
        }
    }

    public void Vent()
    {
        hasVented = true;
        StartCoroutine(VentRoutine());
    }

    IEnumerator VentRoutine()
    {
        // play vent out animation

        transform.position = GameManager.Instance.getRandomPosition();

        // play vent in animation
        yield return null;
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
