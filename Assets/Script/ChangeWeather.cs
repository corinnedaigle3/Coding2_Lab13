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
    public Material rainySkybox;
    public Material snowySkybox;

    [Header("Default")]
    public Material defaultSkybox;
    public bool HideSkybox;

    [Header("Light")]
    public Light directionalLight;
    public float targetIntensity;
    public Color newColor;

    public WeatherParser weatherParser;
    public WeatherManager m;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m = new WeatherManager();
        weatherParser = GetComponent<WeatherParser>();

        StartCoroutine(m.GetWeatherXML_1(m.OnXMLDataLoaded));
        ChangeCity();
        Main();
        HideSkybox = false;
    }

    void Update()
    {
        directionalLight.transform.Rotate(Vector3.right * 10f * Time.deltaTime);
        directionalLight.color = newColor;
        directionalLight.intensity = targetIntensity;
        SkyboxDefault();
    }

    public void ChangeCity()
    {
        switch (cityState)
        {
            case CityState.Orlando:
                StartCoroutine(routine: m.GetWeatherXML_1(weatherParser.ParseWeather));

                //StartCoroutine(m.GetWeatherXML_1(m.OnXMLDataLoaded));
                RenderSettings.skybox = sunnySkybox;
                targetIntensity = 2;
                newColor = Color.blue;
                break;

            case CityState.Paris:
                StartCoroutine(routine: m.GetWeatherXML_2(weatherParser.ParseWeather));
                //StartCoroutine(m.GetWeatherXML_2(m.OnXMLDataLoaded));
                RenderSettings.skybox = rainySkybox;
                targetIntensity = 8;
                newColor = Color.white;
                break;

            case CityState.Tokyo:
                StartCoroutine(routine: m.GetWeatherXML_3(weatherParser.ParseWeather));
                RenderSettings.skybox = cloudySkybox;
                targetIntensity = 15;
                newColor = Color.pink;
                break;

            case CityState.Sacramento:
                StartCoroutine(routine: m.GetWeatherXML_4(weatherParser.ParseWeather));
                //StartCoroutine(m.GetWeatherXML_4(m.OnXMLDataLoaded));
                RenderSettings.skybox = sunnySkybox;
                targetIntensity = 30;
                newColor = Color.yellow;
                break;

            case CityState.Beijing:
                StartCoroutine(routine: m.GetWeatherXML_5(weatherParser.ParseWeather));
                //StartCoroutine(m.GetWeatherXML_5(m.OnXMLDataLoaded));
                RenderSettings.skybox = snowySkybox;
                targetIntensity = 100;
                newColor = Color.green;
                break;
        }
    }

    public void SkyboxDefault()
    {
        if (HideSkybox)
        {
            RenderSettings.skybox = defaultSkybox;
            return;
        }

        switch (cityState)
        {
            case CityState.Orlando:
                RenderSettings.skybox = sunnySkybox;
                break;

            case CityState.Paris:
                RenderSettings.skybox = rainySkybox;
                break;

            case CityState.Tokyo:
                RenderSettings.skybox = cloudySkybox;
                break;

            case CityState.Sacramento:
                RenderSettings.skybox = sunnySkybox;
                break;

            case CityState.Beijing:
                RenderSettings.skybox = snowySkybox;
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

    public static void Main()
    {
        DateTime utcDate = DateTime.UtcNow;
        DateTime localDate = DateTime.Now;

        // Convert UTC to each city’s time zone
        DateTime estDate = TimeZoneInfo.ConvertTimeFromUtc(utcDate, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")); // Orlando (EST)
        DateTime cetDate = TimeZoneInfo.ConvertTimeFromUtc(utcDate, TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time")); // Paris, (Change this one Stockholm)
        DateTime jstDate = TimeZoneInfo.ConvertTimeFromUtc(utcDate, TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time")); // Tokyo
        DateTime cstDate = TimeZoneInfo.ConvertTimeFromUtc(utcDate, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time")); // Beijing
        DateTime pstDate = TimeZoneInfo.ConvertTimeFromUtc(utcDate, TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time")); // Sacramento

        string[] cultureNames = { "en-US"};

        foreach (var cultureName in cultureNames)
        {
            var culture = new System.Globalization.CultureInfo(cultureName);
/*            Debug.Log($"{culture.NativeName}:");

            Debug.Log($"   Local date and time: {localDate.ToString(culture)}, {localDate.Kind}");
            Debug.Log($"   UTC date and time:   {utcDate.ToString(culture)}, {utcDate.Kind}");
            Debug.Log($"   EST (Orlando):       {estDate.ToString(culture)}");
            Debug.Log($"   CET (Paris):         {cetDate.ToString(culture)}");
            Debug.Log($"   JST (Tokyo):         {jstDate.ToString(culture)}");
            Debug.Log($"   CST (Beijing):        {cstDate.ToString(culture)}");
            Debug.Log($"   PST (Sacramento):        {pstDate.ToString(culture)}");*/
        }
    }
}