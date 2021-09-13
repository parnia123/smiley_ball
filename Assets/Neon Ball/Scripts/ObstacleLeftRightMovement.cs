using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLeftRightMovement : MonoBehaviour {

    public float startPos;
    public float endPos;
    public float speed = 0;
    public Rigidbody2D rb;
    private bool left = false;
    
    void FixedUpdate() {
        if(!left) {
            rb.MovePosition(new Vector2(rb.position.x + speed * Time.fixedDeltaTime, rb.position.y));
            if(transform.localPosition.x >= endPos) {
                left = true;
            }
        }else {
            rb.MovePosition(new Vector2(rb.position.x - speed * Time.fixedDeltaTime, rb.position.y));
            if(transform.localPosition.x <= startPos) {
                left = false;
            }
        }
    }
}
