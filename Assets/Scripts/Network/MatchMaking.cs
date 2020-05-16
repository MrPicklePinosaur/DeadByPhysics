using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class MatchMaking : MonoBehaviourPunCallbacks {

    public static MatchMaking matchMaking;

    void Start() {
        MatchMaking.matchMaking = this;

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() {
        //base.OnConnectedToMaster();

        Debug.Log($"connected to {PhotonNetwork.CloudRegion}");
    }

    public void JoinRoom(string code) {
        
        //Join room by room name
        Debug.Log("Attempting to join room");

        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message) {

        Debug.Log("FAILED TO JOIN ROOM");
        //if we failed to join room, we create a new room and join it
        CreateNewRoom();
        JoinRoom("");
    }

    public string CreateNewRoom() {
        string roomCode = Utils.GenerateRoomCode();
        PhotonNetwork.CreateRoom($"Room${roomCode}", new RoomOptions() {
            IsVisible = false,
            IsOpen = true,
            MaxPlayers = (byte)8
        });
        return roomCode;
    }

    public override void OnCreateRoomFailed(short returnCode, string message) {

        Debug.LogError("FAILED TO CREATE ROOM");
        //CreateNewRoom();

    }

    public override void OnJoinedRoom() {

        Debug.Log("Sucessfully Joined Room");
    }

    void StartGame() {
        if (PhotonNetwork.IsMasterClient) {
            Debug.Log("STARTING GAME");
        }
    }

    public override void OnEnable() {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable() {
        PhotonNetwork.RemoveCallbackTarget(this);
    }



}
