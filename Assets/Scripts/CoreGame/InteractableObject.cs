﻿using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;

using static EventSystem;
using static PlayerProfile;

//NOTE: lmao, the eventsystem doesnt identify which generator the player is entering, add some sort of id to each interactable object

//NOTE, make the generator have a trigger collision to act as the activation range, and make
// a seperate collider as a child for the actual physical collider
[RequireComponent(typeof(Collider))]
public abstract class InteractableObject : EventListener {

    public int interactable_id;

    Collider col;

    [SerializeField]protected int interactingActor = -1; //the actor that is interacting with the generator at this point (-1 signifies no one interacting)
    [SerializeField] List<int> playersInInteractZone; //players who are within interaction range

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
                int interact_id = (int)payload[1];

                if (interact_id != this.interactable_id) { break; }
                if (!playersInInteractZone.Contains(actorId)) playersInInteractZone.Add(actorId);
                //Debug.Log($"{actorId} entered interactible object number {interactable_id}");
                break;

            case (byte)EventCodes.OnExitInteractAreaEvent:
                actorId = (int)payload[0];
                interact_id = (int)payload[1];

                if (interact_id != this.interactable_id) { break; }
                if (playersInInteractZone.Contains(actorId)) playersInInteractZone.Remove(actorId);
                //Debug.Log($"{actorId} left interactible object number {interactable_id}");

                break;

            case (byte)EventCodes.OnPlayerInteractEvent:
                //check to see if player who pressed interact is within interact zone, if so raise successful interaction event
                //NOTE: this might be a problem if a player is in two interact zones at once, as it will trigger both
                actorId = (int)payload[0];
                if (playersInInteractZone.Contains(actorId) && interactingActor == -1) {

                    //if no one is interacting, set the current interacting actor
                    interactingActor = actorId;

                    //Raise successful interact event here
                    if (playerProfile.player.ActorNumber == actorId) {
                        OnInteract(actorId);

                        Debug.Log($"{actorId} is INTERACTING!! with interactible id {interactable_id}");
                        
                    } 

                }

                break;

            case (byte)EventCodes.OnPlayerUninteractEvent:
                actorId = (int)payload[0];
                if (interactingActor == actorId) {

                    interactingActor = -1;

                    if (playerProfile.player.ActorNumber == actorId) {
                        OnUninteract(actorId);
                    }

                }

                break;
        }

    }

    public abstract void OnInteract(int actorId);
    //TODO, make an alternate onuninteract
    public abstract void OnUninteract(int actorId);

    private void OnTriggerEnter(Collider other) {

        //NOTE: TEMP USING THIS PLAYERCONTROLLER
        NetworkPlayerController cont = other.gameObject.GetComponent<NetworkPlayerController>();
        if (cont != null && cont.view.IsMine) { //if player that entered is owned by this session
            eventSystem.RaiseNetworkEvent(EventCodes.OnEnterInteractAreaEvent, new object[] { playerProfile.player.ActorNumber, interactable_id });
        }
    }

    private void OnTriggerExit(Collider other) {

        NetworkPlayerController cont = other.gameObject.GetComponent<NetworkPlayerController>();
        if (cont != null && cont.view.IsMine) { //if player that entered is owned by this session
            eventSystem.RaiseNetworkEvent(EventCodes.OnExitInteractAreaEvent, new object[] { playerProfile.player.ActorNumber, interactable_id });
        }
    }

}