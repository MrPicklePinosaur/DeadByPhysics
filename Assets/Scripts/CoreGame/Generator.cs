using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;

using static EventSystem;
using static PlayerProfile;

//NOTE, make the generator have a trigger collision to act as the activation range, and make
// a seperate collider as a child for the actual physical collider
[RequireComponent(typeof(Collider))]
public class Generator : EventListener {

    Collider col;

    bool isBeingUsed;
    List<int> playersInInteractZone; //players who are within interaction range

    void Start() {
        base.Start();

        col = GetComponent<Collider>();
        playersInInteractZone = new List<int>();
    }

    public override void OnEvent(EventData data) {
        object[] payload = (object[])data.CustomData;

        //catch onenterinteractevent and onplayerinteractevent and exit
        switch (data.Code) {

            case (byte)EventCodes.OnEnterInteractAreaEvent:
                //add player to list of players in zone
                int actorId = (int)payload[0];
                if (!playersInInteractZone.Contains(actorId)) playersInInteractZone.Add(actorId);
                break; 

            case (byte)EventCodes.OnExitInteractAreaEvent: 
                actorId = (int)payload[0];
                if (playersInInteractZone.Contains(actorId)) playersInInteractZone.Remove(actorId);
                break; 

            case (byte)EventCodes.OnPlayerInteractEvent:
                //check to see if player who pressed interact is within interact zone, if so raise successful interaction event
                //NOTE: this might be a problem if a player is in two interact zones at once, as it will trigger both
                actorId = (int)payload[0];
                if (playersInInteractZone.Contains(actorId)) {
                    //Raise successful interact event here
                }

                break;
        }
    }

    private void OnTriggerEnter(Collider other) {

        //NOTE: TEMP USING THIS PLAYERCONTROLLER
        NetworkPlayerController cont = other.gameObject.GetComponent<NetworkPlayerController>();
        if (cont != null && cont.view.IsMine) { //if player that entered is owned by this session
            eventSystem.RaiseNetworkEvent(EventCodes.OnEnterInteractAreaEvent, new object[] { playerProfile.player.ActorNumber });
        }
    }

    private void OnTriggerExit(Collider other) {

        NetworkPlayerController cont = other.gameObject.GetComponent<NetworkPlayerController>();
        if (cont != null && cont.view.IsMine) { //if player that entered is owned by this session
            eventSystem.RaiseNetworkEvent(EventCodes.OnExitInteractAreaEvent, new object[] { playerProfile.player.ActorNumber });
        }
    }

}
