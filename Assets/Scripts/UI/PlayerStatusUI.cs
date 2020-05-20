using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class PlayerStatusUI : EventListener {

    void Start() {
        base.Start();
    }

    public override void OnEvent(EventData data) {
        
        switch (data.Code) {

            

        }
    }

    /*

    public void SetStatus(int playerNumber, PlayerStatus status) {
 
        GameObject playerFrame = transform.GetChild(playerNumber).gameObject;

        //remove old status

        //instantiate new status
        switch (status) {
            case PlayerStatus.Excellent:
                break;
            case PlayerStatus.Satisfactory:
                break;
            case PlayerStatus.NeedsImprovement:
                break;
            case PlayerStatus.Disconnected:
                break;
            default:
                Debug.LogWarning("Invalid player status");
                break;
        } 

    }
    */

}
