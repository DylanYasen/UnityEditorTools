using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimOverrideControllerFiller : MonoBehaviour
{
    public AnimatorOverrideController overrideController;
    public List<AnimationClip> clips;

    public void Fill()
    {
        var clipPairs = overrideController.clips;

        foreach (var clipPair in clipPairs)
        {
            foreach (var newClip in clips)
            {
                if (newClip.name == clipPair.originalClip.name)
                {
                    clipPair.overrideClip = newClip;
                    Debug.Log("successfully override " + newClip.name);
                }
            }
        }

        overrideController.clips = clipPairs;
    }
}
