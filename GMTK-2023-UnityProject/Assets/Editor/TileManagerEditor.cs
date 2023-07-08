using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TileManager))]
public class TileManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TileManager myScript = (TileManager)target;
        if (Application.isPlaying && GUILayout.Button("Generate Buttons"))
        {
            myScript.CreateNewButtons();
        }
    }
}
