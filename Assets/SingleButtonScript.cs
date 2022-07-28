using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

public class SingleButtonScript : MonoBehaviour
{
    

    public int userNumber;

    

    
   

    public void OnClickBehaviour()
    {
        RushiForm.instance.OnclickShowData(userNumber);
        //test.DataShow(userNumber);
        //UserScreenManager.instance.ShowSingleUserDetailsCanvas();

    }


}
