using ExitGames.Client.Photon;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EventSystem;

public class EventListener : MonoBehaviour {

    protected virtual void Start() {
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
        eventSystem.EventReceived += OnEvent;
    }

    public virtual void OnEvent(EventData data) { }

    protected virtual void OnDestroy() {
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
        eventSystem.EventReceived -= OnEvent;
    }
}
