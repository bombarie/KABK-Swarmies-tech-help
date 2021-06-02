using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSceneSetTargetTester : MonoBehaviour {

    private bool firstTime = true;

    [Space(10)]
    public Transform boidsTarget;

    void Start() {
        Application.targetFrameRate = 30;
    }

    void Update() {

    }

    void OnGUI() {
        GUILayout.BeginVertical();
        if (GUILayout.Button("randomize target pos")) {
            Debug.Log("randomize target pos");
            Events.instance.Raise(new SwarmEvent(SwarmEvent.EVENT_TYPE.SET_POSITION, Random.insideUnitSphere * 1f));

            if (firstTime) {
                //Events.instance.Raise(new SwarmEvent(SwarmEvent.EVENT_TYPE.ADD_BOIDS, 30));
                firstTime = false;
            }
        }
        GUILayout.Space(10);
        if (GUILayout.Button("boid settings loose")) {
            BoidManagerHelper.instance.boidManager.setSettings(BoidManagerHelper.instance.boidSettingsLoose);
        }
        if (GUILayout.Button("boid settings tight")) {
            BoidManagerHelper.instance.boidManager.setSettings(BoidManagerHelper.instance.boidSettingsTight);
        }
        GUILayout.Space(10);
        if (GUILayout.Button("set target")) {
            BoidManagerHelper.instance.boidManager.setTarget(boidsTarget);
            BoidManagerHelper.instance.boidManager.setSettings(BoidManagerHelper.instance.boidSettingsTight);
        }
        if (GUILayout.Button("remove target")) {
            BoidManagerHelper.instance.boidManager.setTarget(null);
            BoidManagerHelper.instance.boidManager.setSettings(BoidManagerHelper.instance.boidSettingsLoose);
        }

        GUILayout.EndVertical();
    }
}
