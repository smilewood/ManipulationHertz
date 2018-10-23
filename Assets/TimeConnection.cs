using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeConnection : MonoBehaviour {

    public AudioMixer mixer;

    public void connect(AudioMixer m)
    {
        mixer = m;
    }

    private void Update()
    {
        if(mixer != null)
        {
            this.gameObject.GetComponent<Timing>().rate = Vector3.Distance(this.gameObject.transform.position, mixer.gameObject.transform.position);
            mixer.toggle(this.gameObject.GetComponent<Timing>().on);
        }
    }

}
