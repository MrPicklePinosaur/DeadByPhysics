using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelGroup : MonoBehaviour {

    public GameObject[] panels;

    public int curPanelIndex;

    private void Awake() {
        ShowCurrentPanel();
    }
    void ShowCurrentPanel() {

        for (int i = 0; i < panels.Length; i++) {

            panels[i].gameObject.SetActive(i==curPanelIndex);
        }
    }

    public void SetPanelIndex(int ind) {
        curPanelIndex = ind;
        curPanelIndex = Mathf.Clamp(curPanelIndex, 0, panels.Length-1);
        ShowCurrentPanel();
    }

}
