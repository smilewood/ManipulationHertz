using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLine : MonoBehaviour {
    LineRenderer line;
    TimeConnection link;
    public Material on, off;
    // Use this for initialization
    void Start () {
        link = this.gameObject.GetComponent<TimeConnection>();
	}
	
	// Update is called once per frame
	void Update () {
        if (link.mixer == null)
        {
            if (line != null)
            {
                Destroy(line);
            }
            return;
        }
        if (line == null)
        {
            line = this.gameObject.AddComponent<LineRenderer>();
            line.widthMultiplier = .1f;
        }
        line.SetPositions(new Vector3[] {
                this.gameObject.transform.position,
                link.mixer.gameObject.transform.position
            });
        line.material = this.gameObject.GetComponent<Timing>().on ? on : off;
    }
}
