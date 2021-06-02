using UnityEngine;

public class SwarmEvent : AbstractEvent {

    public enum EVENT_TYPE {
        SET_POSITION,
        ADD_BOIDS
    }
    public EVENT_TYPE evtType;

    public Vector3 position;
    public int amount = 1;

    public SwarmEvent(EVENT_TYPE evt) {
        //    Debug.Log(SwarmEvent >> init with event " + evt);
        this.evtType = evt;
    }
    public SwarmEvent(EVENT_TYPE evt, Vector3 pos) {
        //    Debug.Log(SwarmEvent >> init with event " + evt);
        this.evtType = evt;
        this.position = pos;
    }
    public SwarmEvent(EVENT_TYPE evt, int num) {
        //    Debug.Log(SwarmEvent >> init with event " + evt);
        this.evtType = evt;
        this.amount = num;
    }

    override public string ToString() {
        return "SwarmEvent >> event type: " + evtType + ", position: " + position + ", amount: " + amount;
    }

}