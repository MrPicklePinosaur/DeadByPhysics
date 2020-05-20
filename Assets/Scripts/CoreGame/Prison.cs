using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;


//make collider on this gameobject the saving radius
[RequireComponent(typeof(Collider))]
public class Prison : EventListener {

    public int occupiedBy = -1; //the player that is in the prison
    public GameObject trappedPosition;

    int interactingActor;
    List<int> playersInInteractZone;

    private void Start() {
        base.Start();

        playersInInteractZone = new List<int>();

        interactingActor = -1;
    }

    public override void OnEvent(EventData data) {
        
    }

    public void SetOccupant(int actorId) { this.occupiedBy = actorId; }
    public void RemoveOccupant() { this.occupiedBy = -1; }

}
