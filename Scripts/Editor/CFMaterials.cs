
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class CFMaterials : MaterialEditor {

    // this is the same as the ShaderProperty function, show here so 
    // you can see how it works
    private void ShaderPropertyImpl(Shader shader, int propertyIndex)
    {
        int i = propertyIndex;
        string label = ShaderUtil.GetPropertyDescription(shader, i);
        string propertyName = ShaderUtil.GetPropertyName(shader, i);
        switch (ShaderUtil.GetPropertyType(shader, i))
        {
            case ShaderUtil.ShaderPropertyType.Range: // float ranges
            {
                GUILayout.BeginHorizontal();
                //TV float v2 = ShaderUtil.GetRangeLimits(shader, i, 1);
                //TV float v3 = ShaderUtil.GetRangeLimits(shader, i, 2);

                var prop = GetMaterialProperty(targets, propertyName);
                TextureProperty(prop, prop.displayName, true);
                
                GUILayout.EndHorizontal();



                break;
            }
            case ShaderUtil.ShaderPropertyType.Float: // floats
            {
                var prop = GetMaterialProperty(targets, propertyName);
                TextureProperty(prop, prop.displayName, true);
                break;
            }
            case ShaderUtil.ShaderPropertyType.Color: // colors
            {
                var prop = GetMaterialProperty(targets, propertyName);
                TextureProperty(prop, prop.displayName, true);
                break;
            }
            case ShaderUtil.ShaderPropertyType.TexEnv: // textures
            {
                //TV ShaderUtil.ShaderPropertyTexDim desiredTexdim = ShaderUtil.GetTexDim(shader, i);
                var prop = GetMaterialProperty(targets, propertyName);
                TextureProperty(prop, prop.displayName, true);

                GUILayout.Space(6);
                break;
            }
            case ShaderUtil.ShaderPropertyType.Vector: // vectors
            {
                var prop = GetMaterialProperty(targets, propertyName);
                TextureProperty(prop, prop.displayName, true);
                break;
            }
            default:
            {
                GUILayout.Label("ARGH" + label + " : " + ShaderUtil.GetPropertyType(shader, i));
                break;
            }
        }
    }

    public override void OnInspectorGUI ()
    {
        serializedObject.Update ();
        var theShader = serializedObject.FindProperty ("m_Shader"); 
        if (isVisible && !theShader.hasMultipleDifferentValues && theShader.objectReferenceValue != null)
        {
            //TV float controlSize = 64;

            //TV EditorGUIUtility.LookLikeControls(Screen.width - controlSize - 20);
            //TV EditorGUIUtility.labelWidth(Screen.width - controlSize - 20);
            EditorGUI.BeginChangeCheck();
            Shader shader = theShader.objectReferenceValue as Shader;

            for (int i = 0; i < ShaderUtil.GetPropertyCount(shader); i++)
            {
                ShaderPropertyImpl(shader, i);
            }

            if (EditorGUI.EndChangeCheck())
                PropertiesChanged ();
        }
    }
}