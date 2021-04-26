using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerObject : TrapTrigger
{
    void Update()
    {
        if (this.activationStatus)
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
