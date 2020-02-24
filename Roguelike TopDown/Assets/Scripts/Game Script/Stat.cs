using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
    private Image content;

    [SerializeField]
    private Text statValue=null;

    [SerializeField]
    private float lerpSpeed=2;

    private float currentFill;

    public float MyMaxValue { get; set; }
    public float CurrentValue
    {
        get => currentValue;
        set
        {
            if (value > MyMaxValue)
            {
                currentValue = MyMaxValue;
            }else if(currentValue < 0)
            {
                currentValue = 0;
            }else
            {
                currentValue = value;
            }

            currentFill = currentValue / MyMaxValue;

            if(statValue !=null)
            {
                statValue.text = currentValue.ToString();
            }
            
        }
    }   

    private float currentValue;

    void Start()
    {
        content = GetComponent<Image>();
    }

    void Update()
    {
        if(currentFill != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount,currentFill,Time.deltaTime*lerpSpeed);
        }

    }

    public void Initilize(float currentValue,float maxValue)
    {
        if(content == null)
        {
            content = GetComponent<Image>();
        }
        MyMaxValue = maxValue;
        CurrentValue = currentValue;
        content.fillAmount = CurrentValue / MyMaxValue;
    }
}
