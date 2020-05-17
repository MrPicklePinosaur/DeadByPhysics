
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light lt;
    public int counter;

    // Start is called before the first frame update
    void Start()
    {
        lt = GetComponent<Light>();
        InvokeRepeating("Flicker",0,Random.Range(0.1f,1.0f));
    }

    void Flicker()
    {
        if (Random.Range(0,2) == 1){
            lt.intensity = 0.0f;
        }
        else
        {
            lt.intensity = 2.5f;
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
