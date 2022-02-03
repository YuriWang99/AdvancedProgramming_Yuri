using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomeMenu : MonoBehaviour
{
    [SerializeField]
    BasicEventManager eventManager;
    void Start()
    {
        eventManager.OnGameStart += CloseMainMenu;
    }
    void CloseMainMenu()
    {

    }
    private void OnDestroy()
    {
        eventManager.OnGameStart -= CloseMainMenu;
    }

}
