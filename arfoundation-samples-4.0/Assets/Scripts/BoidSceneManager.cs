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

    private bool firstTime = true;

    [Space(10)]
    public int initialBoidsAmount = 35;

    void Start() {
        Events.instance.AddListener<SwarmEvent>(swarmEventHandler);
    }

    private void swarmEventHandler(SwarmEvent e) {
        Debug.Log("BoidSceneManager >> f:swarmEventHandler >> e: " + e.ToString());

        if (e.evtType == SwarmEvent.EVENT_TYPE.SET_POSITION) {
            target.position = e.position;

            // move immediately the first time;
            if (firstTime) {
                Events.instance.Raise(new SwarmEvent(SwarmEvent.EVENT_TYPE.ADD_BOIDS, initialBoidsAmount));
                boidScene.position = e.position;
                firstTime = false;
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
}
