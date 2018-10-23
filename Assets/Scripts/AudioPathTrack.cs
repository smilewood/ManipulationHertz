using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPathTrack : MonoBehaviour {

    List<GameObject> path;

	// Use this for initialization
	void Awake () {
        path = new List<GameObject>();
	}
	
    public void addStep(GameObject s)
    {
        path.Add(s);
    }

    public GameObject cutPath(GameObject remove)
    {
        if (path.Count == 0)
        {
            Destroy(this.gameObject);
            return null;
        }
        int i = path.IndexOf(remove);
        path = path.GetRange(0, i);
        return path.Count > 0 ? path[path.Count-1] : null;
    }
}
