using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SFB;
using Dummiesman;
using System.IO;

public class PC_Selection : MonoBehaviour
{

    public void OpenFileBrowser()
    {
        var paths = StandaloneFileBrowser.OpenFilePanel(
            "Select 3D Model",
            "",
            new[] { new ExtensionFilter("3D Models", "obj", "fbx" , "prefab") },
            false
        );

        if (paths.Length > 0)
        {
            GameObject loadedRoot = new OBJLoader().Load(paths[0]);

            if (loadedRoot == null)
            {
                return;
            }

            MeshFilter meshFilter = loadedRoot.GetComponentInChildren<MeshFilter>();

            if (meshFilter == null || meshFilter.sharedMesh == null)
            {
                return;
            }

            Mesh mesh = meshFilter.sharedMesh;

          
            if (loadedRoot.transform.localToWorldMatrix.determinant < 0)
            { 

                for (int i = 0; i < mesh.subMeshCount; i++)
                {
                    int[] tris = mesh.GetTriangles(i);

                  
                    for (int t = 0; t < tris.Length; t += 3)
                    {
                        int temp = tris[t];
                        tris[t] = tris[t + 1];
                        tris[t + 1] = temp;
                    }

                    mesh.SetTriangles(tris, i);
                }

                mesh.RecalculateNormals();
                mesh.RecalculateBounds();

                
                Vector3 s = loadedRoot.transform.localScale;
                s.x = Mathf.Abs(s.x);
                s.y = Mathf.Abs(s.y);
                s.z = Mathf.Abs(s.z);
                loadedRoot.transform.localScale = s;
            }

            MeshRenderer renderer = meshFilter.GetComponent<MeshRenderer>();

            if (renderer != null)
            {
                Shader shader = Shader.Find("Universal Render Pipeline/Lit");

               
                if (shader == null)
                    shader = Shader.Find("Standard");

                Material mat = new Material(shader);
                mat.color = Color.gray;

                renderer.material = mat;
            }

            CurrentObjectRendererSelection.SetRenderedObject(meshFilter.gameObject);
        }
    }

}

