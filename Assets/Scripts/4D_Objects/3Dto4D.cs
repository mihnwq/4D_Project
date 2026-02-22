using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class _3Dto4D
{
    private GameObject currentObject;

    public float rotationSpeed = 1f;

    public float wDistance = 3f;

    [Header("W Controls")]
    public float wOffset = 0f;
    public bool wOscillation = false;
    public float wOscillationAmplitude = 0.5f;
    public float wOscillationSpeed = 2f;

    public bool wDeform = false;
    public float wDeformStrength = 0.2f;

    private Mesh originalMesh;
    private Mesh workingMesh;

    private Vector4[] baseVertices4D;
    private Vector4[] currentVertices4D;
    private Vector3[] projected3D;

    public void SetCurrentObject(GameObject currentObject)
    {
        this.currentObject = currentObject;
    }

    public void Init()
    {
        originalMesh = currentObject.GetComponent<MeshFilter>().mesh;
        workingMesh = UnityEngine.Object.Instantiate(originalMesh);
        currentObject.GetComponent<MeshFilter>().mesh = workingMesh;
        workingMesh.MarkDynamic();


        Vector3[] verts3 = originalMesh.vertices;
        baseVertices4D = new Vector4[verts3.Length];
        currentVertices4D = new Vector4[verts3.Length];
        projected3D = new Vector3[currentVertices4D.Length];

        for (int i = 0; i < verts3.Length; i++)
        {
            baseVertices4D[i] = new Vector4(verts3[i].x, verts3[i].y, verts3[i].z, 0f);
        }
    }

    public void UpdateMesh()
    {
        if (!currentObject)
            return;

        float t = Time.time * rotationSpeed;

        

        for (int i = 0; i < baseVertices4D.Length; i++)
        {
            currentVertices4D[i] = baseVertices4D[i];

            Vector4 v = currentVertices4D[i];

            v.w += wOffset;

            if (wOscillation)
                v.w += Mathf.Sin(Time.time * wOscillationSpeed) * wOscillationAmplitude;


            if (wDeform)
                v.w += (v.x + v.y + v.z) * wDeformStrength;

            currentVertices4D[i] = v;

            Vector4 v1 = currentVertices4D[i];
            /*   v1 = RotateXW(v1, t * 0.5f); // XW rotation
               v1 = RotateYW(v1, t * 0.3f); // YW rotation*/
            v1 = Rotate4D(v1, t * 0.5f, 0, 3); // XW
            v1 = Rotate4D(v1, t * 0.3f, 1, 3); // YW
           // v1 = Rotate4D(v1, t * 0.2f, 2, 3); // ZW
            currentVertices4D[i] = v1;

            projected3D[i] = ProjectTo3D(currentVertices4D[i]);
        }

       

            // Update mesh
            workingMesh.vertices = projected3D;
        workingMesh.RecalculateNormals();
        workingMesh.RecalculateBounds();
    }

       



        Vector4 RotateXW(Vector4 v, float angle)
    {
        float c = Mathf.Cos(angle);
        float s = Mathf.Sin(angle);

        float x = v.x * c - v.w * s;
        float w = v.x * s + v.w * c;

        return new Vector4(x, v.y, v.z, w);
    }

    Vector4 RotateYW(Vector4 v, float angle)
    {
        float c = Mathf.Cos(angle);
        float s = Mathf.Sin(angle);

        float y = v.y * c - v.w * s;
        float w = v.y * s + v.w * c;

        return new Vector4(v.x, y, v.z, w);
    }

    Vector4 Rotate4D(Vector4 v, float angle, int axis1, int axis2)
    {
        float s = Mathf.Sin(angle);
        float c = Mathf.Cos(angle);

        float a = v[axis1];
        float b = v[axis2];

        v[axis1] = a * c - b * s;
        v[axis2] = a * s + b * c;

        return v;
    }

    Vector3 ProjectTo3D(Vector4 v)
    {
        float k = wDistance;

        float wFactor = k - v.w;
        if (wFactor < 0.01f) wFactor = 0.01f;

        return new Vector3(
            v.x / wFactor,
            v.y / wFactor,
            v.z / wFactor
        );
    }

}

