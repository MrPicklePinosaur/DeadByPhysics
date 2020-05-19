using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Enemy")
        {
            Debug.Log("Hit");
        }
        Debug.Log(collisionInfo.collider.tag);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
