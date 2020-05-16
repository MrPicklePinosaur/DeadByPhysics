using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using static PlayerProfile;

using static EventSystem;

public class LobbyManager : MonoBehaviourPunCallbacks {

    void Start() {

        PhotonNetwork.ConnectUsingSettings();

        /*
        if (!PhotonNetwork.IsConnected) {

        }
        */
        
    }

    public override void OnConnectedToMaster() {

        Debug.Log("Successfully connected to master server");

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = playerProfile.Name;
        PhotonNetwork.JoinLobby(TypedLobby.Default);

    }

    public override void OnJoinedLobby() {

        Debug.Log("Successfully joined lobby");
    }

}
