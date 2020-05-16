using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

using static EventSystem;

public class RoomManager : MonoBehaviourPunCallbacks {

    public static RoomManager roomManager;
    public Room currentRoom;
    public bool isReady = false;

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

    public void StartGame() {

        if (PhotonNetwork.IsMasterClient) {

            //load into new scene

            //TODO: add better system for identifying scene index (prob enum)
            PhotonNetwork.LoadLevel(1);

        }
    }

    public void JoinRoom(string roomName) {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void LeaveRoom() {
        PhotonNetwork.LeaveRoom();
    }

    public void SetReady(bool ready) {
        this.isReady = ready;
    }

    //bunch of event handles
    public override void OnCreatedRoom() {
        Debug.Log("Successfully created room");
    }
    public override void OnCreateRoomFailed(short returnCode, string message) {
        Debug.LogError("FAILED TO CREATE ROOM");
    }

    public override void OnJoinedRoom() {
        currentRoom = PhotonNetwork.CurrentRoom;
        Debug.Log("You successfully joined room");
    }
    public override void OnPlayerEnteredRoom(Player newPlayer) {
        Debug.Log($"{newPlayer.NickName} successfully joined room");
    }
    public override void OnJoinRoomFailed(short returnCode, string message) {
        Debug.LogError("FAILED TO JOIN ROOM");
    }

    public override void OnLeftRoom() {
        currentRoom = null;
        Debug.Log("You left room");
    }
    public override void OnPlayerLeftRoom(Player otherPlayer) {
        Debug.Log($"{otherPlayer.NickName} left room");
    }


    public bool isMasterClient() { return PhotonNetwork.IsMasterClient; }

}
