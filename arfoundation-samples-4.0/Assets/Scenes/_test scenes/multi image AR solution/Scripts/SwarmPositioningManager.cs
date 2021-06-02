using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmPositioningManager : MonoBehaviour {

    private static SwarmPositioningManager _instance;
    public static SwarmPositioningManager instance {
        get {
            if (_instance == null) {
                _instance = GameObject.FindObjectOfType<SwarmPositioningManager>();
            }
            return _instance;
        }
    }

    [HideInInspector]
    public Vector3 swarmPosition;

    public void setSwarmPosition(Vector3 pos) {
        swarmPosition = pos;
    }
}
