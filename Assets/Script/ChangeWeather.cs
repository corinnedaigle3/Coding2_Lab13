using System;
using System.Globalization;
using System.Collections;
using UnityEngine;
using static ChangeWeather;
using System.Collections.Generic;

public class ChangeWeather : MonoBehaviour
{
    public enum CityState
    {
        Orlando,
        Paris,
        Tokyo,
        Sacramento,
        Beijing
    }


    [Header("Current City")]
    public CityState cityState;

    [Header("Skybox")]
    public Material sunnySkybox;
    public Material cloudySkybox;
    public Material fogSkybox;
    public Material overcastSkybox;

    [Header("Default")]
    public Material defaultSkybox;
    public bool HideSkybox;

    [Header("Light")]
    public Light directionalLight;
    public float targetIntensity;
    public Color newColor;

    [Range(0, 360)]
    public int lightRange;


    public WeatherParser weatherParser;
    public WeatherManager m;


    public Dictionary<string, Material> weatherSetting = new Dictionary<string, Material>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m = new WeatherManager();
        weatherParser = GetComponent<WeatherParser>();
        Add();
        StartCoroutine(routine: m.GetWeatherXML_1(weatherParser.ParseWeather));

        changeSkyBox();
        ChangeCity();
        TimeZone();
        HideSkybox = false;
    }

    public void changeSkyBox()
    {
        if (HideSkybox)
        {
            RenderSettings.skybox = defaultSkybox;
            return;
        }

        string currentWeather = weatherParser.weatherDescription;

        if (weatherSetting.ContainsKey(currentWeather))
        {
            RenderSettings.skybox = weatherSetting[currentWeather];
            Debug.Log($"Current Skybox: {weatherSetting[currentWeather]} | Current Weather: {currentWeather}");
        }
        else
        {
            // Fallback if weather not found
            RenderSettings.skybox = defaultSkybox;
        }
    }

    public void Add()
    {
        weatherSetting.Add("few clouds", cloudySkybox);
        weatherSetting.Add("clear sky", sunnySkybox);
        weatherSetting.Add("fog", fogSkybox);
        weatherSetting.Add("overcast clouds", overcastSkybox);
    }

    void Update()
    {
        //directionalLight.transform.Rotate(Max);
        directionalLight.color = newColor;
        directionalLight.intensity = targetIntensity;
    }

    public void ChangeCity()
    {
        switch (cityState)
        {
            case CityState.Orlando:
                StartCoroutine(routine: m.GetWeatherXML_1(weatherParser.ParseWeather));
                //StartCoroutine(m.GetWeatherXML_1(m.OnXMLDataLoaded));
                //RenderSettings.skybox = sunnySkybox;
                targetIntensity = 2;
                newColor = Color.blue;
                break;

            case CityState.Paris:
                StartCoroutine(routine: m.GetWeatherXML_2(weatherParser.ParseWeather));
                //StartCoroutine(m.GetWeatherXML_2(m.OnXMLDataLoaded));
                //RenderSettings.skybox = rainySkybox;
                targetIntensity = 8;
                newColor = Color.white;
                break;

            case CityState.Tokyo:
                StartCoroutine(routine: m.GetWeatherXML_3(weatherParser.ParseWeather));
                //RenderSettings.skybox = cloudySkybox;
                targetIntensity = 15;
                newColor = Color.pink;
                break;

            case CityState.Sacramento:
                StartCoroutine(routine: m.GetWeatherXML_4(weatherParser.ParseWeather));
                //StartCoroutine(m.GetWeatherXML_4(m.OnXMLDataLoaded));
                //RenderSettings.skybox = sunnySkybox;
                targetIntensity = 30;
                newColor = Color.yellow;
                break;

            case CityState.Beijing:
                StartCoroutine(routine: m.GetWeatherXML_5(weatherParser.ParseWeather));
                //StartCoroutine(m.GetWeatherXML_5(m.OnXMLDataLoaded));
                //RenderSettings.skybox = snowySkybox;
                targetIntensity = 100;
                newColor = Color.green;
                break;
        }
    }



    /*IEnumerator LerpLightIntensity(float targetIntensity, float duration)
    {
        float startIntensity = directionalLight.intensity;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            directionalLight.intensity = Mathf.Lerp(startIntensity, targetIntensity, time / duration);
            yield return null;
        }

        directionalLight.intensity = targetIntensity;
    }*/

    public static void TimeZone()
    {
        DateTime utcDate = DateTime.UtcNow;
        DateTime localDate = DateTime.Now;

        // Convert UTC to each city’s time zone
        DateTime estDate = TimeZoneInfo.ConvertTimeFromUtc(utcDate, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")); // Orlando (EST)
        DateTime cetDate = TimeZoneInfo.ConvertTimeFromUtc(utcDate, TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time")); // Paris, (Change this one Stockholm)
        DateTime jstDate = TimeZoneInfo.ConvertTimeFromUtc(utcDate, TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time")); // Tokyo
        DateTime cstDate = TimeZoneInfo.ConvertTimeFromUtc(utcDate, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time")); // Beijing
        DateTime pstDate = TimeZoneInfo.ConvertTimeFromUtc(utcDate, TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time")); // Sacramento

        // Only print the time part
        string estTimeOnly = estDate.ToString("HH:mm:ss");
        string cetTimeOnly = cetDate.ToString("HH:mm:ss");
        string jstTimeOnly = jstDate.ToString("HH:mm:ss");
        string cstTimeOnly = cstDate.ToString("HH:mm:ss");
        string pstTimeOnly = pstDate.ToString("HH:mm:ss");

        string[] cultureNames = { "en-US"};

        foreach (var cultureName in cultureNames)
        {
            var culture = new System.Globalization.CultureInfo(cultureName);
            Debug.Log($"{culture.NativeName}:");

            //Debug.Log($"   Local date and time: {localDate.ToString(culture)}, {localDate.Kind}");
            Debug.Log($"   UTC date and time:   {utcDate.ToString(culture)}, {utcDate.Kind}");
            Debug.Log($"   EST (Orlando):       {estTimeOnly}");
            Debug.Log($"   CET (Paris):         {cetTimeOnly}");
            Debug.Log($"   JST (Tokyo):         {jstTimeOnly}");
            Debug.Log($"   CST (Beijing):       {cstTimeOnly}");
            Debug.Log($"   PST (Sacramento):    {pstTimeOnly}");
        }
    }

}