using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour {

    public static CheckpointManager ActiveCheckpoint;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag(Helper.Tags.Player))
        {
            ActiveCheckpoint = this;
        }
    }
}
