using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel5 : PanelBase, IPanelBehavior {

    public float fadeSpeed = 1f;
    public Color targColor;

    private Image img;

    private bool IsPrepared = false;

    void Start() {
        img = GetComponent<Image>();
    }
    void Update() {
        if (IsPrepared) {
            img.color = Color.Lerp(img.color, targColor, fadeSpeed * Time.deltaTime);
        }
    }

    public override void Prepare() {
        Debug.Log(name + " >> f:Prepare()");
        IsPrepared = true;
    }

    public override void IsLoaded() {
        Debug.Log(name + " >> f:IsLoaded() --> close this menu!");
        Debug.Log(name + " >> f:IsLoaded()");

        Events.instance.Raise(new GlobalEvent(GlobalEvent.EVENT_TYPE.MENU_COMPLETED));

        DragUI.instance.disableMe();
    }

}
