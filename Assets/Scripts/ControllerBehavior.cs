using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using HTC.UnityPlugin.Vive;

public class ControllerBehavior : MonoBehaviour {

    // Update is called once per frame
    void Update () {
        // get trigger axis value         
        if (ViveInput.GetAxisEx(HandRole.RightHand, ControllerAxis.Trigger) > 0.5f)
        {
            Debug.Log("SFKPJGOIKFJGKLSDJKLGD");
        }
       

    }
}
