using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    public GameObject clientAvatar;

    void Start() {

        clientAvatar = PhotonNetwork.Instantiate("Player",Vector3.zero,Quaternion.identity);
        
    }
    
}
