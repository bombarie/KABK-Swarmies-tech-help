using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsTarget : MonoBehaviour {

    public enum GizmoType { Never, SelectedOnly, Always }
    public float targetRadius = 10;
    public Color colour;
    public GizmoType showSpawnRegion;

    private void OnDrawGizmos() {
        if (showSpawnRegion == GizmoType.Always) {
            DrawGizmos();
        }
    }

    void OnDrawGizmosSelected() {
        if (showSpawnRegion == GizmoType.SelectedOnly) {
            DrawGizmos();
        }
    }

    void DrawGizmos() {

        Gizmos.color = new Color(colour.r, colour.g, colour.b, 0.3f);
        Gizmos.DrawSphere(transform.position, targetRadius);
    }
}
