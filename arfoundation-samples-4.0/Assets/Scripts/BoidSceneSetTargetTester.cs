using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSceneSetTargetTester : MonoBehaviour {

    private bool firstTime = true;

    [Space(10)]
    public Transform boidsTarget;

    void Start() {
    }

    void OnGUI() {
        GUILayout.BeginVertical();
        if (GUILayout.Button("randomize target pos")) {
            Debug.Log("randomize target pos");
            Events.instance.Raise(new SwarmEvent(SwarmEvent.EVENT_TYPE.SET_POSITION, Random.insideUnitSphere * 1f));

            if (firstTime) {
                // if the release of Boids was already done elsewhere we shouldn't add more
                if (BoidManagerHelper.instance.boidManager.boidCount() == 0) {
                    Events.instance.Raise(new SwarmEvent(SwarmEvent.EVENT_TYPE.ADD_BOIDS, BoidManagerHelper.instance.spawner.spawnCount));
                }
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
            BoidManagerHelper.instance.setSwarmBehavior(BoidManagerHelper.SwarmBehavior.CONTRACT_TO_TARGET);
        }
        if (GUILayout.Button("remove target")) {
            BoidManagerHelper.instance.setSwarmBehavior(BoidManagerHelper.SwarmBehavior.RELEASE_FROM_TARGET);
        }

        GUILayout.EndVertical();
    }
}
