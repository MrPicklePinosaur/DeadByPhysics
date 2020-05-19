using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    public Animator aniMan;
    
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Character" && aniMan.GetBool("isAttacking") && !aniMan.GetBool("Hit"))
        {
            Debug.Log("Hit");
            aniMan.SetBool("Hit", true);
        }
        Debug.Log(collisionInfo.collider.tag);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
