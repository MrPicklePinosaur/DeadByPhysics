using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MatchMaking;

public class MainMenuManager : MonoBehaviour {

    public List<GameObject> frames;

    public void OnJoinGameButtonClicked() {
        matchMaking.JoinRoom("");
    }

}
