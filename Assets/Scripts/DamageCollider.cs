using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static EventSystem;

public class DamageCollider : MonoBehaviour {
    Animator anim;

    public void Start() {
        anim = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other) {
        

        if (other.gameObject.GetComponent<NetworkPlayerController>() != null && anim.GetBool("isAttacking") && !anim.GetBool("Hit")) {

            anim.SetBool("Hit", true);

            //send player hit event here
            Debug.Log("HIT PLAYER YAYYY");
            //eventSystem.RaiseNetworkEvent(EventCodes.OnPlayerDamageEvent, new object[] {  });
        }
    }

}
