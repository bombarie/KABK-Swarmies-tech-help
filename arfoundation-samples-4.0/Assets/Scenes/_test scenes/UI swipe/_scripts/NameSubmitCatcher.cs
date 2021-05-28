using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameSubmitCatcher : MonoBehaviour {

    void Start() {
        Events.instance.AddListener<NameSubmitEvent>(nameSubmittedHandler);
    }

    private void nameSubmittedHandler(NameSubmitEvent e) {
        Debug.Log("NameSubmitCatcher >> f:nameSubmittedHandler >> name: " + e._name);

        if (e.evtType == NameSubmitEvent.EVENT_TYPE.SUBMIT_NAME) {
            SocketConnection.instance.registerName(e._name);
        }
    }

}
