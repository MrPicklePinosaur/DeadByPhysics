using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using static EventSystem;
using static PlayerProfile;
using static GeneratorUI;
public class Four : MonoBehaviour
{
    public GameObject canvas;
    int answer;
    public GameObject textfield;
    public TMP_Text info;

    float mass;
    float gravity = 9.81f;
    float distance;
    float pd;
    float electroncharge = 1.602f * Mathf.Pow(10,-19);
    float charge;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    void OnEnable()
    {
        //Generate the variables
        answer = Random.Range(1, 12);
        charge = answer * electroncharge;
        pd = Random.Range(100, 1000);
        mass = Random.Range(10, 50) * Mathf.Pow(10, -16);
        distance = charge * pd * (1 / mass) * (1 / gravity);
        info.text = $"An oil droplet of mass {mass/Mathf.Pow(10,-15)} X 10^-15 kg, suspended between two parallel plates {distance*100} cm apart, " +
            $"remains stationary when the potential difference between the plates is {pd} V. How many excess or deficit electrons does it have?";
        Debug.Log("Text changed");
    }

    public void onSubmit()
    {
        int inp;
        if (int.TryParse(textfield.GetComponent<TMP_InputField>().text, out inp))
        {
            if (inp == answer)
            {
                Debug.Log("Correct!");
                Exit();
            }
        }

    }

    public void Exit()
    {
        eventSystem.RaiseNetworkEvent(EventCodes.OnPlayerUninteractEvent, new object[] { playerProfile.player.ActorNumber });
        canvas.SetActive(false);

    }

    void OnDisable()
    {
        Cleanup();
    }

    void Cleanup()
    {
        
        
        textfield.GetComponent<TMP_InputField>().text = "";
    }
}
