using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static PlayerInstance;
using static EventSystem;
using static PlayerProfile;
public class DamageCollider : MonoBehaviour {
    Animator anim;

    public void Start() {
        anim = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other) {

        NetworkPlayerController cont = other.gameObject.GetComponent<NetworkPlayerController>();
        if (cont != null && anim.GetBool("isAttacking") && !anim.GetBool("Hit")) {

            //grab the actor number of the person we hit

            //we only care if the enemy hit our instance and its not a teacher
            //if () { return; }

            anim.SetBool("Hit", true);

            if (other.gameObject == playerInstance.clientAvatar) {

                //send player hit event here
                eventSystem.RaiseNetworkEvent(EventCodes.OnPlayerDamageEvent, new object[] { playerProfile.player.ActorNumber });
            }





        }
    }

}
