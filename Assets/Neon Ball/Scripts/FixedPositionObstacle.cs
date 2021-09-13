using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedPositionObstacle : MonoBehaviour {

    public Vector2 pos;

    void Update() {
        transform.localPosition = pos;
    }
}
