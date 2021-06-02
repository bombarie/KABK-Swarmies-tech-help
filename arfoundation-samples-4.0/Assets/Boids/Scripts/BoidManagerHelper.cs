﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManagerHelper : MonoBehaviour {


    private static BoidManagerHelper _instance;
    public static BoidManagerHelper instance {
        get {
            if (_instance == null) {
                _instance = GameObject.FindObjectOfType<BoidManagerHelper>();
            }
            return _instance;
        }
    }

    public enum Type {
        DEFAULT,
        TYPE_1,
        TYPE_2,
        TYPE_3
    }

    public BoidManager boidManager;
    public Spawner spawner;
    public Transform boidsTarget;

    public BoidSettings boidSettingsTight;
    public BoidSettings boidSettingsLoose;

    void Start() {
        Events.instance.AddListener<SwarmEvent>(swarmEventHandler);

        Vector3 parentScale = transform.parent.localScale;
        boidSettingsTight.scaleMultiplier = parentScale.x * 2f;
        boidSettingsLoose.scaleMultiplier = parentScale.x * 2f;

        boidManager.settings = boidSettingsLoose;
    }

    private void swarmEventHandler(SwarmEvent e) {
        Debug.Log("BoidsAddRemover >> f:swarmEventHandler >> e: " + e.ToString());
        if (e.evtType == SwarmEvent.EVENT_TYPE.ADD_BOIDS) {
            for (int i = 0; i < e.amount; i++) {
                Boid b = spawner.spawnBoid();
                boidManager.InitializeBoid(b);
            }
        }
    }

    Boid latestBoid = null;
    void Update() {
        handleKeyboardInput();

        handleTouches();
    }

    private void handleKeyboardInput() {
        if (Input.GetKeyUp(KeyCode.UpArrow)) {
            Debug.Log("[neutral] add boid");
            Boid b = spawner.spawnBoid();
            boidManager.InitializeBoid(b);
            latestBoid = b;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow)) {
            if (Input.GetKey(KeyCode.LeftShift)) {
                Debug.Log("remove latest boid");
                boidManager.RemoveBoid(latestBoid);
                latestBoid = null;
            } else {
                Debug.Log("remove boid");
                boidManager.RemoveBoid();
            }
        }

        if (Input.GetKeyUp(KeyCode.Alpha1)) {
            Debug.Log("[type1] add boid");
            addBoidType(Type.TYPE_1);
        }
        if (Input.GetKeyUp(KeyCode.Alpha2)) {
            Debug.Log("[type2] add boid");
            addBoidType(Type.TYPE_2);
        }
        if (Input.GetKeyUp(KeyCode.Alpha3)) {
            Debug.Log("[type3] add boid");
            addBoidType(Type.TYPE_3);
        }
    }

    private void handleTouches() {
        handleMouse(); // debug

        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            int layerMask;

            switch (touch.phase) {
                case TouchPhase.Began:
                    layerMask = 1 << 14; // layer 10 is the hotspotCreate layer (aka Wall)

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
                        Debug.Log("I hit a hotspotcreate wall!");

                        //boidManager.settings = boidSettingsTight;
                        Vector3 v = hit.point - (Random.Range(0.2f, .4f) * hit.normal);
                        boidsTarget.position = v;
                        BoidManagerHelper.instance.boidManager.setTarget(boidsTarget);
                        BoidManagerHelper.instance.boidManager.setSettings(BoidManagerHelper.instance.boidSettingsTight);
                        ////Transform t = Instantiate(hotspot1Prefab, v, hit.transform.rotation);
                        //Transform t = Instantiate(UIHotspotSelector.instance.getSelectedHotspotType(), v, hit.transform.rotation);
                    }
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
                    layerMask = 1 << 14; // layer 10 is the hotspotCreate layer (aka Wall)

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
                        Debug.Log("I hit a hotspotcreate wall!");

                        //boidManager.settings = boidSettingsLoose;
                        //Vector3 v = hit.point - (Random.Range(0.1f, 0.55f) * hit.normal);
                        ////Transform t = Instantiate(hotspot1Prefab, v, hit.transform.rotation);
                        //Transform t = Instantiate(UIHotspotSelector.instance.getSelectedHotspotType(), v, hit.transform.rotation);

                        BoidManagerHelper.instance.boidManager.setTarget(null);
                        BoidManagerHelper.instance.boidManager.setSettings(BoidManagerHelper.instance.boidSettingsLoose);
                    }
                    break;
            }
        }
    }

    private void handleMouse() {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layerMask;

        if (Input.GetMouseButtonDown(0)) {
            layerMask = 1 << 14; // layer 10 is the hotspotCreate layer (aka Wall)

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
                Debug.Log("BoidManagerHelper >> settings 'boidSettingsTight'");
                //boidManager.setSettings(boidSettingsTight);
                Vector3 v = hit.point - (Random.Range(0.25f, 0.5f) * hit.normal);
                boidsTarget.position = v;
                BoidManagerHelper.instance.boidManager.setTarget(boidsTarget);
                BoidManagerHelper.instance.boidManager.setSettings(BoidManagerHelper.instance.boidSettingsTight);
            }
        }
        if (Input.GetMouseButtonUp(0)) {
            layerMask = 1 << 14; // layer 10 is the hotspotCreate layer (aka Wall)

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
                Debug.Log("BoidManagerHelper >> settings 'boidSettingsLoose'");
                //boidManager.setSettings(boidSettingsLoose);
                Vector3 v = hit.point - (Random.Range(0.1f, 0.45f) * hit.normal);

                //Boid b = spawner.spawnBoid(v);
                //boidManager.InitializeBoid(b);
                BoidManagerHelper.instance.boidManager.setTarget(null);
                BoidManagerHelper.instance.boidManager.setSettings(BoidManagerHelper.instance.boidSettingsLoose);
            }
        }

    }

    public void addBoidType(Type type) {
        Boid b = spawner.spawnBoid();

        switch (type) {
            case Type.TYPE_1:
                b.SetColour(Color.red);
                b.setScale(new Vector3(2f, .5f, 2f));
                break;
            case Type.TYPE_2:
                b.SetColour(Color.yellow);
                b.setScale(1.5f);
                break;
            case Type.TYPE_3:
                b.SetColour(Color.magenta);
                b.setScale(3f);
                break;
        }

        boidManager.InitializeBoid(b);
        latestBoid = b;
    }
}