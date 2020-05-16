using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {

    public GameObject currentFrame;

    public void SwitchFrame(GameObject frame) {

        if (currentFrame != null) {
            currentFrame.SetActive(false);
        }

        frame.SetActive(true);
        currentFrame = frame;
    }


}
