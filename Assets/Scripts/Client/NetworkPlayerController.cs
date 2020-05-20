using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static PlayerProfile;
using static EventSystem;

[RequireComponent(typeof(PhotonView))]
public class NetworkPlayerController : MonoBehaviour {

    public PhotonView view;
    public float moveSpeed;

    void Start() {
        view = GetComponent<PhotonView>();
    }

    void Update() {

        if (!view.IsMine) return;

        Vector2 inp = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        transform.position += new Vector3(inp.x, 0, inp.y) * moveSpeed * Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.E)) {
            eventSystem.RaiseNetworkEvent(EventCodes.OnPlayerInteractEvent, new object[] { playerProfile.player.ActorNumber });
        }

        //testing take damage
        if (Input.GetKeyDown(KeyCode.P)) {
            eventSystem.RaiseNetworkEvent(EventCodes.OnPlayerDamageEvent, new object[] { playerProfile.player.ActorNumber });
        }
        
    }
    
}
