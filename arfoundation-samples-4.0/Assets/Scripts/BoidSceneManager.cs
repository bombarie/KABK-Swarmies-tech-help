using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSceneManager : MonoBehaviour {

    public enum MoveMethod {
        LERP,
        SLERP,
        LINEAR
    }
    public MoveMethod method;

    public float lerpSpeed;

    public Transform boidScene;
    public Transform target;

    [Space(10)]
    private bool firstTime = true;
    public bool autoSpawnAtStart = true;
    public bool movesToNewPositions = true;
    public bool showDebugInfo = false;


    void Start() {
        Events.instance.AddListener<SwarmEvent>(swarmEventHandler);
        if (autoSpawnAtStart) {
            for (int i = 0; i < BoidManagerHelper.instance.spawner.spawnCount; i++) {
                Boid b = BoidManagerHelper.instance.spawner.spawnBoid();
                BoidManagerHelper.instance.boidManager.InitializeBoid(b);
            }

            BoidManagerHelper.instance.setSwarmBehavior(BoidManagerHelper.SwarmBehavior.RELEASE_FROM_TARGET);
        }
    }

    private void swarmEventHandler(SwarmEvent e) {
        Debug.Log("BoidSceneManager >> f:swarmEventHandler >> e: " + e.ToString());

        if (e.evtType == SwarmEvent.EVENT_TYPE.SET_POSITION) {
            if (movesToNewPositions) {
                target.position = e.position;

                // move immediately the first time;
                if (firstTime) {
                    if (BoidManagerHelper.instance.boidManager.boidCount() == 0) {
                        Events.instance.Raise(new SwarmEvent(SwarmEvent.EVENT_TYPE.ADD_BOIDS, BoidManagerHelper.instance.spawner.spawnCount));
                    }
                    boidScene.position = e.position;
                    firstTime = false;
                }
            }
        }
    }

    // Update is called once per frame
    void Update() {
        switch (method) {
            case MoveMethod.LERP:
                boidScene.position = Vector3.Lerp(boidScene.position, target.position, lerpSpeed * Time.deltaTime);
                break;
            case MoveMethod.SLERP:
                boidScene.position = Vector3.Slerp(boidScene.position, target.position, lerpSpeed * Time.deltaTime);
                break;
            case MoveMethod.LINEAR:
                Vector3 p = target.position - boidScene.position;
                if (p.magnitude >= 1f) {
                    p = Vector3.Normalize(p);
                }
                //lerper.position += p * speed * Time.deltaTime;
                boidScene.Translate(p * lerpSpeed * Time.deltaTime);
                break;
        }
    }

    void OnGUI() {
        if (showDebugInfo) {
            GUILayout.BeginVertical();
            GUILayout.Label("BoidSceneManager position: " + transform.position);
            GUILayout.EndVertical();
        }

    }
}
