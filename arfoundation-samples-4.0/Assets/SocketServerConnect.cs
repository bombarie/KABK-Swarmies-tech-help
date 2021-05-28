using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SocketServerConnect : MonoBehaviour {

    public InputField mainInputField;
    public Canvas canvas;
    public CanvasScaler canvasScaler;
    public float scaleDownSpeed = 2f;
    private bool socketConnected = false;

    void Start() {

        Events.instance.AddListener<SocketEvent>(socketEventHandler);
        string storedIP = PlayerPrefs.GetString("socketserverIP");
        if (storedIP != null || storedIP != "") {
            if (mainInputField != null) {
                mainInputField.text = storedIP;
            }
        }
    }

    void Update() {
        if (socketConnected && canvas.enabled) {
            canvasScaler.scaleFactor = Mathf.Lerp(canvasScaler.scaleFactor, 0f, scaleDownSpeed * Time.deltaTime);
            if (canvasScaler.scaleFactor < 0.01f) {
                Debug.Log("SocketServerConnect >> Update >> disabling Canvas");
                canvas.enabled = false;
            }
        }
    }

    private void socketEventHandler(SocketEvent e) {
        Debug.Log("SocketServerConnect >> f:socketEventHandler");

        socketConnected = true;
    }

    public void connectHandler() {
        SocketConnection.instance.connect(mainInputField.text);
    }

    private void OnDestroy() {
        if (mainInputField != null) {
            if (mainInputField.text != "") {
                PlayerPrefs.SetString("socketserverIP", mainInputField.text);
            }
        }
    }
}
