/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class One : MonoBehaviour
{
    int current_Q;
    int a;
    public GameObject First;
    public GameObject Second;

    string[,] answers = new string[,] {
        {"+","+","Single Outwards","+","Like Outwards" },
        {"+","+","Single Outwards","-","Opposite" },
        {"+","-","Single Outwards","+","Like Inwards" },
        { "+", "-", "Single Outwards", "-", "Opposite Inverted" },
        { "-", "+", "Single Inwards Negative", "+", "Opposite"},
        { "-", "+", "Single Inwards Negative", "-", "Like Outwards"},
        { "-", "-", "Single Outwards Negative", "+", "Opposite Inverted"},
        { "-", "-", "Single Outwards Negative", "-", "Like Inwards"} };


    // Start is called before the first frame update
    void Start()
    {
        
        current_Q = 1;
        a = Random.Range(0, 8);
        Debug.Log("The test charge is " + answers[a][1]);
        foreach (Transform child in First)
        {
           if (child.name == answers[a][0])
            {
                child.GameObject.SetActive(true);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void change_Field()
    {
        Debug.Log("Anish sucks butt");
    }

    public void next_Q()
    {
        current_Q += 1;
        Debug.Log("Next question" + current_Q);
        
    }

    public void submit_Q()
    {
        Debug.Log("Submit and check.");
    }

}
*/