using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timing : MonoBehaviour {
    float time;
    public bool on = false;
	public float rate = 1.0f;
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime * (rate/2);
        if (time > 1)
        {
            time = 0f;
            on = !on;
        }
	}
}
