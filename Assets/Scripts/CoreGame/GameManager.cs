using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager gameManager;

    Generator[] generators;
    Prison[] prisons;

    void Start() {
        gameManager = this;

        generators = FindObjectsOfType<Generator>();
        prisons = FindObjectsOfType<Prison>();
    }


    public Prison FindOpenPrison() {
        foreach(Prison p in prisons) {
            if (p.occupiedBy == -1) {
                return p;
            }
        }
        return null;
    }


    
}
