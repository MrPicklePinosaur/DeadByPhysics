using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using static EventSystem;

using static PlayerProfile;
using static GameManager;

public class GeneratorUI : EventListener {

    public static GeneratorUI generatorUI;
    public GameObject generatorFrame;

    GameObject openedQuestion;
    
    void Start() {
        base.Start();

        generatorUI = this;

    }


    public override void OnEvent(EventData data) {
        object[] payload = (object[])data.CustomData;

        switch (data.Code) {


            case (byte)EventCodes.OnOpenGeneratorWindowEvent:
                int targetActor = (int)payload[1];
                
                if (targetActor == playerProfile.player.ActorNumber) {
                    int generatorId = (int)payload[0];

                    OnOpenWindow(generatorId);
                }

                break;

            case(byte)EventCodes.OnCloseGeneratorWindowEvent:
                targetActor = (int)payload[1];

                if (targetActor == playerProfile.player.ActorNumber) {

                    OnCloseWindow();
                }

                break;
        }
    }

    void OnOpenWindow(int interactable_id) {
        //pull the right questions and display it
        generatorFrame.SetActive(true);

        Generator cur_gen = gameManager.FindGeneratorById(interactable_id);
        int question = cur_gen.questions_remaining[0];

        generatorFrame.transform.GetChild(question).gameObject.SetActive(true);


    }

    public void OnCloseWindow() {
        

        //close generator window
        generatorFrame.SetActive(false);
    }


}
