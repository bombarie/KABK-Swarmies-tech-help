using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {

    BoidSettings settings;

    // State
    [HideInInspector]
    public Vector3 position;
    [HideInInspector]
    public Vector3 forward;
    Vector3 velocity;

    // To update:
    Vector3 acceleration;
    [HideInInspector]
    public Vector3 avgFlockHeading;
    [HideInInspector]
    public Vector3 avgAvoidanceHeading;
    [HideInInspector]
    public Vector3 centreOfFlockmates;
    [HideInInspector]
    public int numPerceivedFlockmates;

    // Cached
    Material material;
    Transform cachedTransform;
    Transform target;

    void Awake() {
        material = transform.GetComponentInChildren<MeshRenderer>().material;
        cachedTransform = transform;
    }

    public void Initialize(BoidSettings settings, Transform target) {
        this.target = target;
        this.settings = settings;

        position = cachedTransform.position;
        forward = cachedTransform.forward;

        float startSpeed = ((settings.minSpeed * settings.scaleMultiplier) + (settings.maxSpeed * settings.scaleMultiplier)) / 2;
        velocity = transform.forward * startSpeed;
    }

    public void SetColour(Color col) {
        if (material != null) {
            material.color = col;
        }
    }
    public void setScale(float scale) {
        setScale(Vector3.one * scale);
    }
    public void setScale(Vector3 scale) {
        transform.localScale = scale;
    }

    public void setSettings(BoidSettings settings) {
        this.settings = settings;
    }

    public void setTarget(Transform target) {
        this.target = target;
    }

    public void UpdateBoid() {
        Vector3 acceleration = Vector3.zero;

        if (target != null) {
            Vector3 offsetToTarget = (target.position - position);
            acceleration = SteerTowards(offsetToTarget) * (settings.targetWeight * settings.scaleMultiplier);
        }

        if (numPerceivedFlockmates != 0) {
            centreOfFlockmates /= numPerceivedFlockmates;

            Vector3 offsetToFlockmatesCentre = (centreOfFlockmates - position);

            var alignmentForce = SteerTowards(avgFlockHeading) * (settings.alignWeight * settings.scaleMultiplier);
            var cohesionForce = SteerTowards(offsetToFlockmatesCentre) * (settings.cohesionWeight * settings.scaleMultiplier);
            var seperationForce = SteerTowards(avgAvoidanceHeading) * (settings.seperateWeight * settings.scaleMultiplier);

            acceleration += alignmentForce;
            acceleration += cohesionForce;
            acceleration += seperationForce;
        }

        if (IsHeadingForCollision()) {
            Vector3 collisionAvoidDir = ObstacleRays();
            Vector3 collisionAvoidForce = SteerTowards(collisionAvoidDir) * (settings.avoidCollisionWeight * settings.scaleMultiplier);
            acceleration += collisionAvoidForce;
        }

        velocity += acceleration * Time.deltaTime;
        float speed = velocity.magnitude;
        Vector3 dir = velocity / speed;
        speed = Mathf.Clamp(speed, settings.minSpeed * settings.scaleMultiplier, settings.maxSpeed * settings.scaleMultiplier);
        velocity = dir * speed;

        cachedTransform.position += velocity * Time.deltaTime;
        cachedTransform.forward = dir;
        position = cachedTransform.position;
        forward = dir;
    }

    BoidManagerHelper.Type boidType = BoidManagerHelper.Type.DEFAULT;
    public void setType(BoidManagerHelper.Type type) {
        boidType = type;

        switch (boidType) {
            //case 
        }
    }

    bool IsHeadingForCollision() {
        RaycastHit hit;
        if (Physics.SphereCast(position, settings.boundsRadius * settings.scaleMultiplier, forward, out hit, settings.collisionAvoidDst * settings.scaleMultiplier, settings.obstacleMask)) {
            return true;
        } else { }
        return false;
    }

    Vector3 ObstacleRays() {
        Vector3[] rayDirections = BoidHelper.directions;

        for (int i = 0; i < rayDirections.Length; i++) {
            Vector3 dir = cachedTransform.TransformDirection(rayDirections[i]);
            Ray ray = new Ray(position, dir);
            if (!Physics.SphereCast(ray, settings.boundsRadius * settings.scaleMultiplier, settings.collisionAvoidDst * settings.scaleMultiplier, settings.obstacleMask)) {
                return dir;
            }
        }

        return forward;
    }

    Vector3 SteerTowards(Vector3 vector) {
        Vector3 v = vector.normalized * (settings.maxSpeed * settings.scaleMultiplier) - velocity;
        return Vector3.ClampMagnitude(v, settings.maxSteerForce * settings.scaleMultiplier);
    }

}