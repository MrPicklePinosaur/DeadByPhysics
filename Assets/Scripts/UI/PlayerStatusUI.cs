using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using static EventSystem;

using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class PlayerStatusUI : EventListener {

    //keep track of actorIDs
    public List<int> actorNumbers;

    void Start() {
        base.Start();

        actorNumbers = new List<int>();

    }

    public override void OnEvent(EventData data) {
        object[] payload = (object[])data.CustomData;
        
        switch (data.Code) {

            //catch inits
            case (byte)EventCodes.OnStudentInitEvent:
                int actorId = (int)payload[0];
                actorNumbers.Add(actorId);

                //also set username
                SetPlayerUsername(actorId);

                //refresh ui
                eventSystem.RaiseNetworkEvent(EventCodes.OnPlayerStatusChange, new object[] { actorId, PlayerStatus.Excellent });

                break;

            //catch update status 
            case (byte)EventCodes.OnPlayerStatusChange:
                actorId = (int)payload[0];
                PlayerStatus playerStatus = (PlayerStatus)payload[1];
                UpdatePlayerStatusUI(actorId, playerStatus);

                break;
        }
    }

    public void SetPlayerUsername(int actorId) {
        //get index of element by actor number
        int uiInd = actorNumbers.IndexOf(actorId);
        GameObject playerStatusUI = transform.GetChild(uiInd).gameObject;

        TMP_Text username_text = GetComponentInChildren<TMP_Text>();
        username_text.text = GetPlayerByActorID(actorId).NickName;
    }

    public void UpdatePlayerStatusUI(int actorId, PlayerStatus playerStatus) {
        Debug.Log($"Updating status to {playerStatus}");

        int uiInd = actorNumbers.IndexOf(actorId);
        GameObject playerStatusUI = transform.GetChild(uiInd).gameObject;

        //TEMP right now
        Image image = GetComponentInChildren<Image>();

        switch(playerStatus) {
            case PlayerStatus.Excellent:
                image.color = Color.green;
                break;
            case PlayerStatus.Satisfactory:
                image.color = Color.yellow;
                break;
            case PlayerStatus.NeedsImprovement:
                image.color = Color.red;
                break;
            case PlayerStatus.Disconnected:
                image.color = Color.gray;
                break;
        } 
    }

    public static Player GetPlayerByActorID(int id) {
        foreach (Player p in PhotonNetwork.PlayerList) {
            if (p.ActorNumber == id) {
                return p;
            }
        }
        return null;
    } 
}
