using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (this.gameObject.transform.position.y < 0)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, 0, this.gameObject.transform.position.z);
        }
    }
}
