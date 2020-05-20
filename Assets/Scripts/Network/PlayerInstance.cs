using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
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

    //client owner info
    public GameObject clientAvatar; //reference to the player gameobject
    public PlayerStatus playerStatus;
    public bool isTeacher;

    void Start() {
        base.Start();

        playerInstance = this;

        if (PhotonNetwork.IsMasterClient) {

            //choose a player to be the teacher
            Player[] playerList = PhotonNetwork.PlayerList;

            int teacherInd = Random.Range(0, playerList.Length);
            for (var i = 0; i < playerList.Length; i++) {
                if (i == teacherInd) continue;

                //tell player to init
                eventSystem.RaiseNetworkEvent(EventCodes.OnStudentInitEvent, new object[] { playerList[i].ActorNumber });
            }
            //tell teacher to init
            eventSystem.RaiseNetworkEvent(EventCodes.OnTeacherInitEvent, new object[] { playerList[teacherInd].ActorNumber });
        }

    }

    public void InitStudent() {
        Debug.Log("Initing student");
        clientAvatar = PhotonNetwork.Instantiate("Players/Student", Vector3.zero, Quaternion.identity);
        playerStatus = PlayerStatus.Excellent;
        isTeacher = false;
    }

    public void InitTeacher() {
        Debug.Log("Initing teacher");
        //init teacher

        clientAvatar = PhotonNetwork.Instantiate("Players/Teacher", Vector3.zero, Quaternion.identity);
        //not playerstatus is not inited for teacher
        isTeacher = true;
    }

    public override void OnEvent(EventData data) {
        object[] payload = (object[])data.CustomData;

        switch (data.Code) {

            //init student
            case (byte)EventCodes.OnStudentInitEvent:
                int actorId = (int)payload[0];

                if (actorId == playerProfile.player.ActorNumber) {
                    InitStudent();
                }

                break;

            case (byte)EventCodes.OnTeacherInitEvent:

                actorId = (int)payload[0];

                if (actorId == playerProfile.player.ActorNumber) {
                    InitTeacher();
                }

                break;

            //handle taking damage   ONPLAYERHIT
            case (byte)EventCodes.OnPlayerDamageEvent:
                actorId = (int)payload[0];

                //we only care if we own
                if (actorId != playerProfile.player.ActorNumber) return;

                TakeDamage();

                //send message out to everyone to update status on ui
                eventSystem.RaiseNetworkEvent(EventCodes.OnPlayerStatusChange, new object[] { actorId, playerStatus });
                
                break;

            //handle player disconnect, send message out to update status to disconnected


            //handle player 'death'


        }
    }

    public void TakeDamage() {
        //update current status

        switch (playerStatus) {

            case PlayerStatus.Excellent:
                playerStatus = PlayerStatus.Satisfactory;
                break;
            case PlayerStatus.Satisfactory:
                playerStatus = PlayerStatus.NeedsImprovement;
                //trigger 'death' here
                eventSystem.RaiseNetworkEvent(EventCodes.OnPlayerDeathEvent, new object[] { PhotonNetwork.LocalPlayer.ActorNumber });

                break;
        }
    }


}
