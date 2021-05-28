using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel4 : PanelBase, IPanelBehavior {

    public InputField inputField;

    void Start() {
        Events.instance.AddListener<NameSubmitEvent>(nameSubmitHandler);

    }

    public override void Prepare() {
        Debug.Log(name + " >> f:Prepare()");
    }

    public override void IsLoaded() {
        Debug.Log(name + " >> f:IsLoaded() --> close this menu!");
        Debug.Log(name + " >> f:IsLoaded()");
    }

    public void submitName() {
        if (inputField == null) return;
        Debug.Log(name + " >> f:submitName >> entered name is " + inputField.text);

        // TODO -> do due diligence on the name! No illegal characters! :)

        Events.instance.Raise(new NameSubmitEvent(NameSubmitEvent.EVENT_TYPE.SUBMIT_NAME, inputField.text));
    }

    private void nameSubmitHandler(NameSubmitEvent e) {
        if (e.evtType == NameSubmitEvent.EVENT_TYPE.NAME_SUBMITTED) {
            DragUI.instance.nextPanel();
        }
    }

}
