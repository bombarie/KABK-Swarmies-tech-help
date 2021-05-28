using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHotspots : MonoBehaviour {

    public Transform hotspot1Prefab;

    private float screenWidth;
    private float screenHeight;
    void Start() {
        screenWidth = (float)Screen.width / 2.0f;
        screenHeight = (float)Screen.height / 2.0f;
    }

    // Update is called once per frame
    void Update() {

        screenWidth = (float)Screen.width / 2.0f;
        screenHeight = (float)Screen.height / 2.0f;

        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            int layerMask;

            switch (touch.phase) {
                case TouchPhase.Began:
                    break;
                case TouchPhase.Moved:
                    /* nvm the moving
                    layerMask = 1 << 11; // layer 11 is the hotspot layer

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {

                        Vector2 pos = touch.position;
                        pos.x = (pos.x - screenWidth) / screenWidth;
                        pos.y = (pos.y - screenHeight) / screenHeight;

                        // Position the cube.
                        hit.transform.Translate(new Vector3(-touch.deltaPosition.x, touch.deltaPosition.y, 0.0f));
                        return;
                    }
                    //*/
                    break;
                case TouchPhase.Ended:
                    layerMask = 1 << 11; // layer 11 is the hotspot layer

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
                        Debug.Log("we hit a Hotspot!");
                        GameObject.Destroy(hit.transform.gameObject);
                        return;
                    }

                    layerMask = 1 << 10; // layer 10 is the hotspotCreate layer

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
                        Debug.Log("I hit a hotspotcreate wall!");

                        Vector3 v = hit.point - (Random.Range(0.1f, 0.55f) * hit.normal);
                        //Transform t = Instantiate(hotspot1Prefab, v, hit.transform.rotation);
                        Transform t = Instantiate(UIHotspotSelector.instance.getSelectedHotspotType(), v, hit.transform.rotation);
                    }
                    break;
            }

        }
    }
}
