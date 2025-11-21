using System;
using System.Globalization;
using System.Collections;
using UnityEngine;
using static ChangeWeather;
using System.Collections.Generic;
using GameAnalyticsSDK;

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

    [Range(0, 360)]
    public float lightRange;


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
        DateTime localTime = GetCityLocalTime();

        lightRange = GetSunAngleFromTime(localTime);

        directionalLight.transform.rotation = Quaternion.Euler(lightRange, 0f, 0f);

        float timeBasedIntensity = GetLightIntensity(localTime);
        directionalLight.intensity = timeBasedIntensity;
    }

    public void ChangeCity()
    {
        switch (cityState)
        {
            case CityState.Orlando:
                StartCoroutine(routine: m.GetWeatherXML_1(weatherParser.ParseWeather));
                break;

            case CityState.Paris:
                StartCoroutine(routine: m.GetWeatherXML_2(weatherParser.ParseWeather));
                break;

            case CityState.Tokyo:
                StartCoroutine(routine: m.GetWeatherXML_3(weatherParser.ParseWeather));
                break;

            case CityState.Sacramento:
                StartCoroutine(routine: m.GetWeatherXML_4(weatherParser.ParseWeather));
                break;

            case CityState.Beijing:
                StartCoroutine(routine: m.GetWeatherXML_5(weatherParser.ParseWeather));
                break;
        }

        DateTime localTime = GetCityLocalTime();
        directionalLight.intensity = GetLightIntensity(localTime);

        GameAnalytics.NewDesignEvent("CityChange");
        GameAnalytics.NewDesignEvent("CityChange:" + cityState);
    }

    public static void TimeZone()
    {
        DateTime utcDate = DateTime.UtcNow;
        DateTime localDate = DateTime.Now;

        // Convert UTC to each city’s time zone
        DateTime estDate = TimeZoneInfo.ConvertTimeFromUtc(utcDate, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")); // Orlando (EST)
        DateTime cetDate = TimeZoneInfo.ConvertTimeFromUtc(utcDate, TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time")); // Paris, (Change this one Stockholm)
        DateTime jstDate = TimeZoneInfo.ConvertTimeFromUtc(utcDate, TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time")); // Tokyo
        DateTime cstDate = TimeZoneInfo.ConvertTimeFromUtc(utcDate, TimeZoneInfo.FindSystemTimeZoneById("China Standard Time")); // Beijing
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


    float GetSunAngleFromTime(DateTime localTime)
    {
        float hour = localTime.Hour;
        float minute = localTime.Minute;

        // Time in hours, including minutes
        float timeInHours = hour + (minute / 60f);

        // Shift so 6:00 = 0h
        float shifted = timeInHours - 6f;
        if (shifted < 0f)
            shifted += 24f;

        // Fraction of 24h
        float fractionOfDay = shifted / 24f;

        // Map to 0–360 degrees
        float angle = fractionOfDay * 360f;
        return angle;
    }

    DateTime GetCityLocalTime()
    {
        DateTime utcNow = DateTime.UtcNow;

        switch (cityState)
        {
            case CityState.Orlando:
                return TimeZoneInfo.ConvertTimeFromUtc(
                    utcNow,
                    TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")
                );

            case CityState.Paris:
                return TimeZoneInfo.ConvertTimeFromUtc(
                    utcNow,
                    TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time")
                );

            case CityState.Tokyo:
                return TimeZoneInfo.ConvertTimeFromUtc(
                    utcNow,
                    TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time")
                );

            case CityState.Sacramento:
                return TimeZoneInfo.ConvertTimeFromUtc(
                    utcNow,
                    TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time")
                );

            case CityState.Beijing:
                return TimeZoneInfo.ConvertTimeFromUtc(
                    utcNow,
                    TimeZoneInfo.FindSystemTimeZoneById("China Standard Time")
                );

            default:
                return DateTime.Now;
        }
    }

    float GetLightIntensity(DateTime localTime)
    {
        float hour = localTime.Hour + (localTime.Minute / 60f);

        // Daylight window (adjust if needed)
        float sunrise = 6f;     // 6am
        float noon = 12f;       // 1pm
        float sunset = 18f;     // 6pm

        // Default intensity curve based on time of day
        targetIntensity = 0f;

        if (hour >= sunrise && hour <= sunset)
        {
            // Distance from noon: peak intensity = noon
            float distance = Mathf.Abs(hour - noon);

            // Max distance = 6 hours (12 to 6 or 12 to 18)
            float normalized = Mathf.InverseLerp(6f, 0f, distance);

            // Base intensity curve
            targetIntensity = normalized * 80f; // Base max = 80
        }


        // Set target intensity

        if (RenderSettings.skybox == sunnySkybox)
        {
            targetIntensity *= 1.5f; // Sunny to brighter
            directionalLight.color = new Color32(255, 240, 0, 255);

        }
        else if (RenderSettings.skybox == cloudySkybox)
        {
            targetIntensity *= 0.6f; // dim cloudy
            directionalLight.color = new Color32(255, 200, 0, 255);
        }
        else if (RenderSettings.skybox == fogSkybox)
        {
            targetIntensity *= 0.3f; // very dim
            directionalLight.color = new Color32(200, 160, 0, 255);
        }
        else if (RenderSettings.skybox == overcastSkybox)
        {
            targetIntensity *= 0.5f;
            directionalLight.color = new Color32(255, 255, 200, 255);
        }

        return Mathf.Clamp(targetIntensity, 0f, 120f);
    }
}