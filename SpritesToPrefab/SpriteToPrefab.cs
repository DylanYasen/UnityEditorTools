using UnityEngine;
using System.Collections;

public class SpriteToPrefab : MonoBehaviour
{
    public Sprite[] sprites; //{ get; private set; }
    public string resPath = "";
    public string desPath = "Prefabs/";

    public void LoadSprite()
    {
        sprites = Resources.LoadAll<Sprite>(resPath);
    }

}
