using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitObjectHere : MonoBehaviour {

    void Start() {
        GameObject newObj = PhotonNetwork.Instantiate(this.name,transform.position,Quaternion.identity);
        newObj.transform.SetParent(transform.parent);
        Destroy(this.gameObject);
    }
    
}
