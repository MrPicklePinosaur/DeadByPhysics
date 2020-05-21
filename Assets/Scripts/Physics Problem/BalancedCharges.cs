using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class BalancedCharges : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider sliderObj;
    public InputField txtObj;
    public GameObject rHolder;
    public GameObject lHolder;
    public Text rText, lText;

    private float ansProp = 0f;
    private int correct = 0;
    private void OnEnable()
    {
        correct = 0;
        makeNewProblem();
    }
    public void sliderChange()
    {
        
        txtObj.text = ((sliderObj.value-(1/3.0f)*sliderObj.maxValue)/10.0f).ToString("0.0");
        
    }
    public void txtChange()
    {
        
        float val;
        if (float.TryParse(txtObj.text,out val))
        {
            val = val*10 + 1 / 3.0f * sliderObj.maxValue;
            if (val <= sliderObj.maxValue && val >= sliderObj.minValue)
            {
                sliderObj.value = val;
            }
        }
    }
    public void makeNewProblem()
    {
        float leftCharge =(float) Math.Round((double)UnityEngine.Random.Range(-1f,1f),4);
        float ans = (float)Math.Round((double)UnityEngine.Random.Range(-800f, 1800f), 4);
        float rightCharge =(float) Math.Round((double)UnityEngine.Random.Range(-1f,1f),4);
        if(ans>=0 && ans<=1000)
        {
            rightCharge = leftCharge * Mathf.Pow((1000 - ans), 2) / Mathf.Pow(ans, 2);
        }
        else
        {
            rightCharge = -leftCharge * Mathf.Pow((1000 - ans), 2) / Mathf.Pow(ans, 2);
        }
        rightCharge = (float)Math.Round((double)rightCharge, 4);
        if (rightCharge >= 0)
        {
            rHolder.transform.Find("positive").gameObject.SetActive(true);
            rHolder.transform.Find("negative").gameObject.SetActive(false);
        }
        else
        {
            rHolder.transform.Find("positive").gameObject.SetActive(false);
            rHolder.transform.Find("negative").gameObject.SetActive(true);
        }
        if (leftCharge >= 0)
        {
            lHolder.transform.Find("positive").gameObject.SetActive(true);
            lHolder.transform.Find("negative").gameObject.SetActive(false);
        }
        else
        {
            lHolder.transform.Find("positive").gameObject.SetActive(false);
            lHolder.transform.Find("negative").gameObject.SetActive(true);
        }
        if((rightCharge>=0 && leftCharge >= 0)||(rightCharge<=0 && leftCharge<=0))
        {
            float squareRoot = Mathf.Sqrt(Mathf.Abs(leftCharge) / Mathf.Abs(rightCharge));
            ans = (1000 * squareRoot) / (1 + squareRoot);
        }
        else if (Mathf.Abs(rightCharge) > Mathf.Abs(leftCharge))
        {
            float squareRoot = Mathf.Sqrt(Mathf.Abs(leftCharge) / Mathf.Abs(rightCharge));
            ans = -(1000 * squareRoot) / (1 - squareRoot);
        }
        else
        {
            float squareRoot = Mathf.Sqrt(Mathf.Abs(leftCharge) / Mathf.Abs(rightCharge));
            ans = (1000 * squareRoot) / (squareRoot - 1);
        }
        rText.text = rightCharge.ToString() + " C";
        lText.text = leftCharge.ToString() + " C";
        Debug.Log(ans);

        ansProp = ans;
    }
    public void submitAns()
    {
        if (String.Equals(ansProp.ToString("0.0"), txtObj.text))
        {
            Debug.Log("correct");
            correct += 1;
            Exit();
        }
        else
        {
            Debug.Log("incorrect");
        }
        makeNewProblem();
    }
    void Start()
    {
        correct = 0;
        makeNewProblem();
    }
    public void Exit()
    {
        this.gameObject.SetActive(false);
    }
}
