using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.IO;
using TMPro;
using UnityEngine.Networking;

using SimpleJSON;


public class r_Form : MonoBehaviour
{
    public TMP_InputField _nameTxt;
    public TMP_InputField _emailTxt;
    public TMP_InputField _mobileTxt;
    public TMP_InputField _countryTxt;
    public TMP_InputField _cityTxt;
    public TMP_InputField _addTxt;
    public Root root;

    private void Start()
    {
         StartCoroutine(GetDataAtStart());
    }

    public void OnSubmit()
    {
        if (NotNull ())
        {

            User data = new User();

            data.Name = _nameTxt.text;
            data.EmailId = _emailTxt.text;
            data.MobileNo = long.Parse(_mobileTxt.text);
            data.Country = _countryTxt.text;
            data.City = _cityTxt.text;
            data.Address = _addTxt.text;


            root.DataList.Add(data);
            string storeDataInJson = JsonUtility.ToJson(root);
            File.WriteAllText(Application.dataPath + "/JsonText.txt", storeDataInJson);
            UnityWebRequest Url = UnityWebRequest.Put("https://api.jsonbin.io/v3/b/62d69f225ecb581b56c5c1c1", storeDataInJson);
            Url.SetRequestHeader("Content-Type", "application/json");
            Url.SetRequestHeader("X-Master-Key", "$2b$10$ej.j9V9nupU6o5WIIS.cfOSpeohkLlhPxICi52gFrrLjVxbE0Othy");
            Url.SendWebRequest();
            Reset();





        }

    }

    IEnumerator GetDataAtStart()
    {

        UnityWebRequest getUrl = UnityWebRequest.Get("https://api.jsonbin.io/v3/b/62d69f225ecb581b56c5c1c1");
        //getUrl.SendWebRequest();

        yield return getUrl.SendWebRequest();

        if(getUrl.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("net error");
        }
        else
        {
            string jsonString = getUrl.downloadHandler.text;

            var thisRoot = JSON.Parse(jsonString);

            root = JsonUtility.FromJson<Root>(thisRoot["record"].ToString());

            Debug.Log(root.DataList.Count);

            
        }
        getUrl.Dispose();

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

        bool isEmail = Regex.IsMatch(_emailTxt.text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        if (isEmail == false)
        {
            Debug.Log("please enter valid Email address");
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
    public class User
    {
        public string Name;
        public string EmailId;
        public long MobileNo;
        public string Country;
        public string City;
        public string Address;

    }

    [System.Serializable]
    public class Root
    {
        public List<User> DataList;
    }



}
