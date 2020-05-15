using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MatchMaking : MonoBehaviourPunCallbacks {

    void Start() {
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

    }

    public override void OnJoinRandomFailed(short returnCode, string message) {

        Debug.LogError("FAILED TO JOIN ROOM");
        //if we failed to join room, we create a new room and join it
        //CreateNewRoom();
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



}
