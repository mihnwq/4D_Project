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
            GameObject obj = new OBJLoader().Load(paths[0]);
            //CurrentObjectRendererSelection.SetRenderedObject(obj);
        }
    }

}

