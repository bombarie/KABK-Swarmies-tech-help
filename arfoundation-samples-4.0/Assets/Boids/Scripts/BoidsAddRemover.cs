using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsAddRemover : MonoBehaviour {


    private static BoidsAddRemover _instance;
    public static BoidsAddRemover instance {
        get {
            if (_instance == null) {
                _instance = GameObject.FindObjectOfType<BoidsAddRemover>();
            }

            return _instance;
        }
    }

    public enum Type {
        TYPE_1,
        TYPE_2,
        TYPE_3
    }

    public BoidManager boidManager;
    public Spawner spawner;

    void Start() {
    }

    Boid latestBoid = null;
    void Update() {
        if (Input.GetKeyUp(KeyCode.UpArrow)) {
            Debug.Log("[neutral] add boid");
            Boid b = spawner.spawnBoid();
            boidManager.InitializeBoid(b);
            latestBoid = b;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow)) {
            if (Input.GetKey(KeyCode.LeftShift)) {
                Debug.Log("remove latest boid");
                boidManager.RemoveBoid(latestBoid);
                latestBoid = null;
            } else {
                Debug.Log("remove boid");
                boidManager.RemoveBoid();
            }
        }

        if (Input.GetKeyUp(KeyCode.Alpha1)) {
            Debug.Log("[type1] add boid");
            addBoidType(Type.TYPE_1);
        }
        if (Input.GetKeyUp(KeyCode.Alpha2)) {
            Debug.Log("[type2] add boid");
            addBoidType(Type.TYPE_2);
        }
        if (Input.GetKeyUp(KeyCode.Alpha3)) {
            Debug.Log("[type3] add boid");
            addBoidType(Type.TYPE_3);
        }
    }

    public void addBoidType(Type type) {
        Boid b = spawner.spawnBoid();

        switch (type) {
            case Type.TYPE_1:
                b.SetColour(Color.red);
                b.setScale(new Vector3(2f, .5f, 2f));
                break;
            case Type.TYPE_2:
                b.SetColour(Color.yellow);
                b.setScale(1.5f);
                break;
            case Type.TYPE_3:
                b.SetColour(Color.magenta);
                b.setScale(3f);
                break;
        }

        boidManager.InitializeBoid(b);
        latestBoid = b;
    }
}
