using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation.Samples;

public class MainSceneScript : MonoBehaviour {

    public PlaceOnPlane placeOnPlaceScript;
    void Start() {

        placeOnPlaceScript.enabled = false;
        Events.instance.AddListener<GlobalEvent>(globalEventHandler);

    }

    private void globalEventHandler(GlobalEvent e) {
        Debug.Log("MainSceneScript >> f:globalEventHandler >> evt: " + e.ToString());

        if (e.evtType == GlobalEvent.EVENT_TYPE.MENU_COMPLETED) {
            placeOnPlaceScript.enabled = true;
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
