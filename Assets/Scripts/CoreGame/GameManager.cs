using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager gameManager;

    Generator[] generators;
    Prison[] prisons;

    [SerializeField]int generatorsFinished = 0;

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

    public Generator FindGeneratorById(int id) {
        foreach(Generator g in generators) {
            if (g.interactable_id == id) {
                return g;
            }
        }
        return null;
    }

    public void FinishedGenerator() {
        generatorsFinished += 1;

        if (generatorsFinished >= 4) {
            //trigger win state

            Debug.Log("WIN YAYYYYYYYYY");
        }
    }

    
}
