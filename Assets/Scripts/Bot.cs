using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Character
{
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
}
