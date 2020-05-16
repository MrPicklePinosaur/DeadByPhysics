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
        


        //Network events


    }

    public event Func<byte, object[], EventData> EventRecieved;

    public void RaiseClientEvent(byte eventCode) {
        RaiseClientEvent(eventCode, new object[] { });
    }
    public void RaiseClientEvent(byte eventCode, object[] context) {
        EventRecieved?.Invoke(eventCode,context);
    }

    public void RaiseNetworkEvent(byte eventCode) {
        RaiseNetworkEvent(eventCode, new object[] { });
    }
    public void RaiseNetworkEvent(byte eventCode, object[] context) {
        RaiseEventOptions eventOpt = new RaiseEventOptions() { Receivers = ReceiverGroup.All };
        SendOptions sendOpt = new SendOptions() { Reliability = true };
        PhotonNetwork.RaiseEvent(eventCode, context, eventOpt, sendOpt);
    }


    //PhotonNetwork.NetworkingClient.EventRecievend += OnEvent




}
