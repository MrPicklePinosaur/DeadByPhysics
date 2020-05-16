using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class NetworkPlayerController : MonoBehaviour {

    PhotonView view;
    public float moveSpeed;

    void Start() {
        view = GetComponent<PhotonView>();
    }

    void Update() {

        if (view.IsMine) {
            Vector2 inp = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            transform.position += new Vector3(inp.x, 0, inp.y) * moveSpeed * Time.deltaTime;
        }
        
    }
    
}
