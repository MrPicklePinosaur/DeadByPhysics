using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prison : MonoBehaviour {

    public int occupiedBy = -1; //the player that is in the prison
    public GameObject trappedPosition; 
    
    public void SetOccupant(int actorId) { this.occupiedBy = actorId; }
    public void RemoveOccupant() { this.occupiedBy = -1; }

}
