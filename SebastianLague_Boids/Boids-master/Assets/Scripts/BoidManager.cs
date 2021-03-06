using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour {

    const int threadGroupSize = 1024;

    public BoidSettings settings;
    public ComputeShader compute;
    Boid[] boids;
    List<Boid> _boids;
    public Transform boidsTarget = null;

    void Start() {
        _boids = new List<Boid>();

        boids = FindObjectsOfType<Boid>();
        foreach (Boid b in boids) {
            InitializeBoid(b);
        }
    }

    public void InitializeBoid(Boid b) {
        _boids.Add(b);
        b.Initialize(settings, boidsTarget);
    }
    public void RemoveBoid() {
        Boid b = _boids[Random.Range(0, _boids.Count)];
        RemoveBoid(b);
    }
    public void RemoveBoid(Boid b) {
        if (b == null) {
            b = _boids[Random.Range(0, _boids.Count)];
        }
        _boids.Remove(b);
        GameObject.Destroy(b.gameObject);
    }

    void Update() {
        if (_boids != null) {

            int numBoids = _boids.Count;
            if (numBoids == 0) return;

            var boidData = new BoidData[numBoids];

            for (int i = 0; i < _boids.Count; i++) {
                boidData[i].position = _boids[i].position;
                boidData[i].direction = _boids[i].forward;
            }

            var boidBuffer = new ComputeBuffer(numBoids, BoidData.Size);
            boidBuffer.SetData(boidData);

            compute.SetBuffer(0, "boids", boidBuffer);
            compute.SetInt("numBoids", _boids.Count);
            compute.SetFloat("viewRadius", settings.perceptionRadius);
            compute.SetFloat("avoidRadius", settings.avoidanceRadius);

            int threadGroups = Mathf.CeilToInt(numBoids / (float)threadGroupSize);
            compute.Dispatch(0, threadGroups, 1, 1);

            boidBuffer.GetData(boidData);

            for (int i = 0; i < _boids.Count; i++) {
                _boids[i].avgFlockHeading = boidData[i].flockHeading;
                _boids[i].centreOfFlockmates = boidData[i].flockCentre;
                _boids[i].avgAvoidanceHeading = boidData[i].avoidanceHeading;
                _boids[i].numPerceivedFlockmates = boidData[i].numFlockmates;

                _boids[i].UpdateBoid();
            }

            boidBuffer.Release();
        }
    }

    public struct BoidData {
        public Vector3 position;
        public Vector3 direction;

        public Vector3 flockHeading;
        public Vector3 flockCentre;
        public Vector3 avoidanceHeading;
        public int numFlockmates;

        public static int Size {
            get {
                return sizeof(float) * 3 * 5 + sizeof(int);
            }
        }
    }
}