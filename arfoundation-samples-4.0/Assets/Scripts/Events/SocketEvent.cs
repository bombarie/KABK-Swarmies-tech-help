public class SocketEvent : AbstractEvent {

    public enum EVENT_TYPE {
        CONNECTED,
    }
    public EVENT_TYPE evtType;

    public SocketEvent(EVENT_TYPE evt) {
        //    Debug.Log(SocketEvent >> init with event " + evt);
        this.evtType = evt;
    }

    override public string ToString() {
        return "SocketEvent >> event type: " + evtType;
    }

}