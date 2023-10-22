using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Character
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        this.impostor = true;
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
