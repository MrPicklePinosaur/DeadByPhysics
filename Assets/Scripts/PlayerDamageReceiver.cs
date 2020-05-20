using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageReceiver : MonoBehaviour
{
    // Start is called before the first frame update
    public int curPlayerIndex = 0;

    public GameObject playerStatuses;
    public GameObject possStatuses;
    private List<string> statuses = new List<string> { "Disconnected","Needs Improvement","Satisfactory","Excellent"};
    public int curStatus = 3;


    // Update is called once per frame

    public void Hit()
    {
        if (curStatus > 1)
        {
            curStatus--;
            if (curStatus == 1)
            {
                Debug.Log("Go To Detention Center");
            }
            UpdateCurrentStatus();
        }
        
    }
    private void UpdateCurrentStatus()
    {
        GameObject holder = playerStatuses.transform.GetChild(curPlayerIndex).gameObject;
        GameObject prevStatus = holder.transform.GetChild(1).gameObject;
        Destroy(prevStatus);
        Instantiate(possStatuses.transform.Find(statuses[curStatus]).gameObject, holder.transform);
        GameObject newStatus = holder.transform.GetChild(holder.transform.childCount-1).gameObject;
        Debug.Log(holder.transform.childCount);
        newStatus.transform.localPosition = new Vector3(0, 0, 0);
        newStatus.SetActive(true);
    }
}
