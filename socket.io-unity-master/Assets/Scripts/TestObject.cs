using System.Collections;
using System.Collections.Generic;
using Socket.Newtonsoft.Json.Linq;
using Socket.Newtonsoft.Json;
using Socket.Quobject.SocketIoClientDotNet.Client;
using UnityEngine;

public class JSONMSG {
    public string msg;
    public string name;

    public override string ToString() {
        return "message: " + msg + ", name: " + name;
    }
}

public class TestObject : MonoBehaviour {
    private QSocket socket;
    public string url = "";
    public int portNum = 3000;


    void Start() {
        Debug.Log("start >> url: " + url);
        string _url;
        if (url != "") {
            if (!url.StartsWith("http://")) {
                url = "http://" + url;
            }
            _url = url + ":" + portNum.ToString();
        } else {
            _url = "http://localhost:3000";
        }
        Debug.Log("connecting to url '" + _url + "'");
        socket = IO.Socket(_url);

        socket.On(QSocket.EVENT_CONNECT, () => {
            Debug.Log("Connected");
            socket.Emit("chat", "test");
        });

        socket.On("chat", data => {
            Debug.Log("data : " + data);
        });
        socket.On("fname", data => {
            Debug.Log("fname : " + data);
        });
        socket.On("fnameNew", data => {
            Debug.Log("fnameNew : " + data);

            JSONMSG jm = JsonConvert.DeserializeObject<JSONMSG>(data.ToString());
            Debug.Log("jm: " + jm.ToString());

            JObject o = JObject.Parse(data.ToString());
            Debug.Log("o is: " + o.ToString());
            if (o != null) {
                if (o.HasValues) {
                    Debug.Log("JObject has values");

                    // src for help: 
                    //      https://www.newtonsoft.com/json/help/html/N_Newtonsoft_Json_Linq.htm
                    //      https://www.newtonsoft.com/json/help/html/Properties_T_Newtonsoft_Json_Linq_JObject.htm
                    //      https://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_Linq_JToken.htm


                    // this is just the whole object (but reads as a 'property'.. eh?)
                    JToken jt = o.First;
                    Debug.Log("jt.ToString(): " + jt.ToString());
                    Debug.Log("jt.Type: " + jt.Type);

                    // ok, this is the value of that key
                    JToken msgToken = o["msg"];
                    Debug.Log("msgToken.ToString(): " + msgToken.ToString());
                    Debug.Log("msgToken.Type: " + msgToken.Type);
                    JToken nameToken = o["name"];
                    Debug.Log("nameToken.ToString(): " + nameToken.ToString());
                    Debug.Log("nameToken.Type: " + nameToken.Type);


                    
                    //JToken jt3 = o["foo"];
                    JToken jt3 = o.GetValue("msg"); ;
                Debug.Log("jt3: " + jt3.ToString());

                    string s = jt.Value<string>("msg");
                    Debug.Log("'msg' value: " + s);
                }
            }

        });
    }

    void Update() {
        if (Input.GetKeyUp(KeyCode.Space)) {
            Debug.Log("spacebar");

            if (socket != null) {
                socket.Emit("fname", "Frank");
            }
        }
    }

    private void OnDestroy() {
        socket.Disconnect();
    }
}