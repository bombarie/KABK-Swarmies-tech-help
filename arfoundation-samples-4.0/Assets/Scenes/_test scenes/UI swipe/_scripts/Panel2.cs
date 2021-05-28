using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel2 : PanelBase, IPanelBehavior {

    void Start() {
        allowSwipeNext = false;
    }

    public override void Prepare() {
        Debug.Log(name + " >> f:Prepare()");
    }

    public override void IsLoaded() {
        Debug.Log(name + " >> f:IsLoaded()");
    }

}
