using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

class CanvasManager : MonoBehaviour
{

    private string name = " ";

    [SerializeField]
    TextMeshProUGUI textObj;

    [SerializeField]
    Scrollbar rotationSpeed;

    [SerializeField]
    Scrollbar atribute1;

    [SerializeField]
    Scrollbar atribute2;

    [SerializeField]
    Scrollbar atribute3;

    [SerializeField]
    TextMeshProUGUI textAt1;
    [SerializeField]
    TextMeshProUGUI textAt2;
    [SerializeField]
    TextMeshProUGUI textAt3;



    private GameObject currentObject;
    public _4D_Object current4D_Object;

    public float speed = 0f;         
    private float lastValue = 0f;      
    private float speedMultiplier = 0.03f;

    private void Start()
    {

        rotationSpeed.value = 0.5f;

        rotationSpeed.onValueChanged.AddListener(OnScroll);

        atribute1.onValueChanged.AddListener(OnValueChangedA1);
        atribute2.onValueChanged.AddListener(OnValueChangedA2);
        atribute3.onValueChanged.AddListener(OnValueChangedA3);

        currentObject = CurrentObjectRenderer.GetCurrentRenderedObject();
        current4D_Object = currentObject.GetComponent<_4D_Object>();
        OnScroll(0.5f);
    }

    public void OnScroll(float value)
    {
        float delta = value - lastValue; 

        if (Mathf.Abs(delta) > 0.0001f) 
        {
  
            speed += delta * speedMultiplier;
            
            
            speed = Mathf.Clamp(speed, 0.0f, 0.1f);


            current4D_Object.ChangeSpeed(speed);
          
        }

        lastValue = value;
    }

    public void OnValueChangedA1(float value)
    {
        value *= 40;

        current4D_Object.objectProprieties[0].SetValue((int)value);
    }

    public void OnValueChangedA2(float value)
    {
        value *= 40;

        current4D_Object.objectProprieties[1].SetValue((int)value);
    }

    public void OnValueChangedA3(float value)
    {
        value *= 40;

        current4D_Object.objectProprieties[2].SetValue((int)value);
    }

    private void Update()
    {
        CheckObjectChanged();

        currentObject = CurrentObjectRenderer.GetCurrentRenderedObject();

        name = currentObject.name;
    }

    private void CheckObjectChanged()
    {
        if (!textObj.text.Equals(name))
        {
            textObj.text = name;



            current4D_Object = currentObject.GetComponent<_4D_Object>();
            ChangeAtributeName();
            current4D_Object.ChangeSpeed(speed);
        }
            
    }

    private void ChangeAtributeName()
    {
        textAt1.text = current4D_Object.objectProprieties[0].GetName();
        textAt2.text = current4D_Object.objectProprieties[1].GetName();
        textAt3.text = current4D_Object.objectProprieties[2].GetName();
    }



}

