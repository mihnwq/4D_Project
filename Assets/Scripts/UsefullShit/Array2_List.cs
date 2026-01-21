using System;
using UnityEngine;

[System.Serializable]
public class Array2_List
{
    public string Name;

    [SerializeField] private int intValue;
    [SerializeField] private float floatValue;
    [SerializeField] private bool isFloat;  

    public Array2_List(string name, string rawValue)
    {
        Name = name;
        SetValue(rawValue);
    }

    public void SetValue(string rawValue)
    {
        if (int.TryParse(rawValue, out int intResult))
        {
            intValue = intResult;
            isFloat = false;
        }
        else if (float.TryParse(rawValue, out float floatResult))
        {
            floatValue = floatResult;
            isFloat = true;
        }
        else
        {
            throw new ArgumentException("Value is neither int nor float.");
        }
    }

    public void SetValue(int value)
    {
        intValue = value;
        isFloat = false;
    }

    public void SetValue(float value)
    {
        floatValue = value;
        isFloat = true;
    }

    public void SetName(string name)
    {
        Name = name;
    }

    public string GetName()
    {
        return Name;
    }

    public void GetValueInto(ref int value)
    {
        
        value = isFloat ? (int)floatValue : intValue;
    }

    public void GetValueInto(ref float value)
    {
       
        value = isFloat ? floatValue : intValue;
    }
}


