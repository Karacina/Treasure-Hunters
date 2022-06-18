using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items")]
public class Item : ScriptableObject
{
    public string displayName;
    public Sprite icon;
    public GameObject prefab;
}
