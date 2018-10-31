using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TopContactHandlerBase : MonoBehaviour{
    abstract public void ContactUp(MonoBehaviour collider);
}
