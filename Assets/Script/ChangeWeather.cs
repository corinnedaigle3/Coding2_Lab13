using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeWeather : MonoBehaviour
{
    public enum CityState
    {
        Orlando,
        Paris,
        Tokyo,
        Athens,
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
    //public Vector3 lightRotation;

    public WeatherManager m;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m = new WeatherManager();
        StartCoroutine(m.GetWeatherXML_1(m.OnXMLDataLoaded));
        directionalLight = Object.FindFirstObjectByType<Light>();
    }

    void Update()
    {
        //directionalLight.transform.Rotate(Vector3.right * 10f * Time.deltaTime);
        //ChangeToOrlandoSkybox();
    }
    public void ChangeCity()
    {
        switch(cityState)
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

            case CityState.Athens:
                Debug.Log("Athens");
                StartCoroutine(m.GetWeatherXML_4(m.OnXMLDataLoaded));
                RenderSettings.skybox = sunnySkybox;
                break;

            case CityState.Stockholm:
                Debug.Log("Stockholm");
                StartCoroutine(m.GetWeatherXML_5(m.OnXMLDataLoaded));
                RenderSettings.skybox = snowySkybox;
                break;
        }
    }

    IEnumerator LerpLightIntensity(float targetIntensity, float duration = 1f)
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
    }
}