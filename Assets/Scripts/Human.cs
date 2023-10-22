using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : Character
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        this.impostor = false;
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
