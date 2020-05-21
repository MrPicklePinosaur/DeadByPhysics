using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Three : MonoBehaviour
{
    float distance;
    int problem;
    List<float> charges = new List<float>();
    float answer;
    public GameObject textfield;
    public GameObject canvas;
    float k = 9000000000.00f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {

        answer = 0;
        //Generate distance between charges
        distance = Mathf.Round(Random.Range(10.0f,100.0f));
        canvas.transform.GetChild(7).GetComponent<TMP_Text>().text = "Given the distances between adjacent charges is " + distance.ToString() + ".00 m, " +
            "calculate the total electric potential energy of the group of charges.";
        Debug.Log(distance);
        //Generate which shape
        problem = Random.Range(0,4);
        //Generate charges
        //Display the problem
        canvas.transform.GetChild(problem).gameObject.SetActive(true);
        charges = new List<float>();
        for (int i = 0; i< problem + 2;i++)
        {
            float charge = Mathf.Round(Random.Range(-10.0f, 10.0f)*100)/100;
            
            canvas.transform.GetChild(problem).gameObject.transform.GetChild(i).gameObject.GetComponent<TMP_Text>().text = charge.ToString() + "mC";
            charges.Add(charge / 1000000);

        }

        //+kq1q2/r
        if (charges.Count == 2)
        {
            answer = k * charges[0] * charges[1] * (1 / distance);
        }
        else if (charges.Count == 3)
        {
            answer = k * (charges[0]*charges[1] + charges[0]*charges[2] + charges[1]*charges[2]) * (1/distance);
        }
        else if (charges.Count == 4)
        {
            float adjacent = k * (charges[0]*charges[1] + charges[1]*charges[2] + charges[2]*charges[3] + charges[3] * charges[0]) * (1 / distance);
            float across = k * (charges[0]*charges[2] + charges[1] *charges[3]) * (1 / (Mathf.Pow(Mathf.Pow(distance, 2.0f) + Mathf.Pow(distance, 2.0f), 0.5f)));
            answer = adjacent + across;
        }
        else if (charges.Count == 5)
        {
            float adjacent = k * (charges[0]*charges[1] + charges[1]*charges[2] + charges[2]*charges[3] + charges[3]*charges[4] + charges[4]*charges[0]) * (1 / distance);
            float c = Mathf.Pow(2*Mathf.Pow(distance,2.0f)-2*distance*distance*Mathf.Cos(108*(Mathf.PI/180)),0.5f);
            float across = k * (charges[0]*charges[2] + charges[0]*charges[3] + charges[1]*charges[3] + charges[1]*charges[4] + charges[2]*charges[4]) * (1 / (c));
            answer = across + adjacent;
        }

        answer = Mathf.Round(answer*100)/100;
        Debug.Log(answer);
        
        



    }

    public void onSubmit()
    {
        float inp;
        if (float.TryParse(textfield.GetComponent<TMP_InputField>().text, out inp))
        {
            inp = float.Parse(textfield.GetComponent<TMP_InputField>().text);
            if (inp == answer)
            {
                Debug.Log("Correct!");
                Exit();
            }
        }
        
    }

    public void Exit()
    {
        canvas.SetActive(false);
    }

    void OnDisable()
    {
        Cleanup();
    }

    void Cleanup()
    {
        canvas.transform.GetChild(0).gameObject.SetActive(false);
        canvas.transform.GetChild(1).gameObject.SetActive(false);
        canvas.transform.GetChild(2).gameObject.SetActive(false);
        canvas.transform.GetChild(3).gameObject.SetActive(false);

        textfield.GetComponent<TMP_InputField>().text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
