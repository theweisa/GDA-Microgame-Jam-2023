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
