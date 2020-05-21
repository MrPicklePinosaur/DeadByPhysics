
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerProfile;
using static EventSystem;
using static GeneratorUI;
public class One : MonoBehaviour
{
    int current_Q;
    int a;
    int current_F;
    public GameObject First;
    public GameObject Second;
    public GameObject problem;

    string[,] answers = new string[,] {
        {"+","+","Single Outwards","+","Like Outwards" },
        {"+","+","Single Outwards","-","Opposite Inverted" },
        {"+","-","Single Inwards","+","Like Inwards" },
        { "+", "-", "Single Inwards", "-", "Opposite" },
        { "-", "+", "Single Inwards", "+", "Opposite"},
        { "-", "+", "Single Inwards", "-", "Like Inwards"},
        { "-", "-", "Single Outwards", "+", "Opposite Inverted"},
        { "-", "-", "Single Outwards", "-", "Like Outwards"} };


    // Start is called before the first frame update
    void OnEnable()
    {
        
        current_Q = 1;
        a = Random.Range(0, 8);
        Debug.Log("The test charge is " + answers[a,1]);
        //6 and 7
        if(answers[a,1] == "-")
        {
            problem.transform.GetChild(6).gameObject.SetActive(true);
        }
        else
        {
            problem.transform.GetChild(7).gameObject.SetActive(true);
        }
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

    void OnDisable()
    {
        Cleanup();
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
            problem.transform.GetChild(3).gameObject.SetActive(false);
            problem.transform.GetChild(4).gameObject.SetActive(true);

        }
        else
        {
            Debug.Log("Wrong, try again.");

            Cleanup();
            OnEnable();
        }
    }

    public void submit_Q()
    {
        if (Second.transform.GetChild(current_F).gameObject.name == answers[a, 4])
        {
            Debug.Log("Correct");

            problem.SetActive(false);

        }
        else
        {
            Debug.Log("Wrong, try again");

            Cleanup();
            OnEnable();
        }
    }

    void Cleanup()
    {
        foreach (Transform child in Second.transform)
        {
            child.gameObject.SetActive(false);
        }
        foreach (Transform child in First.transform)
        {
            child.gameObject.SetActive(false);
                
        }
        problem.transform.GetChild(3).gameObject.SetActive(true);
        problem.transform.GetChild(4).gameObject.SetActive(false);
        problem.transform.GetChild(6).gameObject.SetActive(false);
        problem.transform.GetChild(7).gameObject.SetActive(false);
        Debug.Log("Cleaned");

    }

    public void Exit() {
        generatorUI.OnCloseWindow();
        problem.SetActive(false);
    }

}
