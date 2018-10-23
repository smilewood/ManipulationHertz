using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faceCamera : MonoBehaviour {
    public GameObject camera;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (camera == null)
            return;
        var fwd = camera.transform.forward;
        fwd.y = 0.0f;
        transform.rotation = Quaternion.LookRotation(fwd);
    }
}
