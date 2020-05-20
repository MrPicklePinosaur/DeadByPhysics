using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class NetworkTeacherController : NetworkPlayerController {

    bool isAttacking;


    //called from animation event
    private void stopAttacking() {
        anim.SetBool("isAttacking", false);
    }

    void Update() {
        if (!view.IsMine) return;

        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking) {
            isAttacking = true;
        } else if (Input.GetKeyUp(KeyCode.Space)) {
            isAttacking = false;
        } else if (isAttacking && !anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) {
            isAttacking = false;
            anim.SetBool("isAttacking", true);
            anim.SetBool("Hit", false);
        } else if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) {

            //you are not allowed to move if attacking
            HandleMovement();

        }
    }
    
}
