using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    Generator[] generators;
    Prison[] prisons;

    void Start() {
        generators = FindObjectsOfType<Generator>();
        prisons = FindObjectsOfType<Prison>();
    }




    
}
