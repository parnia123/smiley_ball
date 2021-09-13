using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleShowHide : MonoBehaviour {

    private SpriteRenderer sr;
    private float alpha = 0;
    public bool isOn = false;
    private EdgeCollider2D edgeCollider;

    void Start () {
        sr = GetComponent<SpriteRenderer> ();
        edgeCollider = GetComponent<EdgeCollider2D> ();
    }

    void Update() {
        if(isOn) {
            edgeCollider.enabled = true;
            if(alpha >= 1) return;
            alpha += Time.deltaTime;
        }else {
            if(alpha <= 0) {
                edgeCollider.enabled = false;
                return;
            }
            alpha -= Time.deltaTime; 
        }  
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
    }
}
