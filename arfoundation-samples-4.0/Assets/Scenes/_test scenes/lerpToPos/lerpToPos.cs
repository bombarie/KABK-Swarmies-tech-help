using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lerpToPos : MonoBehaviour {

    public enum Method {
        LERP,
        SLERP,
        LINEAR
    }
    public Method method;
    public float speed;
    public Transform lerper;
    public Transform target;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        switch (method) {
            case Method.LERP:
                lerper.position = Vector3.Lerp(lerper.position, target.position, speed * Time.deltaTime);
                break;
            case Method.SLERP:
                lerper.position = Vector3.Slerp(lerper.position, target.position, speed * Time.deltaTime);
                break;
            case Method.LINEAR:
                Vector3 p = target.position - lerper.position;
                if (p.magnitude >= 1f) {
                    p = Vector3.Normalize(p);
                }
                //lerper.position += p * speed * Time.deltaTime;
                lerper.Translate(p * speed * Time.deltaTime);
                break;
        }
    }
}
