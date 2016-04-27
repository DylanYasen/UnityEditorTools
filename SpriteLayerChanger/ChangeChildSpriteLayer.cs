using UnityEngine;
using System.Collections;
using UnityEditorInternal;
using System;
using System.Reflection;

public class ChangeChildSpriteLayer : MonoBehaviour
{
    public Transform targetObject;

    public string[] LoadSortingLayers()
    {
        System.Type internalEditorUtilityType = typeof(InternalEditorUtility);
        PropertyInfo sortingLayerProperty = internalEditorUtilityType.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
        string[] layers = (string[])sortingLayerProperty.GetValue(null, new object[0]);
        Debug.Log(layers);
        return layers;
    }

    public void ChangeLayer(string layer)
    {
        SpriteRenderer[] renderers = targetObject.GetComponentsInChildren<SpriteRenderer>(true);

        foreach (var renderer in renderers)
        {
            renderer.sortingLayerName = layer;
        }
    }
}