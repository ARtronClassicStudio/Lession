using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CustomLitEditor : ShaderGUI
{


    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
       // materialEditor.ColorProperty(properties[0],"Life Color");
        base.OnGUI(materialEditor, properties);
    }

}
