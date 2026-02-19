using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CurrentObjectRendererSelection : MonoBehaviour
{
    public List<GameObject> _4D_Objects;

    public static GameObject currentObject;

    private int minObjectCount, maxObjectCount;

    private int index = 1;

    [SerializeField]
    Vector3 standardPosition;

    GameObject instantiatedObject = null;

    _3Dto4D convertor;

    private void Start()
    {
        minObjectCount = 0;
        maxObjectCount = 1;
        convertor = new _3Dto4D();
        MoveToNextObject(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            MoveToNextObject(1);

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            MoveToNextObject(-1);

        convertor.UpdateMesh();
    }



    private void MoveToNextObject(int nextPosition)
    {
        if (instantiatedObject)
        {
            Destroy(instantiatedObject);
        }

        index += nextPosition;

        if (index < 0)
            index = maxObjectCount;
        else if (index > maxObjectCount)
            index = minObjectCount;

        instantiatedObject = Instantiate(_4D_Objects[index]);

        convertor.SetCurrentObject(instantiatedObject);
        convertor.Init();

        MoveAroundObject.SetTarget(instantiatedObject.transform);

        instantiatedObject.SetActive(true);
        instantiatedObject.transform.position = standardPosition;

        currentObject = instantiatedObject;
    }

    public static GameObject GetCurrentRenderedObject() => currentObject;


}

