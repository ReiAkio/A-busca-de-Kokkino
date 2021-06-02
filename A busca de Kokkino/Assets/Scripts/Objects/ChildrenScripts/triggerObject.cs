using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class triggerObject : TrapTrigger
{
    void Update()
    {
        var spriteR = this.GetComponent<SpriteRenderer>();
        var spriteC = this.GetComponent<Collider2D>();
        var tileR = this.GetComponent<TilemapRenderer>();
        var tileC = this.GetComponent<TilemapCollider2D>();
        
        if (this.activationStatus)
        {
            if (spriteR)
            {
                spriteR.enabled = false;
                spriteC.enabled = false;
            }

            if (tileR)
            {
                tileR.enabled = false;
                tileC.enabled = false;
            }

        }
        else
        {
            if (spriteR)
            {
                spriteR.enabled = true;
                spriteC.enabled = true;
            }

            if (tileR)
            {
                tileR.enabled = true;
                tileC.enabled = true;
            }
        }
    }
}
