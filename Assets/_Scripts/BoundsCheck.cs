using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour {
    /// <summary>
    ///     Keeps a GameObject on screen.
    ///     Note that this ONLY works for an orthographic Main Camera at [0, 0, 0].
    /// </summary>


    [Header("Set in Inspector")]
    public float radius = 1f;

    [Header("Set Dynamically")]
    public float camWidth;
    public float camHeight;

    // Use this for initialization
    void Awake() {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }

    // Update is called once per frame
    void LateUpdate() {
        Vector3 pos = transform.position;
        if (pos.x > camWidth - radius) {
            pos.x = camWidth - radius;
        }

        if (pos.x < -camWidth + radius) {
            pos.x = -camWidth + radius;
        }

        if (pos.y>camHeight-radius) {
            pos.y = camHeight - radius;
        }

        if (pos.y < -camHeight + radius) {
            pos.y = -camHeight + radius;
        }

        transform.position = pos;
    }

    //draw the bounds in the Scene pane using OnDrawGizmos()
    void OnDrawGizmos() {
        if (!Application.isPlaying) return;
        Vector3 boundSize = new Vector3(camWidth*2, camHeight*2, 0.1f);
        Gizmos.DrawWireCube(Vector3.zero, boundSize);
    }
}
