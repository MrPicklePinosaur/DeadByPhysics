using ExitGames.Client.Photon;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EventSystem;

public abstract class EventListener : MonoBehaviour {

    //PROBLEM: it seems like the onEvent is refering to the instance in this class, and not working for any in the child classes,
    //so no events are actually triggering
    protected virtual void Start() {
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
        eventSystem.EventReceived += OnEvent;
    }

    public abstract void OnEvent(EventData data);

    protected virtual void OnDestroy() {
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
        eventSystem.EventReceived -= OnEvent;
    }
}
