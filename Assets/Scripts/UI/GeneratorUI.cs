using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using static EventSystem;

using static PlayerProfile;

public class GeneratorUI : EventListener {

    public GameObject generatorFrame;
    
    void Start() {
        base.Start();

    }


    public override void OnEvent(EventData data) {
        object[] payload = (object[])data.CustomData;

        switch (data.Code) {


            case (byte)EventCodes.OnOpenGeneratorWindowEvent:
                int targetActor = (int)payload[1];
                
                if (targetActor == playerProfile.player.ActorNumber) {
                    int generatorId = (int)payload[0];

                    //pull the right questions and display it
                    generatorFrame.SetActive(true);
                }

                break;

            case(byte)EventCodes.OnCloseGeneratorWindowEvent:
                targetActor = (int)payload[1];

                if (targetActor == playerProfile.player.ActorNumber) {

                    //close generator window
                    generatorFrame.SetActive(false);
                }

                break;
        }
    }

    

}
