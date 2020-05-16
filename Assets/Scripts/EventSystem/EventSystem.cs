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
        //Client events 
        TESTEVENT = 1


        //Network events



        //Photon codes: 200-255

    }

    //public event Func<byte, EventData> EventReceived = code => new EventData() { Code = code };
    public event Action<EventData> EventReceived;

    public void RaiseClientEvent(EventCodes eventCode) {
        EventReceived?.Invoke(new EventData() { Code = (byte)eventCode });
    }

    public void RaiseNetworkEvent(EventCodes eventCode) {
        RaiseEventOptions eventOpt = new RaiseEventOptions() { Receivers = ReceiverGroup.All };
        SendOptions sendOpt = new SendOptions() { Reliability = true };
        PhotonNetwork.RaiseEvent((byte)eventCode, new object[] { }, eventOpt, sendOpt);
    }


    //PhotonNetwork.NetworkingClient.EventRecievend += OnEvent
    //eventSystem.EventRecieved += OnEvent




}
