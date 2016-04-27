using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(AnimFlipper))]
public class AnimFlipperEditor : Editor
{

    private AnimFlipper tool;

    void OnEnable()
    {
        tool = (AnimFlipper)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Flip 'em Bitch"))
        {
            if (tool.clips.Length < 1)
            {
                Debug.LogError("No animation specified");
                return;
            }

            foreach (var clip in tool.clips)
            {
                clip.name = clip.name.ToLower();
                // add a frame to original clip
                // so it will flip the sprite back
                AnimationCurve oriCurve = new AnimationCurve(new Keyframe(0, tool.scaleFactor));

                clip.SetCurve("", typeof(Transform), "localScale.x", oriCurve);
                clip.SetCurve("", typeof(Transform), "localScale.y", oriCurve);
                clip.SetCurve("", typeof(Transform), "localScale.z", oriCurve);

                // get bindings
                //				GameObject obj = null;
                //				EditorCurveBinding[] bindings = AnimationUtility.GetCurveBindings (clip);
                //				AnimationUtility.GetAnimatedObject (obj, bindings [0]);

                // duplicate a file
                string path = AssetDatabase.GetAssetPath(clip);
                string pathWoFormat = GetFilepathWithoutFormatStr(path);
                string newPath = pathWoFormat + " copy.anim";
                bool duplicateSuccessed = AssetDatabase.CopyAsset(path, newPath);
                if (duplicateSuccessed)
                {
                    Debug.Log("copied");
                    AssetDatabase.Refresh();
                }
                else {
                    Debug.LogError("copy anim clip failed");
                    return;
                }

                // add flipped frame to new animation clip
                AnimationClip newClip = AssetDatabase.LoadAssetAtPath<AnimationClip>(newPath);
                AnimationCurve flippedCurve = new AnimationCurve(new Keyframe(0, -tool.scaleFactor));

                newClip.SetCurve("", typeof(Transform), "localScale.x", flippedCurve);
                newClip.SetCurve("", typeof(Transform), "localScale.y", oriCurve);
                newClip.SetCurve("", typeof(Transform), "localScale.z", oriCurve);

                // update new clip name 
                string newClipName = GetFlippedDirectionString(clip.name);
                Debug.Log(newClipName);
                AssetDatabase.RenameAsset(newPath, newClipName);
                AssetDatabase.Refresh();
            }
        }
    }

    char period = '.';
    public string GetFilepathWithoutFormatStr(string path)
    {
        int commaIndex = path.IndexOf(period);
        string pathWoFormat = path.Substring(0, commaIndex);

        return pathWoFormat;
    }

    private string left = "left";
    private string right = "right";
    public string GetFlippedDirectionString(string animName)
    {

        if (animName.Contains(left))
            animName = animName.Replace(left, right);
        else if (animName.Contains(right))
            animName = animName.Replace(right, left);
        else
            Debug.LogError("don't know if the clip is left or right oriented.");

        return animName;
    }
}
