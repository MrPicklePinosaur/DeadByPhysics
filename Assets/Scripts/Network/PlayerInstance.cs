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

    public static PlayerInstance gameManager;

    //client owner info
    public GameObject clientAvatar; //reference to the player gameobject
    public PlayerStatus playerStatus;
    public bool isTeacher;

    void Start() {
        base.Start();

        gameManager = this;

        //client specific info
        clientAvatar = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);

        playerStatus = PlayerStatus.Excellent;

    }

    public override void OnEvent(EventData data) {
        object[] payload = (object[])data.CustomData;

        switch (data.Code) {

            //handle taking damage   ONPLAYERHIT
            case (byte)EventCodes.OnPlayerDamageEvent:
                int actorId = (int)payload[0];

                //we only care if we own
                if (actorId != playerProfile.player.ActorNumber) break;

                //update current status


                //send message out to everyone to update status on ui
                eventSystem.RaiseNetworkEvent(EventCodes.OnPlayerStatusChange, new object[] { actorId, playerStatus });
                
                break;

            //handle player disconnect, send message out to update status to disconnected

        }
    }


}
