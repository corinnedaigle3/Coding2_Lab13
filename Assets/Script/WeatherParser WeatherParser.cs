using UnityEngine;
using System.Xml;
using System.Collections;
using System;

public class WeatherParser : MonoBehaviour
{
    public string weatherDescription;

    // This is the callback: it will be called when XML is downloaded
    public void ParseWeather(string xmlText)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlText);

        XmlNode currentNode = xmlDoc.SelectSingleNode("current");

        XmlNode cityNode = currentNode.SelectSingleNode("city");
        string cityName = cityNode.Attributes["name"].Value;

        XmlNode weatherNode = currentNode.SelectSingleNode("weather");
        weatherDescription = weatherNode.Attributes["value"].Value;

/*        XmlNode sunNode = cityNode.SelectSingleNode("sun");
        string sunrise = sunNode.Attributes["rise"].Value;
        string sunset = sunNode.Attributes["set"].Value;

        Debug.Log($"City: {cityName}");
        Debug.Log($"Weather: {weatherDescription}");
        Debug.Log($"Sunrise: {sunrise}, Sunset: {sunset}");*/


    }
}
