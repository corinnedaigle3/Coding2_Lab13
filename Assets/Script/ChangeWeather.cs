using System;
using System.Globalization;
using System.Collections;
using UnityEngine;

public class ChangeWeather : MonoBehaviour
{
    public enum CityState
    {
        Orlando,
        Paris,
        Tokyo,
        California,
        Stockholm
    }

    [Header("Current City")]
    public CityState cityState;

    [Header("Skybox")]
    public Material sunnySkybox;
    public Material cloudySkybox;
    public Material rainySkybox;
    public Material snowySkybox;

    [Header("Light")]
    public Light directionalLight;
    public float targetIntensity;
    public float lightRotationSpeed;
    public Vector3 currentSunRotation;

    public WeatherManager m;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m = new WeatherManager();
        StartCoroutine(m.GetWeatherXML_1(m.OnXMLDataLoaded));
        directionalLight = UnityEngine.Object.FindFirstObjectByType<Light>();

        Main();
    }

    void Update()
    {
        directionalLight.transform.Rotate(Vector3.right * lightRotationSpeed * Time.deltaTime);
        //ChangeToOrlandoSkybox();
    }

    public void ChangeCity()
    {
        switch (cityState)
        {
            case CityState.Orlando:
                Debug.Log("Orlando");
                StartCoroutine(m.GetWeatherXML_1(m.OnXMLDataLoaded));
                RenderSettings.skybox = sunnySkybox;
                break;

            case CityState.Paris:
                Debug.Log("Paris");
                StartCoroutine(m.GetWeatherXML_2(m.OnXMLDataLoaded));
                RenderSettings.skybox = rainySkybox;
                break;

            case CityState.Tokyo:
                Debug.Log("Tokyo");
                StartCoroutine(m.GetWeatherXML_3(m.OnXMLDataLoaded));
                RenderSettings.skybox = cloudySkybox;
                break;

            case CityState.California:
                Debug.Log("California");
                StartCoroutine(m.GetWeatherXML_4(m.OnXMLDataLoaded));
                RenderSettings.skybox = sunnySkybox;
                //StartCoroutine(LerpLightIntensity(0.3f, 2f));
                break;

            case CityState.Stockholm:
                Debug.Log("Stockholm");
                StartCoroutine(m.GetWeatherXML_5(m.OnXMLDataLoaded));
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
        DateTime cetDate = TimeZoneInfo.ConvertTimeFromUtc(utcDate, TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time")); // Paris, Stockholm
        DateTime jstDate = TimeZoneInfo.ConvertTimeFromUtc(utcDate, TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time")); // Tokyo
        DateTime pstDate = TimeZoneInfo.ConvertTimeFromUtc(utcDate, TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time")); // California

        string[] cultureNames = { "en-US"};

        foreach (var cultureName in cultureNames)
        {
            var culture = new System.Globalization.CultureInfo(cultureName);
            Debug.Log($"{culture.NativeName}:");

            Debug.Log($"   Local date and time: {localDate.ToString(culture)}, {localDate.Kind}");
            Debug.Log($"   UTC date and time:   {utcDate.ToString(culture)}, {utcDate.Kind}");
            Debug.Log($"   EST (Orlando):       {estDate.ToString(culture)}");
            Debug.Log($"   CET (Paris / Stockholm):         {cetDate.ToString(culture)}");
            Debug.Log($"   JST (Tokyo):         {jstDate.ToString(culture)}");
            Debug.Log($"   PST (California):        {pstDate.ToString(culture)}");
        }
    }
}