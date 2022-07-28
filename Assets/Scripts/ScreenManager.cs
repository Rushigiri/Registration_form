using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{

    public Canvas RegisterScreen;
    public Canvas SingleScreen;
    public Canvas AllUserNameScreen;

    public static ScreenManager instance;
    private void Awake()
    {
        instance = this;
    }

    public void ShowSingleScreen()
    {
        SingleScreen.enabled = true;
        RegisterScreen.enabled = false;
        AllUserNameScreen.enabled = false;
    }

    public void ShowAllUserScreen()
    {
        AllUserNameScreen.enabled = true;
        SingleScreen.enabled = false;
        RegisterScreen.enabled = false;
    }

    public void ShowRegisterScreen()
    {
        RegisterScreen.enabled = true;
        AllUserNameScreen.enabled = false;
        SingleScreen.enabled = false;
    }
}
