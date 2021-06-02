using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace swarmies.test {

    public class CubeScript : MonoBehaviour {

        private Material mat;
        private Vector3 position;
        private float width;
        private float height;
        void Start() {
            MeshRenderer r = GetComponent<MeshRenderer>();
            if (r != null) {
                mat = new Material(r.material);
                r.material = mat;
            }


            width = (float)Screen.width / 2.0f;
            height = (float)Screen.height / 2.0f;

            // Position used for the cube.
            position = new Vector3(0.0f, 0.0f, 0.0f);

        }

        // Update is called once per frame
        void Update() {
            /*
            if (Input.GetMouseButtonUp(0)) {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit)) {
                    Transform objectHit = hit.transform;
                    MeshRenderer r = objectHit.GetComponent<MeshRenderer>();
                    if (r != null) {
                        r.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
                    }
                }
            }
            */

            // src: https://docs.unity3d.com/ScriptReference/Input.GetTouch.html
            for (int i = 0; i < Input.touchCount; i++) {
                Touch touch = Input.GetTouch(i);

                if (touch.phase == TouchPhase.Began) {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);

                    if (Physics.Raycast(ray)) {
                        if (Physics.Raycast(ray, out hit)) {
                            Transform objectHit = hit.transform;
                            MeshRenderer r = objectHit.GetComponent<MeshRenderer>();
                            if (r != null) {
                                r.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
                            }
                        }
                    }
                }
            }
        }
    }
}
