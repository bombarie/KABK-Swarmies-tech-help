using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel1 : PanelBase, IPanelBehavior {

    void Start() {

    }

    public override void Prepare() {
        Debug.Log(name + " >> f:Prepare()");
    }

    public override void IsLoaded() {
        Debug.Log(name + " >> f:IsLoaded()");
    }
}
