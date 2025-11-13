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
    /*public Material orlandoSkybox;
    public Material parisSkybox;
    public Material tokyoSkybox;
    public Material athensSkybox;
    public Material stockholmSkybox;*/

    [Header("Light")]
    //public Light directionalLight;
    //public float targetIntensity;
    //public Vector3 lightRotation;

    public WeatherManager m;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m = new WeatherManager();
        StartCoroutine(m.GetWeatherXML_1(m.OnXMLDataLoaded));
        
        //directionalLight = GetComponent<Light>();
        //RenderSettings.skybox = orlandoSkybox;
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
                //RenderSettings.skybox = orlandoSkybox;
                break;
            case CityState.Paris:
                Debug.Log("Paris");
                StartCoroutine(m.GetWeatherXML_2(m.OnXMLDataLoaded));
                //RenderSettings.skybox = parisSkybox;
                break;
            case CityState.Tokyo:
                Debug.Log("Tokyo");
                StartCoroutine(m.GetWeatherXML_3(m.OnXMLDataLoaded));
                //RenderSettings.skybox = tokyoSkybox;
                break;
            case CityState.Athens:
                Debug.Log("Athens");
                StartCoroutine(m.GetWeatherXML_4(m.OnXMLDataLoaded));
                //RenderSettings.skybox = athensSkybox;
                break;
            case CityState.Stockholm:
                Debug.Log("Stockholm");
                StartCoroutine(m.GetWeatherXML_5(m.OnXMLDataLoaded));
                //RenderSettings.skybox = stockholmSkybox;
                break;
        }
    }
}