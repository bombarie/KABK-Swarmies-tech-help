using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHotspotSelector : MonoBehaviour {
    public Transform hotspotGoodPrefab;
    public Transform hotspotBadPrefab;

    public Text selectedHotspotValue;

    public enum SelectedHotspotType {
        GOOD,
        BAD
    }
    public SelectedHotspotType selectedHotspotType;

    private static UIHotspotSelector _instance;
    public static UIHotspotSelector instance {
        get {
            if (_instance == null) {
                _instance = GameObject.FindObjectOfType<UIHotspotSelector>();
            }

            return _instance;
        }
    }

    void Start() {
        selectGood();
    }

    void Update() {

    }

    public Transform getSelectedHotspotType() {
        switch (selectedHotspotType) {
            case SelectedHotspotType.GOOD:
                return hotspotGoodPrefab;
            case SelectedHotspotType.BAD:
                return hotspotBadPrefab;
        }
        return hotspotBadPrefab;
    }

    public void selectGood() {
        Debug.Log("good");
        selectedHotspotType = SelectedHotspotType.GOOD;
        if (selectedHotspotValue != null) {
            selectedHotspotValue.text = "Good";
        }
    }
    public void selectBad() {
        Debug.Log("bad");
        selectedHotspotType = SelectedHotspotType.BAD;
        if (selectedHotspotValue != null) {
            selectedHotspotValue.text = "Bad";
        }
    }
}
