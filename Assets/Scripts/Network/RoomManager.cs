using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks {

    public void CreateNewRoom(string roomName) {

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
