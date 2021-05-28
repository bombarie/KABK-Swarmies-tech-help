public class GlobalEvent : AbstractEvent {

    public enum EVENT_TYPE {
        MENU_COMPLETED,
    }
    public EVENT_TYPE evtType;

    public GlobalEvent(EVENT_TYPE evt) {
        //    Debug.Log(GlobalEvent >> init with event " + evt);
        this.evtType = evt;
    }

    override public string ToString() {
        return "GlobalEvent >> event type: " + evtType;
    }

}