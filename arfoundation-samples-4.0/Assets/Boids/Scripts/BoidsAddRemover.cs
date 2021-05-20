using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsAddRemover : MonoBehaviour {
    // Start is called before the first frame update

    public BoidManager boidManager;
    public Spawner spawner;
    void Start() {
        //boidManager = GetComponent<BoidManager>();
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
            Boid b = spawner.spawnBoid();
            b.SetColour(Color.yellow);
            b.setScale(1.5f);
            boidManager.InitializeBoid(b);
            latestBoid = b;
        }
        if (Input.GetKeyUp(KeyCode.Alpha2)) {
            Debug.Log("[type2] add boid");
            Boid b = spawner.spawnBoid();
            b.SetColour(Color.red);
            b.setScale(new Vector3(2f, .5f, 2f));
            boidManager.InitializeBoid(b);
            latestBoid = b;
        }
        if (Input.GetKeyUp(KeyCode.Alpha3)) {
            Debug.Log("[type3] add boid");
            Boid b = spawner.spawnBoid();
            b.SetColour(Color.magenta);
            b.setScale(3f);
            boidManager.InitializeBoid(b);
            latestBoid = b;
        }
    }
}
