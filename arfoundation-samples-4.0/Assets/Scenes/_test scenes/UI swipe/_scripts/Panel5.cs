using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel5 : PanelBase, IPanelBehavior {

    public float fadeSpeed = 1f;
    public Color targColor;

    private Image img;

    private bool IsPrepared = false;

    [Space(10)]
    public Image logo;
    public Color logoTargcolor;

    [Space(10)]
    public Text title;
    public Color titleTargColor;


    void Start() {
        img = GetComponent<Image>();
    }
    void Update() {
        if (IsPrepared) {
            img.color = Color.Lerp(img.color, targColor, fadeSpeed * Time.deltaTime);
            title.color = Color.Lerp(title.color, titleTargColor, fadeSpeed * Time.deltaTime);
            logo.color = Color.Lerp(logo.color, logoTargcolor, fadeSpeed * Time.deltaTime);

            if (img.color.a < 0.05f) {
                Events.instance.Raise(new GlobalEvent(GlobalEvent.EVENT_TYPE.MENU_COMPLETED));

                IsPrepared = false;

                DragUI.instance.disableMe();
            }
        }

    }

    public override void Prepare() {
        Debug.Log(name + " >> f:Prepare()");
    }

    public override void IsLoaded() {
        Debug.Log(name + " >> f:IsLoaded()");

        IsPrepared = true;

    }

}
