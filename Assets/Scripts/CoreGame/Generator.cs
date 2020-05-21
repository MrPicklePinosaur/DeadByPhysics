using System.Collections;
using System.Collections.Generic;
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

}
