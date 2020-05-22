using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EventSystem;
using static PlayerProfile;
using static GameManager;

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
                eventSystem.RaiseNetworkEvent(EventCodes.OnStudentInitEvent, new object[] { playerList[i].ActorNumber, i });

                //teleport player
            }
            //tell teacher to init
            eventSystem.RaiseNetworkEvent(EventCodes.OnTeacherInitEvent, new object[] { playerList[teacherInd].ActorNumber, 4 });
        }

    }

    public void InitStudent(int spawnId) {
        
        Vector3 spawnPos = gameManager.FindSpawnpointById(spawnId).transform.position;
        Debug.Log($"Initing student at {spawnPos}");
        clientAvatar = PhotonNetwork.Instantiate("Players/Student", spawnPos, Quaternion.identity);
        playerStatus = PlayerStatus.Excellent;
        isTeacher = false;

        //teleport player to spawn
        

    }

    public void InitTeacher(int spawnId) {
        
        //init teacher
        Vector3 spawnPos = gameManager.FindSpawnpointById(spawnId).transform.position;
        Debug.Log($"Initing teacher at {spawnPos}");
        clientAvatar = PhotonNetwork.Instantiate("Players/Teacher", spawnPos, Quaternion.identity);
        //not playerstatus is not inited for teacher
        isTeacher = true;


    }

    public override void OnEvent(EventData data) {
        object[] payload = (object[])data.CustomData;

        switch (data.Code) {

            //init student
            case (byte)EventCodes.OnStudentInitEvent:
                int actorId = (int)payload[0];
                int spawnId = (int)payload[1];

                if (actorId == playerProfile.player.ActorNumber) {
                    InitStudent(spawnId);
                }

                break;

            case (byte)EventCodes.OnTeacherInitEvent:

                actorId = (int)payload[0];
                spawnId = (int)payload[1];

                if (actorId == playerProfile.player.ActorNumber) {
                    InitTeacher(spawnId);
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
            case (byte)EventCodes.OnPlayerReviveEvent:
                actorId = (int)payload[0];

                if (actorId == playerProfile.player.ActorNumber) {
                    playerStatus = PlayerStatus.Excellent;
                } 
                break;




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
                eventSystem.RaiseNetworkEvent(EventCodes.OnPlayerDeathEvent, new object[] { playerProfile.player.ActorNumber });

                break;
        }
    }



}
