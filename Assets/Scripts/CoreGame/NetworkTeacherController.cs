using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator),typeof(PhotonView))]
public class NetworkTeacherController : MonoBehaviour {

    public float moveSpeed;

    PhotonView view;
    Animator anim;

    bool isAttacking;

    private void Start() {
        view = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();
    }
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
        }

        if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) {

            //you are not allowed to move if attacking
            HandleMovement();

        }
    }
    protected void HandleMovement() {
        //sprinting
        var sprintMult = 1;
        if (Input.GetKey(KeyCode.LeftShift)) {
            sprintMult *= 2;
        }

        Vector2 inp = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        Vector3 move = new Vector3(inp.x, 0, inp.y) * moveSpeed * sprintMult ;
        transform.Translate(move * Time.deltaTime, Space.Self);
        //animation stuff
        anim.SetFloat("Forward/Backward Speed", move.z);
        anim.SetFloat("SideToSide Speed", move.x);
    }

}
