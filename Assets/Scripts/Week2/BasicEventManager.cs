using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEventManager : MonoBehaviour
{
    public delegate void GameStart();
    public GameStart OnGameStart;
    public void OnDestroy()
    {
        if(OnGameStart!=null)
        {
            OnGameStart = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
