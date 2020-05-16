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


    public override void OnCreatedRoom() {
        Debug.Log("Successfully Created Room");
    }
    public override void OnCreateRoomFailed(short returnCode, string message) {
        Debug.LogError("FAILED TO CREATE ROOM");
    }


}
