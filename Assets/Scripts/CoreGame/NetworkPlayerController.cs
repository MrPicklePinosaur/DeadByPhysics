﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static PlayerProfile;
using static EventSystem;
using static PlayerInstance;
using static GameManager;

using ExitGames.Client.Photon;

[RequireComponent(typeof(PhotonView),typeof(PhotonAnimatorView),typeof(Animator))]
public class NetworkPlayerController : EventListener {

    public PhotonView view;
    protected Animator anim;
    public float moveSpeed;

    public bool isTrapped;

    void Start() {
        base.Start();

        view = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();

        isTrapped = false;
    }

    void Update() {

        if (!view.IsMine) return;

        //interact
        if (Input.GetKeyDown(KeyCode.E)) {
            eventSystem.RaiseNetworkEvent(EventCodes.OnPlayerInteractEvent, new object[] { playerProfile.player.ActorNumber });
        }


        if (!isTrapped) {
            HandleMovement();
        }

    }


    protected void HandleMovement() {
        //sprinting
        var sprintMult = 1;
        if (Input.GetKey(KeyCode.LeftShift)) {
            sprintMult *= 2;
        }

        Vector2 inp = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        Vector3 move = new Vector3(inp.x, 0, inp.y) * moveSpeed * sprintMult;
        transform.position += move * Time.deltaTime;

        //animation stuff
        anim.SetFloat("Forward/Backward Speed", move.z);
        anim.SetFloat("SideToSide Speed", move.x);
    }

    public override void OnEvent(EventData data) {
        object[] payload = (object[])data.CustomData;

        switch (data.Code) {

            case (byte)EventCodes.OnPlayerDeathEvent:

                int actorId = (int)payload[0];
                Debug.Log($"{actorId} is dead");

                HandleDeath(actorId);


                break;
            case (byte)EventCodes.OnPlayerReviveEvent:
                actorId = (int)payload[0];
                HandleRevive(actorId);
                Debug.Log($"{actorId} is revived");

                break;
        }
    }

    public void HandleDeath(int actorId) {

        //choose prison to place player in
        Prison prison = gameManager.FindOpenPrison();
        prison.SetOccupant(playerProfile.player.ActorNumber);

        if (actorId == playerProfile.player.ActorNumber) {
            isTrapped = true; //disable movement
            //teleport player to prison
            transform.position = prison.trappedPosition.transform.position;
        }
        
        //do sm with animation


    }

    public void HandleRevive(int actorId) {

        if (actorId == playerProfile.player.ActorNumber) {
            isTrapped = false;
        }

        //do sm with animation
    }
}
