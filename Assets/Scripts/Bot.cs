using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Character
{
    public GameObject explosion;
    // Start is called before the first frame update
    protected override void Start()
    {
        this.impostor = true;
        base.Start();
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    override public void OnDie() {
        StartCoroutine(Explode());
        base.OnDie();
    }
    IEnumerator Explode() {
        CameraManager.Instance.StartShake(10, 0.5f, 5);
        GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Destroy(exp);
    }
}
