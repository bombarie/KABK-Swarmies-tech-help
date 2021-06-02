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
            // linear
            img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a - (fadeSpeed * Time.deltaTime));
            title.color = new Color(title.color.r, title.color.g, title.color.b, title.color.a - (fadeSpeed * Time.deltaTime));
            logo.color = new Color(logo.color.r, logo.color.g, logo.color.b, logo.color.a - (fadeSpeed * Time.deltaTime));

            // lerp
            //img.color = Color.Lerp(img.color, targColor, fadeSpeed * Time.deltaTime);
            //title.color = Color.Lerp(title.color, titleTargColor, fadeSpeed * Time.deltaTime);
            //logo.color = Color.Lerp(logo.color, logoTargcolor, fadeSpeed * Time.deltaTime);

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
