using UnityEngine;
using System.Collections;

public class PanelBase : MonoBehaviour, IPanelBehavior {

    public bool allowDragging = false;
    public bool allowSwipePrevious = true;
    public bool allowSwipeNext = true;

    public virtual void Prepare() {
        Debug.Log(name + " >> f:Prepare()");
    }

    public virtual void IsLoaded() {
        Debug.Log(name + " >> f:IsLoaded()");
    }

    public void AllowDragging(bool b) {
        Debug.Log(name + " >> f:AllowDragging() >> setting to " + b);
        allowDragging = b;
    }

    public bool AllowSwipePrevious() {
        return allowSwipePrevious;
    }

    public bool AllowSwipeNext() {
        return allowSwipeNext;
    }

}
