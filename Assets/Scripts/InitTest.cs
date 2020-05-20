using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitTest : MonoBehaviour {

    static int curId = 0;
    int id;

    void Start() {
        id = curId;
        curId += 1;

        Debug.Log($"Inited object number {this.name}");
    }

    
}
