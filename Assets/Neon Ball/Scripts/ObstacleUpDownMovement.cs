using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleUpDownMovement : MonoBehaviour {

    public float startPos;
    public float endPos;
    public float speed = 0;
    public Rigidbody2D rb;
    private bool up = false;
    
    void FixedUpdate() {
        if(up) {
            rb.MovePosition(new Vector2(rb.position.x, rb.position.y + speed * Time.fixedDeltaTime));
            if(transform.localPosition.y >= endPos) {
                up = false;
            }
        }else {
            rb.MovePosition(new Vector2(rb.position.x, rb.position.y - speed * Time.fixedDeltaTime));
            if(transform.localPosition.y <= startPos) {
                up = true;
            }
        }
    }
}
