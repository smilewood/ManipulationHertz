using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HTC.UnityPlugin.Vive;

public class test : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
        if(ViveInput.GetPadPressAxis(HandRole.LeftHand) != Vector2.zero)
        {
            Debug.Log("111111111111");
            Debug.Log(ViveInput.GetPadPressAxis(HandRole.LeftHand));
            Debug.Log("22222222222222");
        }
        //if (ViveInput.GetPadPressAxisEx(HandRole.LeftHand) != Vector2.zero)
        //{
        //    Debug.Log("3333333333333");
        //    Debug.Log(ViveInput.GetPadPressAxisEx(HandRole.LeftHand));
        //    Debug.Log("444444444444");
        //}

        //Vector2 padLocation = ViveInput.GetPadAxis(HandRole.LeftHand);
        //if (padLocation == Vector2.zero)
        //{
        //    return;
        //}
        //else
        //{
        //    padLocation.
        //    }
    }
}
