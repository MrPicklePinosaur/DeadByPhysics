using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;

using static EventSystem;
//make collider on this gameobject the saving radius
[RequireComponent(typeof(Collider))]
public class Prison : InteractableObject {

    public int occupiedBy = -1; //the player that is in the prison
    public GameObject trappedPosition;

    public override void OnInteract(int actorId) {

        if (occupiedBy == -1) { return; } //do nothing if there is no one worth saving
        Debug.Log($"actor {actorId} is Saving friend {occupiedBy}");

        eventSystem.RaiseNetworkEvent(EventCodes.OnPlayerReviveEvent, new object[] { occupiedBy });
        RemoveOccupant();

        interactingActor = -1; //lmao xdd (makes it so this event is not toggleable like the generator)

    }

    public override void OnUninteract(int actorId) {

    }

    public void SetOccupant(int actorId) { this.occupiedBy = actorId; }
    public void RemoveOccupant() { this.occupiedBy = -1; }

}
