using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectAtributesSelection : MonoBehaviour
{

    private string name = " ";

    [SerializeField]
    TextMeshProUGUI textObj;

    [SerializeField]
    Scrollbar rotationSpeed;

    [SerializeField]
    CurrentObjectRendererSelection cors;

    private _3Dto4D convertor;

    public float speed = 0f;
    private float lastValue = 0f;
    private float speedMultiplier = 0.2f;

    

    private void Start()
    {
        convertor = cors.GetCurrentObjectConvertor();

        rotationSpeed.value = 0.2f;

        rotationSpeed.onValueChanged.AddListener(OnScroll);

        OnScroll(0.2f);
    }

    public void OnScroll(float value)
    {
        float delta = value - lastValue;

        if (Mathf.Abs(delta) > 0.0001f)
        {

            speed += delta * speedMultiplier;


            speed = Mathf.Clamp(speed, 0.0f, 0.4f);


            convertor.SetSpeed(speed);

        }

        lastValue = value;
    }
}

