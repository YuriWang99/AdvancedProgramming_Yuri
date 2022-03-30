using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject GameStart, RedWin, BlueWin, Draw;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowGameStart()
    {
        GameStart.SetActive(true);
    }
    public void CloseGameStart()
    {
        GameStart.SetActive(false);
    }

    public void ShowRedWin()
    {
        RedWin.SetActive(true);
    }
    public void CloseRedWin()
    {
        RedWin.SetActive(false);
    }

    public void ShowBlueWin()
    {
        BlueWin.SetActive(true);
    }
    public void CloseBlueWin()
    {
        BlueWin.SetActive(false);
    }

    public void ShowDraw()
    {
        Draw.SetActive(true);
    }
    public void CloseDraw()
    {
        Draw.SetActive(false);
    }

    public void Clean()
    {
        CloseBlueWin();
        CloseGameStart();
        CloseRedWin();
        CloseDraw();
    }
}
