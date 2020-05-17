using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static EventSystem;

//catch pun callbacks and converts them into events
public class PunCallbacksCatcher : MonoBehaviourPunCallbacks {

    public override void OnConnected() { }

    public override void OnLeftRoom() {
        Debug.Log("You left room");
        eventSystem.RaiseClientEvent(EventCodes.OnLeftRoomEvent); }

    public override void OnMasterClientSwitched(Player newMasterClient) { }

    public override void OnCreateRoomFailed(short returnCode, string message) { }

    public override void OnJoinRoomFailed(short returnCode, string message) { }

    public override void OnCreatedRoom() { eventSystem.RaiseClientEvent(EventCodes.OnCreatedRoomEvent); }

    public override void OnJoinedLobby() { }

    public override void OnLeftLobby() {  }

    public override void OnDisconnected(DisconnectCause cause) { }

    public override void OnRegionListReceived(RegionHandler regionHandler) { }

    public override void OnRoomListUpdate(List<RoomInfo> roomList) { }

    public override void OnJoinedRoom() { eventSystem.RaiseClientEvent(EventCodes.OnJoinedRoomEvent); }

    public override void OnPlayerEnteredRoom(Player newPlayer) { }

    public override void OnPlayerLeftRoom(Player otherPlayer) { Debug.Log("Someone left room"); }

    public override void OnJoinRandomFailed(short returnCode, string message) { }

    public override void OnConnectedToMaster() { }

    /*
    public virtual void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged) {
    }

    public virtual void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps) {
    }

    public virtual void OnFriendListUpdate(List<FriendInfo> friendList) {
    }

    public virtual void OnCustomAuthenticationResponse(Dictionary<string, object> data) {
    }

   
    public virtual void OnCustomAuthenticationFailed(string debugMessage) {
    }

    public virtual void OnWebRpcResponse(OperationResponse response) {
    }

    public virtual void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics) {
    }
    */

}
