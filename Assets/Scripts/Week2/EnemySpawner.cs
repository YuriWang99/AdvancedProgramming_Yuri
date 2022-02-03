using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    BasicEventManager eventManager;
    void Start()
    {
        eventManager.OnGameStart += EnemyStartSpawm;
    }

    // Update is called once per frame
    void EnemyStartSpawm()
    {
        Debug.Log("Enemy start soawn");
    }
    private void OnDestroy()
    {
        eventManager.OnGameStart -= EnemyStartSpawm;
    }
}
