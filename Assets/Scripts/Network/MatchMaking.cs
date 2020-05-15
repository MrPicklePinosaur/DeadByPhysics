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

    public void JoinRoom() {
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Attempting to join room");
    }

    public override void OnJoinRandomFailed(short returnCode, string message) {

        //if we failed to join room, we create a new room and join it
        CreateNewRoom();
    }

    public void CreateNewRoom() {
        int roomCode = Random.Range(0, 1000000);
        PhotonNetwork.CreateRoom($"Room${roomCode}", new RoomOptions() {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = (byte)8
        });
    }

    public override void OnCreateRoomFailed(short returnCode, string message) {

        Debug.LogError("FAILED TO CREATE ROOM");
        //CreateNewRoom();
    }



}
