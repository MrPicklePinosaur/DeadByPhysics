
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class One : MonoBehaviour
{
    int current_Q;
    int a;
    int current_F;
    public GameObject First;
    public GameObject Second;

    string[,] answers = new string[,] {
        {"+","+","Single Outwards","+","Like Outwards" },
        {"+","+","Single Outwards","-","Opposite" },
        {"+","-","Single Outwards","+","Like Inwards" },
        { "+", "-", "Single Outwards", "-", "Opposite Inverted" },
        { "-", "+", "Single Inwards", "+", "Opposite"},
        { "-", "+", "Single Inwards", "-", "Like Inwards"},
        { "-", "-", "Single Outwards", "+", "Opposite Inverted"},
        { "-", "-", "Single Outwards", "-", "Like Outwards"} };


    // Start is called before the first frame update
    void Start()
    {
        
        current_Q = 1;
        a = Random.Range(0, 8);
        Debug.Log("The test charge is " + answers[a,1]);
        foreach (Transform child in First.transform)
        {
           if (child.name == answers[a,0])
            {
                child.gameObject.SetActive(true);
                current_F = 0;
                First.transform.GetChild(0).gameObject.SetActive(true);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void change_Field()
    {
        Debug.Log("Change Field");
        if (current_Q == 1)
        {
            First.transform.GetChild(current_F).gameObject.SetActive(false);
            current_F = 1 - current_F;
            First.transform.GetChild(current_F).gameObject.SetActive(true);
        }
        else if (current_Q == 2)
        {
            Second.transform.GetChild(current_F).gameObject.SetActive(false);
            current_F = (current_F + 1) % 4;
            Second.transform.GetChild(current_F).gameObject.SetActive(true);
        }
    }

    public void next_Q()
    {
        current_Q += 1;
        if (First.transform.GetChild(current_F).gameObject.name == answers[a, 2])
        {
            Debug.Log("Next question" + current_Q);
            First.transform.GetChild(current_F).gameObject.SetActive(false);
            foreach (Transform child in Second.transform)
            {
                if (child.name == answers[a, 3])
                {
                    child.gameObject.SetActive(true);
                    current_F = 0;
                    Second.transform.GetChild(current_F).gameObject.SetActive(true);
                }
            }
            
        }
        else
        {
            Debug.Log("Wrong, exit the application.");
        }

        
        
    }

    public void submit_Q()
    {
        if (Second.transform.GetChild(current_F).gameObject.name == answers[a, 4])
        {
            Debug.Log("Correct");

        }
        else
        {
            Debug.Log("Wrong, exit the application.");
        }
    }

}
