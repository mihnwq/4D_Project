using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CurrentObjectRendererSelection : MonoBehaviour
{

    private static GameObject selectedObject = null;

    private static GameObject currentObject = null;

    [SerializeField]
    private GameObject emptyObject;

    [SerializeField]
    private Vector3 standardPosition;

    private GameObject instantiatedObject = null;

    private _3Dto4D convertor;

    

    private void Start()
    {
        convertor = new _3Dto4D();
        MoveToNextObject(emptyObject);
        convertor.SetCurrentObject(null);
    }
  
    private void Update()
    {
        if(currentObject != selectedObject)
        MoveToNextObject(selectedObject);

        convertor.UpdateMesh();
    }



    private void MoveToNextObject(GameObject _3D_Object)
    {
        if (instantiatedObject)
        {
            Destroy(instantiatedObject);
        }

        if (!_3D_Object || _3D_Object == emptyObject)
        {
            MoveAroundObject.SetTarget(emptyObject.transform);
            return;
        }

        instantiatedObject = Instantiate(_3D_Object);

        convertor.SetCurrentObject(instantiatedObject);
        convertor.Init();

        MoveAroundObject.SetTarget(instantiatedObject.transform);

        instantiatedObject.SetActive(true);
        instantiatedObject.transform.position = standardPosition;

        currentObject = instantiatedObject;
    }

    public static GameObject GetCurrentRenderedObject() => currentObject;

    public static void SetRenderedObject(GameObject newObject) { selectedObject = newObject; }


}

