using System.Collections;
using System.Collections.Generic;
using Socket.Newtonsoft.Json.Linq;
using Socket.Newtonsoft.Json;
using Socket.Quobject.SocketIoClientDotNet.Client;
using UnityEngine;

public class SocketConnection : MonoBehaviour {
   public bool autoconnect = true;

   public QSocket socket;
   public string url = "";
   public int portNum = 3000;


   private static SocketConnection _instance;
   public static SocketConnection instance {
      get {
         if (_instance == null) {
            _instance = GameObject.FindObjectOfType<SocketConnection>();
         }

         return _instance;
      }
   }


   void Start() {
      //Debug.Log("start >> url: " + url);
      if (autoconnect) connect(url);
   }

   private bool addBoid = false;
   private bool nameAdded = false;
   void Update() {
      if (nameAdded) {
         nameAdded = false;
         Events.instance.Raise(new NameSubmitEvent(NameSubmitEvent.EVENT_TYPE.NAME_SUBMITTED));
      }

      if (addBoid) {
         try {
            int i = Random.Range(0, 3);
            switch (i) {
               case 0:
                  if (BoidsAddRemover.instance != null) {
                     BoidsAddRemover.instance.addBoidType(BoidsAddRemover.Type.TYPE_1);
                  }
                  break;
               case 1:
                  if (BoidsAddRemover.instance != null) {
                     BoidsAddRemover.instance.addBoidType(BoidsAddRemover.Type.TYPE_2);
                  }
                  break;
               case 2:
                  if (BoidsAddRemover.instance != null) {
                     BoidsAddRemover.instance.addBoidType(BoidsAddRemover.Type.TYPE_3);
                  }
                  break;
            }
         } catch (System.Exception e) {
            Debug.LogWarning("ERROR while adding new Boid type: " + e.ToString());
         }

         addBoid = false;
      }
      if (Input.GetKeyUp(KeyCode.Space)) {
         Debug.Log("spacebar >> registering name 'Frank'");

         if (socket != null) {
            socket.Emit("fname", "Frank");
         }
      }
   }

   public void connect() {
      connect(url);
   }
   public void connect(string url) {
      Debug.Log("SocketConnection >> f:connect >> url: " + url);

      if (socket != null) {
         socket.Disconnect();
      }

      string _url;
      if (url != "") {
         if (!url.StartsWith("http://")) {
            url = "http://" + url;
         }
         _url = url + ":" + portNum.ToString();
      } else {
         _url = "http://localhost:3000";
      }
      Debug.Log("SocketConnection >> f:connect >> connecting to url '" + _url + "'");

      socket = IO.Socket(_url);

      socket.On(QSocket.EVENT_CONNECT, () => {
         Debug.Log("SocketConnection >>Connected");
         Events.instance.Raise(new SocketEvent(SocketEvent.EVENT_TYPE.CONNECTED));
      });

      socket.On("fnameNew", data => {
         Debug.Log("SocketConnection >> fnameNew : " + data);
         addBoid = true;
         nameAdded = true;
         Debug.Log("SocketConnection >> fnameNew >> ping!");
      });
   }

   public void registerName(string name) {
      Debug.Log("SocketConnection >> f:registerName >> registering name '" + name + "'");

      socket.Emit("fname", name);
   }

   private void OnDestroy() {
      if (socket != null) {
         socket.Disconnect();
      }
   }
}
