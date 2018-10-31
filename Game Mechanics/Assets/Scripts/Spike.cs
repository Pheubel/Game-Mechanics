using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : TopContactHandlerBase
{

    public override void ContactUp(MonoBehaviour collider)
    {
        var player = collider as PlayerController;

        if (player == null) return;

        
    }
}
