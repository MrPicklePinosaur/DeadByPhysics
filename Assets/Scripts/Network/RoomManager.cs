using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

using static EventSystem;

public class RoomManager : MonoBehaviourPunCallbacks {

    public static RoomManager roomManager;

    private void Start() {
        RoomManager.roomManager = this;
    }

    public void CreateNewRoom(string roomName) {

        Debug.Log($"Attempting to create room of name <{roomName}>");

        PhotonNetwork.CreateRoom(roomName, new RoomOptions() {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 5
        }, TypedLobby.Default);

    }
    public void JoinRoom(string roomName) {
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinedRoom() {
        Debug.Log("Successfully joined room");
    }
    public override void OnJoinRoomFailed(short returnCode, string message) {
        Debug.LogError("FAILED TO JOIN ROOM");
    }

    public override void OnCreatedRoom() {
        Debug.Log("Successfully created room");
    }
    public override void OnCreateRoomFailed(short returnCode, string message) {
        Debug.LogError("FAILED TO CREATE ROOM");
    }

}
