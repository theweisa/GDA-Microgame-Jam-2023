using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : Character
{
    // Start is called before the first frame update
    protected override void Start()
    {
        if (GameManager.Instance.difficulty > 1) {
            SetAccessories();
        }
        this.impostor = false;
        base.Start();
    }

    void SetAccessories() {
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

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void OnDie()
    {
        base.OnDie();
    }
}
