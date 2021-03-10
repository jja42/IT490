using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequest : MonoBehaviour
{
    string str;
    string postURL;
    string[] strarr;
    // Start is called before the first frame update
    public IEnumerator PostRequest(string str)
    {
        WWWForm form = new WWWForm();
        form.AddField("test_var", str);
        UnityWebRequest www = UnityWebRequest.Post(postURL, form);
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(www.error);
        }
    }
    public IEnumerator GetRequest()
    {
        UnityWebRequest www = UnityWebRequest.Get(postURL);
        yield return www.SendWebRequest();
        str = www.downloadHandler.text;
        API_Parse(str);
    }
    public void Api_Request()
    {
        postURL = "http://25.8.118.66/api_request.php";
        StartCoroutine(GetRequest());
    }
    public void API_Parse(string str)
    {
        strarr = str.Split('\n','>');
        print(strarr[5]);
        print(strarr[7]);
        print(strarr[9]);
        print(strarr[11]);
        print(strarr[13]);
    }
}
