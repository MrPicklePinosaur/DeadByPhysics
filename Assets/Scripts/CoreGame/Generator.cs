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

    static int curGenId = 0;
    public int generatorId;

    Collider col;

    int interactingActor; //the actor that is interacting with the generator at this point (-1 signifies no one interacting)
    List<int> playersInInteractZone; //players who are within interaction range

    void Start() {
        base.Start();

        generatorId = curGenId;
        curGenId += 1;

        col = GetComponent<Collider>();
        playersInInteractZone = new List<int>();

        interactingActor = -1;
    }

    public override void OnEvent(EventData data) {
        object[] payload = (object[])data.CustomData;

        //catch onenterinteractevent and onplayerinteractevent and exit
        switch (data.Code) {

            case (byte)EventCodes.OnEnterInteractAreaEvent:
                //add player to list of players in zone
                int actorId = (int)payload[0];
                if (!playersInInteractZone.Contains(actorId)) playersInInteractZone.Add(actorId);
                Debug.Log($"{actorId} entered");
                break; 

            case (byte)EventCodes.OnExitInteractAreaEvent: 
                actorId = (int)payload[0];
                if (playersInInteractZone.Contains(actorId)) playersInInteractZone.Remove(actorId);
                Debug.Log($"{actorId} left");
                break; 

            case (byte)EventCodes.OnPlayerInteractEvent:
                //check to see if player who pressed interact is within interact zone, if so raise successful interaction event
                //NOTE: this might be a problem if a player is in two interact zones at once, as it will trigger both
                actorId = (int)payload[0];
                if (playersInInteractZone.Contains(actorId)) {

                    //if no one is interacting, set the current interacting actor
                    if (interactingActor == -1) {
                        interactingActor = actorId;

                        //Raise successful interact event here
                        if (playerProfile.player.ActorNumber == actorId) {
                            eventSystem.RaiseNetworkEvent(EventCodes.OnOpenGeneratorWindowEvent, new object[] { generatorId, actorId });
                        }
                        
                        Debug.Log($"{actorId} is INTERACTING!!");

                    } else if (interactingActor == actorId) { //if person who pressed interact key is already interacting, uninteract
                        interactingActor = -1;

                        //Raise un interact event
                        if (playerProfile.player.ActorNumber == actorId) {
                            eventSystem.RaiseNetworkEvent(EventCodes.OnCloseGeneratorWindowEvent, new object[] { generatorId, actorId });
                        }
                        Debug.Log($"{actorId} stopped INTERACTING!!");
                    }

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
