using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;

using static EventSystem;

public class Generator : InteractableObject {

    public List<int> questions_remaining;

    private void Start() {
        base.Start();

        //init order of questions
        questions_remaining = new List<int> { 0, 1, 2, 3 };
        questions_remaining.Shuffle();

    }

    public override void OnInteract(int actorId) {
        //dont do anything if theres no more questionsa
        if (questions_remaining.Count == 0) { return; }

        eventSystem.RaiseNetworkEvent(EventCodes.OnOpenGeneratorWindowEvent, new object[] { interactable_id, actorId });
    }

    public override void OnUninteract(int actorId) {

        eventSystem.RaiseNetworkEvent(EventCodes.OnCloseGeneratorWindowEvent, new object[] { interactable_id, actorId });


    }

    public override void OnEvent(EventData data) {
        base.OnEvent(data);

        object[] payload = (object[])data.CustomData;
        switch (data.Code) {
            case (byte)EventCodes.OnCorrectAnswer:

                int gen_id = (int)payload[0];
                if (gen_id == interactable_id) {
                    //we finished the question, so we can remove the first question
                    Debug.Log($"finished question {questions_remaining[0]}!!");
                    questions_remaining.RemoveAt(0);

                    //check to see if there are no questions left, if so, the gen is done

                }
                break;

        }
    }

}
