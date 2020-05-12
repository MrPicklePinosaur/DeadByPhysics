using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkController : MonoBehaviourPunCallbacks {

    void Start() {
        PhotonNetwork.ConnectUsingSettings();
    }


    void Update() {
        
    }

    public override void OnConnectedToMaster() {
        //base.OnConnectedToMaster();

        Debug.Log($"connected to {PhotonNetwork.CloudRegion}");
    }
}
