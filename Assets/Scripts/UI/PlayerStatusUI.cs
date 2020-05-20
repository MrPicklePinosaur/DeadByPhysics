using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using static EventSystem;

public class PlayerStatusUI : EventListener {

    //keep track of actorIDs
    public int[] actorNumbers = new int[] { -1, -1, -1, -1 };

    void Start() {
        base.Start();

        //prepopulate
        //foreach connected player (not teacher), fill actorNumber array


        //set usernames and status for each player

    }

    public override void OnEvent(EventData data) {
        object[] payload = (object[])data.CustomData;

        switch (data.Code) {

            //catch update status 
            case (byte)EventCodes.OnPlayerStatusChange:
                int actorId = (int)payload[0];
                PlayerStatus playerStatus = (PlayerStatus)payload[1];
                UpdatePlayerStatusUI(actorId, playerStatus);

                break;
        }
    }

    public void UpdatePlayerStatusUI(int actorId, PlayerStatus playerStatus) {

    }

}
