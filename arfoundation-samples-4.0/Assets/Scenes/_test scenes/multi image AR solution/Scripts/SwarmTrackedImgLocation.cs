using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmTrackedImgLocation : MonoBehaviour {

    [HideInInspector]
    public Vector3 swarmTargetPos;

    public Transform targetIndicator;
    private Material targetIndicatorMat;
    public float updateThreshold = .01f;

    private Vector3 prevPos;
    void Start() {
        //randomColor();
        prevPos = transform.position;
    }

    void Update() {
        if (Vector3.Distance(prevPos, transform.position) > updateThreshold) {
            Events.instance.Raise(new SwarmEvent(SwarmEvent.EVENT_TYPE.SET_POSITION, targetIndicator.position));
            randomColor();
            prevPos = transform.position;
        }

        targetIndicator.rotation = Quaternion.identity;
    }

    private void randomColor() {
        if (targetIndicatorMat == null) {
            createUniqueMaterial();
        }
        targetIndicatorMat.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
    }

    private void createUniqueMaterial() {
        targetIndicator.GetComponent<Renderer>().material = new Material(targetIndicator.GetComponent<Renderer>().material);
        targetIndicatorMat = targetIndicator.GetComponent<Renderer>().material;
        Debug.Log(name + " >> SwarmTrackedImgLocation >> f:createUniqueMaterial >> targetIndicatorMat: " + targetIndicatorMat);
    }

    void OnEnable() {
        Debug.Log(name + " >> SwarmTrackedImgLocation >> f:OnEnable");
        randomColor();
    }
    void OnDisable() {
        Debug.Log(name + " >> SwarmTrackedImgLocation >> f:OnDisable");
    }

    private void OnGUI() {
        //Vector2 nativeSize = new Vector2(640, 480);
        //GUIStyle style = new GUIStyle();
        //style.fontSize = (int)(20.0f * ((float)Screen.width / (float)nativeSize.x));

        GUI.skin.label.fontSize = 25;

        GUILayout.BeginArea(new Rect(10, 10, Screen.width - 20, 500));
        GUILayout.BeginVertical();
        GUILayout.Label(name + " >> position: " + transform.position);
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}
