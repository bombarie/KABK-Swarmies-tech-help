using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SocketServerConnect : MonoBehaviour {

   public InputField mainInputField;

   void Start() {
      string storedIP = PlayerPrefs.GetString("socketserverIP");
      if (storedIP != null || storedIP != "") {
         if (mainInputField != null) {
            mainInputField.text = storedIP;
         }
      }
   }

   public void connectHandler() {
      SocketConnection.instance.connect(mainInputField.text);
   }

   private void OnDestroy() {
      if (mainInputField != null) {
         if (mainInputField.text != "") {
            PlayerPrefs.SetString("socketserverIP", mainInputField.text);
         }
      }
   }
}
