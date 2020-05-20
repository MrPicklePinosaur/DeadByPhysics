using ExitGames.Client.Photon;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EventSystem;
using static PlayerProfile;

public enum PlayerStatus {
    Excellent,
    Satisfactory,
    NeedsImprovement,
    Disconnected
}

//Singleton holding info about current player
public class PlayerInstance : EventListener {

    public static PlayerInstance playerInstance;
    public static int curPlayerNumber = 0;

    public int playerNumber;
    public PlayerStatus playerStatus;

    public GameObject clientAvatar; //reference to the player gameobject

    void Start() {
        base.Start();

        playerInstance = this;

        playerNumber = curPlayerNumber;
        curPlayerNumber += 1;

        playerStatus = PlayerStatus.Excellent;

        clientAvatar = PhotonNetwork.Instantiate("Player",Vector3.zero,Quaternion.identity);
        
    }

    public override void OnEvent(EventData data) {
        object[] payload = (object[])data.CustomData;

        switch (data.Code) {

            //handle taking damage   ONPLAYERHIT
            case (byte)EventCodes.OnPlayerDamageEvent:
                int actorId = (int)payload[0];
                //change state if we own
                if (actorId == playerProfile.player.ActorNumber) {

                }



                
                break;

            //handle player disconnect

        }
    }


}
