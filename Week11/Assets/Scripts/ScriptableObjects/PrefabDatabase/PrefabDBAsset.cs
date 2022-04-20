using UnityEngine;
using UnityEditor;

public class PrefabDBAsset
{
    [MenuItem("Assets/Create/ScriptableObjects/PrefabDB")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<PrefabDB>();
    }
}