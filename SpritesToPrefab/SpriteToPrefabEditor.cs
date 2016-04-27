using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpriteToPrefab))]
public class SpriteToPrefabEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SpriteToPrefab editor = (SpriteToPrefab)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Load & Create"))
        {
            editor.LoadSprite();

            for (int i = 0; i < editor.sprites.Length; i++)
            {
                //var localPath : String = "Assets/" + go.name + ".prefab";
                string path = "Assets/" + editor.desPath + editor.sprites[i].name + ".prefab";

                GameObject obj = new GameObject(editor.sprites[i].name);
                obj.AddComponent<SpriteRenderer>().sprite = editor.sprites[i];
                PrefabUtility.CreatePrefab(path, obj);

                DestroyImmediate(obj);
            }
        }
    }
}
