using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {

    private static DragUI _instance;
    public static DragUI instance {
        get {
            if (_instance == null) {
                _instance = GameObject.FindObjectOfType<DragUI>();
            }

            return _instance;
        }
    }


    public Vector3 panelTargetPos;
    public int panelW = 1200;
    public int panelH = 2000;
    public float snapSpeed = 15f;
    public float changePanelSwipeThreshold = 0.5f;
    public float nextPanelLoadedDistanceThreshold = 0.05f;

    //public Transform[] panels;
    public RectTransform[] panels;
    public int selectedPanel;

    public bool IsDragging = false;
    public bool IsMovingTowardsTarget = false;

    private Canvas canvas;
    public Transform screensParent;

    void Start() {
        canvas = GetComponent<Canvas>();

        panelTargetPos = screensParent.position;
        panelW = Screen.width;
        panelH = Screen.height;
        Debug.Log("DragUI >> Start() >> panelW,panelH = " + panelW + "," + panelH);

        for (int i = 0; i < panels.Length; i++) {
            //panels[i].transform.localPosition = new Vector3(i * panelW, panels[i].transform.localPosition.y, i * 5);
            panels[i].transform.localPosition = new Vector3(i * panelW, 0f, i * 5);
        }

        IPanelBehavior ipb = panels[selectedPanel].GetComponent<IPanelBehavior>();
        if (ipb != null) {
            ipb.Prepare();
            ipb.IsLoaded();
        }

    }

    void Update() {
        if (!IsDragging) {
            if (Vector3.Distance(screensParent.position, panelTargetPos) > nextPanelLoadedDistanceThreshold) {
                IsMovingTowardsTarget = true;
                screensParent.position = Vector3.Lerp(screensParent.position, panelTargetPos, snapSpeed * Time.deltaTime);
            } else {
                if (IsMovingTowardsTarget) {
                    IPanelBehavior ipb = panels[selectedPanel].GetComponent<IPanelBehavior>();
                    if (ipb != null) {
                        ipb.IsLoaded();
                    }
                    IsMovingTowardsTarget = false;
                }
            }

        }
    }

    public void OnBeginDrag(PointerEventData eventData) {
        //Debug.Log("f:OnBeginDrag >> eventData.position" + eventData.pressPosition);
        IsDragging = true;
    }

    public void OnDrag(PointerEventData eventData) {
        //Debug.Log("f:OnDrag >> eventData delta pos: " + (eventData.position - eventData.pressPosition));
        screensParent.position = panelTargetPos + new Vector3(eventData.position.x - eventData.pressPosition.x, 0f, 0f);
    }

    public void OnEndDrag(PointerEventData eventData) {
        float pctChange = (eventData.position.x - eventData.pressPosition.x) / Screen.width;
        Debug.Log("f:OnEndDrag >> pct change (x-axis):" + pctChange);

        if (pctChange > changePanelSwipeThreshold) {
            // swiped right
            IPanelBehavior ipb = panels[selectedPanel].GetComponent<IPanelBehavior>();
            if (ipb != null) {
                if (ipb.AllowSwipePrevious()) {
                    previousPanel();
                }
            }
        }
        if (pctChange < -changePanelSwipeThreshold) {
            // swiped left
            IPanelBehavior ipb = panels[selectedPanel].GetComponent<IPanelBehavior>();
            if (ipb != null) {
                if (ipb.AllowSwipeNext()) {
                    nextPanel();
                }
            }
        }
        IsDragging = false;
    }

    public void nextPanel() {
        Debug.Log("f:nextPanel");
        if (panels.Length == 0) return;

        selectedPanel++;
        selectedPanel = Mathf.Min(selectedPanel, panels.Length - 1);

        IPanelBehavior ipb = panels[selectedPanel].GetComponent<IPanelBehavior>();
        if (ipb != null) {
            ipb.Prepare();
        }
        // I don't understand why I have to offset the y target with the panel's height but hey... *shrug*
        panelTargetPos = new Vector3(-panels[selectedPanel].localPosition.x + (panels[selectedPanel].rect.width / 2f), panels[selectedPanel].rect.height / 2f, 0f);
    }

    public void previousPanel() {
        Debug.Log("f:previousPanel");
        if (panels.Length == 0) return;

        selectedPanel--;
        selectedPanel = Mathf.Max(selectedPanel, 0);

        // I don't understand why I have to offset the y target with the panel's height but hey... *shrug*
        panelTargetPos = new Vector3(-panels[selectedPanel].localPosition.x + (panels[selectedPanel].rect.width / 2f), panels[selectedPanel].rect.height / 2f, 0f);
    }

    public void disableMe() {
        //screensParent.gameObject.SetActive(false);
        canvas.enabled = false;
    }

}

