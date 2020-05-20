using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour {

    public static EventSystem eventSystem;

    private void Awake() {
        EventSystem.eventSystem = this;
    }

    public enum EventCodes {

        //PUN callback codes: 0-29
        OnConnectedEvent = 0,
        OnLeftRoomEvent = 1,
        OnMasterClientSwitchedEvent = 2,
        OnCreateRoomFailedEvent = 3,
        OnJoinRoomFailedEvent = 4,
        OnCreatedRoomEvent = 5,
        OnJoinedLobbyEvent = 6,
        OnLeftLobbyEvent = 7,
        OnDisconnectedEvent = 8,
        OnRegionListReceivedEvent = 9,
        OnRoomListUpdateEvent = 10,
        OnJoinedRoomEvent = 11,
        OnPlayerEnteredRoomEvent = 12,
        OnPlayerLeftRoomEvent = 13,
        OnJoinRandomFailedEvent = 14,
        OnConnectedToMasterEvent = 15,


        //Client event 50-99
        OnOpenGeneratorWindowEvent = 50, //format: { generatorId: int, targetActor: int}
        OnCloseGeneratorWindowEvent = 51, //format: { generatorId: int, targetActor: int}



        //Network events 100-199
        OnChatMessageEvent = 101, //format: { message: string }
        OnPlayerStatusChange = 102, //format: { actorId: int, status: PlayerStatus (int) }

        OnEnterInteractAreaEvent = 111, //used to display an 'E' icon whenever player can interact with sm
        OnExitInteractAreaEvent = 112,

        OnPlayerInteractEvent = 115, //format: { actorId: int, interactable_id: int } fired whenever a player presses the 'E' key
        OnPlayerDamageEvent = 116, //format: { actorId: int, interactable_id: int }

        OnPlayerDeathEvent = 117, //format: { actorId: int }
        OnPlayerReviveEvent = 118, //format: { actorId: int }

        OnStudentInitEvent = 120, //format: { actorId: int }
        OnTeacherInitEvent = 121, //format: { actorId: int }

        //Photon codes: 200-255 (https://doc-api.photonengine.com/en/pun/v2/class_photon_1_1_realtime_1_1_event_code.html)
        AzureNodeInfoEvent = 210,

        LobbyStatsEvent = 224,
        AppStatsEvent = 226,
        MatchEvent = 227,
        QueueStateEvent = 228,
        GameListUpdateEvent = 229,
        GameListEvent = 230,

        LeaveEvent = 254,
        JoinEvent = 255,
        
        
    }

    //public event Func<byte, EventData> EventReceived = code => new EventData() { Code = code };
    public event Action<EventData> EventReceived;

    public void RaiseClientEvent(EventCodes eventCode) {
        EventReceived?.Invoke(new EventData() { Code = (byte)eventCode });
    }


    public void RaiseNetworkEvent(EventCodes eventCode, object[] ctx, ReceiverGroup recievers=ReceiverGroup.All) {
        RaiseEventOptions eventOpt = new RaiseEventOptions() { Receivers = recievers };
        SendOptions sendOpt = new SendOptions() { Reliability = true };
        PhotonNetwork.RaiseEvent((byte)eventCode, ctx, eventOpt, sendOpt);
    }
    public void RaiseNetworkEvent(EventCodes eventCode, ReceiverGroup recievers = ReceiverGroup.All) {
        RaiseNetworkEvent(eventCode, new object[] { }, recievers);
    }


}
