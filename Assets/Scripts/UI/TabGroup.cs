using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour {

    public List<TabButton> tabButtons;
    public List<GameObject> tabFrames;

    int curFrameIndex = 0;

    private void Start() {
        tabButtons = new List<TabButton>();
        tabFrames = new List<GameObject>();
    }

    public void Subscribe(TabButton button, GameObject frame) {
        tabButtons.Add(button);
        tabFrames.Add(frame);
    }

    public void OnTabEnter(TabButton button) {

    }
    public void OnTabSelected(TabButton button) {

    }
    public void OnTabExit(TabButton button) {

    }

}
