using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MuteThisEvent : UnityEvent<bool> { }
public class RememberTheTime : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
		
	}
	
    public void link(AudioMixer m)
    {
        m.mute.AddListener(mute);
    }

    void mute(bool mute)
    {
        this.gameObject.GetComponent<AudioSource>().mute = mute;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
