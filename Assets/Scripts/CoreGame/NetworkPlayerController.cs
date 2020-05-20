using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static PlayerProfile;
using static EventSystem;

[RequireComponent(typeof(PhotonView),typeof(PhotonAnimatorView),typeof(Animator))]
public class NetworkPlayerController : MonoBehaviour {

    public PhotonView view;
    PhotonAnimatorView photonAnim;
    Animator anim;
    public float moveSpeed;

    void Start() {
        view = GetComponent<PhotonView>();
        photonAnim = GetComponent<PhotonAnimatorView>();
        anim = GetComponent<Animator>();
    }

    void Update() {

        if (!view.IsMine) return;

        //sprinting
        var sprintMult = 1;
        if (Input.GetKey(KeyCode.LeftShift)) {
            sprintMult *= 2;
        }

        Vector2 inp = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        Vector3 move = new Vector3(inp.x, 0, inp.y) * moveSpeed * sprintMult;
        transform.position +=  move * Time.deltaTime;
        

        //interact
        if (Input.GetKeyDown(KeyCode.E)) {
            eventSystem.RaiseNetworkEvent(EventCodes.OnPlayerInteractEvent, new object[] { playerProfile.player.ActorNumber });
        }


        //testing take damage
        if (Input.GetKeyDown(KeyCode.P)) {
            eventSystem.RaiseNetworkEvent(EventCodes.OnPlayerDamageEvent, new object[] { playerProfile.player.ActorNumber });
        }

        //animation stuff
        anim.SetFloat("Forward/Backward Speed", move.z);
        anim.SetFloat("SideToSide Speed", move.x);
    }
    
}
