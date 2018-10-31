#define DebugCheats

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    [SerializeField] private GameObject _startCheckpoint;

	// Use this for initialization
	void Start () {
        var checkpoint = _startCheckpoint.GetComponent<CheckpointManager>();

        if (checkpoint == null)
            throw new System.Exception("The object attached to the start checkpoint field does not contain a CheckpointManager");
        CheckpointManager.ActiveCheckpoint = checkpoint;
	}

#if DebugCheats
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            FindObjectOfType<PlayerController>().transform.position = CheckpointManager.ActiveCheckpoint.transform.position;
    }
#endif
}
