using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIPage : MonoBehaviour
{
    [SerializeField]
    BasicEventManager eventManager;
    public void ButtonStartPress()
    {
        //press button to start
        eventManager.OnGameStart.Invoke();

    }
    void Update()
    {
        
    }
}
