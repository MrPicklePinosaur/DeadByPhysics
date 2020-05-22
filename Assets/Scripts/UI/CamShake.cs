using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour {

    Vector3 newPos;
    Quaternion newRot;

    public Vector2 min;
    public Vector2 max;
    public Vector2 yRot;
    public float lerpSpeed;

    void Awake() {
        newPos = transform.position;
        newRot = transform.rotation;
    }

    void Update() {
        transform.position = Vector3.Lerp(transform.position,newPos, Time.deltaTime*lerpSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime * lerpSpeed);

        if (Vector3.Distance(transform.position, newPos) < 1f) {

            newRot = Quaternion.Euler(Random.Range(yRot.x,yRot.y),0,0);
            newPos = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y),transform.position.z);
        }
    }
    
}
