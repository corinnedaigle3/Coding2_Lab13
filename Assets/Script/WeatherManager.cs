using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class WeatherManager
{
    public void OnXMLDataLoaded(string data)
    {
        //Debug.Log(data);
    }



    // First City -- Orlando
    private const string xmlApi_1 = "https://api.openweathermap.org/data/2.5/weather?q=Orlando,us&mode=xml&appid=8c7d626d2f67ba1d87b11bb8e603ef9d";

    //api.openweathermap.org/data/2.5/weather?q=Orlando,us&mode=xml&appid=76d07681130d560a364db557a2872510

    public IEnumerator GetWeatherXML_1(Action<string> callback)
    {
        return CallAPI(xmlApi_1, callback);
    }



    // Second City -- Paris
    private const string xmlApi_2 = "https://api.openweathermap.org/data/2.5/weather?q=Paris,fr&mode=xml&appid=8c7d626d2f67ba1d87b11bb8e603ef9d";


    public IEnumerator GetWeatherXML_2(Action<string> callback)
    {
        return CallAPI(xmlApi_2, callback);
    }

    // Third City -- Tokyo
    private const string xmlApi_3 = "https://api.openweathermap.org/data/2.5/weather?q=Tokyo,jp&mode=xml&appid=8c7d626d2f67ba1d87b11bb8e603ef9d";


    public IEnumerator GetWeatherXML_3(Action<string> callback)
    {
        return CallAPI(xmlApi_3, callback);
    }

    // Third City -- Sacramento
    private const string xmlApi_4 = "https://api.openweathermap.org/data/2.5/weather?q=Sacramento,us&mode=xml&appid=8c7d626d2f67ba1d87b11bb8e603ef9d";


    public IEnumerator GetWeatherXML_4(Action<string> callback)
    {
        return CallAPI(xmlApi_4, callback);
    }

    // Fourth City -- Beijing
    private const string xmlApi_5 = "https://api.openweathermap.org/data/2.5/weather?q=Beijing,cn&mode=xml&appid=8c7d626d2f67ba1d87b11bb8e603ef9d";


    public IEnumerator GetWeatherXML_5(Action<string> callback)
    {
        return CallAPI(xmlApi_5, callback);
    }

    private IEnumerator CallAPI(string url, Action<string> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError($"network problem: {request.error}");
            }
            else if (request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"response error: {request.responseCode}");
            }
            else
            {
                callback(request.downloadHandler.text);
            }
        }
    }
}