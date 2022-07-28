using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.Networking;
using Newtonsoft.Json;
using SimpleJSON;



[System.Serializable]
public class RushiForm : MonoBehaviour
{
    public GameObject DataShowPanel;
    public GameObject FullDataShowPanel;
    public TMP_InputField _nameTxt;
    public TMP_InputField _emailTxt;
    public TMP_InputField _mobileTxt;
    public TMP_InputField _countryTxt;
    public TMP_InputField _cityTxt;
    public TMP_InputField _addTxt;
    public Root root;
    public GameObject buttonPrefab;
    public Transform content;

    public List<SingleButtonScript> listOfButtons;

    public Canvas ShowFullDetailCanvas;

    public int btnCounter;


    public static RushiForm instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        btnCounter = 0;
        GetData();

        ShowFullDetailCanvas.enabled = false;

        listOfButtons = new List<SingleButtonScript>();
        
    }

    bool NotNull()
    {
        if (_nameTxt.text == "")
        {
            Debug.Log("Enter Your Full Name");
            return false;
        }

        if (_emailTxt.text == "")
        {
            Debug.Log("Enter Your Email");
            return false;
        }

        if (_mobileTxt.text == "")
        {
            Debug.Log("Enter Your Mobile Number");
            return false;
        }

        if (_countryTxt.text == "")
        {
            Debug.Log("Enter Your Country");
            return false;
        }

        if (_cityTxt.text == "")
        {
            Debug.Log("Enter Your City");
            return false;
        }
        if (_addTxt.text == "")
        {
            Debug.Log("Enter your Address");
            return false;
        }
        return true;
    }



    public void SubmitUserDetails()
    {
        if (NotNull())
        {
            User user = new User();
            user.Name = _nameTxt.text;
            user.EmailId = _emailTxt.text;
            user.MobileNo = long.Parse(_mobileTxt.text);
            user.Country = _countryTxt.text;
            user.City = _cityTxt.text;
            user.Address = _addTxt.text;
            Reset();
            root.DataList.Add(user);
            string jsonData = JsonUtility.ToJson(root);
            Debug.Log(jsonData);
            UnityWebRequest putData = UnityWebRequest.Put("https://api.jsonbin.io/v3/b/62d69f225ecb581b56c5c1c1", jsonData);

            putData.SetRequestHeader("Content-Type", "application/json");
            putData.SetRequestHeader("X-Master-Key", "$2b$10$ej.j9V9nupU6o5WIIS.cfOSpeohkLlhPxICi52gFrrLjVxbE0Othy");
            putData.SendWebRequest();
        }
    }

    public void GetData()
    {
        StartCoroutine(GetJsonData());
    }
    
    public void OnclickShowData(int a)
    {

        DataShow(a);
        ShowFullDetailCanvas.enabled = true;

    }

    public void DataShow(int b)
    {
        GetData();

        TextMeshProUGUI name = FullDataShowPanel.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI country = FullDataShowPanel.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();

        name.text = root.DataList[b].Name;

        country.text = root.DataList[b].Country;


    }


   

    IEnumerator GetJsonData()
    {


        UnityWebRequest url = UnityWebRequest.Get("https://api.jsonbin.io/v3/b/62d69f225ecb581b56c5c1c1");



        yield return url.SendWebRequest();

        if (url.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("net error");
        }
        else
        {
            string jsonString = url.downloadHandler.text;
            Debug.Log(jsonString);

            var currentRoot = JSON.Parse(jsonString);
            root = JsonUtility.FromJson<Root>(currentRoot["record"].ToString());

            Debug.Log(root.DataList.Count);
            //TextMeshProUGUI buttonName =
            for (int i = 0; i < root.DataList.Count; i++)
            {
                if(i>btnCounter)
                {
                    GameObject thisBtn = Instantiate(buttonPrefab, content);
                    thisBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = root.DataList[i].Name;
                    thisBtn.GetComponent<SingleButtonScript>().userNumber = i;
                    listOfButtons.Add(thisBtn.GetComponent<SingleButtonScript>());
                    btnCounter++;
                }

            }

            
        }

        url.Dispose();

    }

    [System.Serializable]
    public class User
    {
        public string Name;
        public string EmailId;
        public long MobileNo;
        public string Country;
        public string City;
        public string Address;
    }
    public void Reset()
    {
        _nameTxt.text = "";
        _countryTxt.text = "";
        _mobileTxt.text = "";
        _emailTxt.text = "";
        _cityTxt.text = "";
        _addTxt.text = "";
    }
    [System.Serializable]
    public class Root
    {
        public List<User> DataList;
    }
    public List<User> GetLatestUserList()
    {
        return root.DataList;
    }
}



