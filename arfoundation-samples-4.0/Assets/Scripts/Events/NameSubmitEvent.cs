public class NameSubmitEvent : AbstractEvent {

    public enum EVENT_TYPE {
        SUBMIT_NAME,
        NAME_SUBMITTED
    }
    public EVENT_TYPE evtType;

    public string _name = "";

    public NameSubmitEvent(EVENT_TYPE evt, string s) {
        //    Debug.Log(NameSubmitEvent >> init with event " + evt);
        this.evtType = evt;
        _name = s;
    }

    public NameSubmitEvent(EVENT_TYPE evt) {
        //    Debug.Log(NameSubmitEvent >> init with event " + evt);
        this.evtType = evt;
    }

    override public string ToString() {
        return "NameSubmitEvent >> event type: " + evtType + ", name: " + this._name;
    }

}