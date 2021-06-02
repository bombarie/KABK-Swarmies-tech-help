using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SwarmiesMainscript : MonoBehaviour {

    public ARTrackedImageManager trackedImgManager;

    public int targetFrameRate = 30;

    void Awake() {
        trackedImgManager.enabled = false;
    }

    void Start() {
        Application.targetFrameRate = targetFrameRate;

        Events.instance.AddListener<GlobalEvent>(globalEventHandler);
    }

    private void globalEventHandler(GlobalEvent e) {
        if (e.evtType == GlobalEvent.EVENT_TYPE.MENU_COMPLETED) {
            trackedImgManager.enabled = true;
        }
    }


    void Update() {
    }

}
