using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(AnimOverrideControllerFiller))]
public class AnimOverrideControllerFillerEditor : Editor
{
    private AnimOverrideControllerFiller tool;
    void OnEnable()
    {
        tool = (AnimOverrideControllerFiller)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Auto fill anims"))
        {
            tool.Fill();
        }
    }
}