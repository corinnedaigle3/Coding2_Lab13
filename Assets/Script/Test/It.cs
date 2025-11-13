using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class It : MonoBehaviour
{
    [Header("Skybox")]
    /*public Material orlandoSkybox;
    public Material parisSkybox;
    public Material tokyoSkybox;
    public Material athensSkybox;
    public Material stockholmSkybox;*/

    public Material sunnySkybox;
    public Material cloudySkybox;
    public Material rainySkybox;
    public Material snowySkybox;

    [Header("Light")]
    public Light directionalLight;
    public float targetIntensity;
    //public Vector3 lightRotation;

    public WeatherManager m;

    [Header("Input Actions")]
    private InputSystem_Actions actions;
    private InputAction orlandoWeather;
    private InputAction parisWeather;
    private InputAction tokyoWeather;
    private InputAction athensWeather;
    private InputAction stockholmWeather;

    void OnEnable()
    {
        if (actions == null)
        {
            actions = new InputSystem_Actions();
            orlandoWeather = actions.Player.Orlando;
            parisWeather = actions.Player.Paris;
            tokyoWeather = actions.Player.Tokyo;
            athensWeather = actions.Player.Athens;
            stockholmWeather = actions.Player.Stockholm;
        }

        actions.Player.Enable();

        // Subscribe to input events
        orlandoWeather.performed += ChangeToOrlandoSkybox;
        parisWeather.performed += ChangeToParisSkybox;
        tokyoWeather.performed += ChangeToTokyoSkybox;
        athensWeather.performed += ChangeToAthensSkybox;
        stockholmWeather.performed += ChangeToStockholmSkybox;
    }

    void OnDisable()
    {
        if (actions == null) return; // prevent null refs on disable

        // Unsubscribe
        orlandoWeather.performed -= ChangeToOrlandoSkybox;
        parisWeather.performed -= ChangeToParisSkybox;
        tokyoWeather.performed -= ChangeToTokyoSkybox;
        athensWeather.performed -= ChangeToAthensSkybox;
        stockholmWeather.performed -= ChangeToStockholmSkybox;

        actions.Player.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m = new WeatherManager();
        StartCoroutine(m.GetWeatherXML_1(m.OnXMLDataLoaded));
        directionalLight = Object.FindFirstObjectByType<Light>();

        directionalLight.intensity = 1.2f;
    }

    void Update()
    {
        //directionalLight.transform.Rotate(Vector3.right * 10f * Time.deltaTime);
    }

    public void ChangeToOrlandoSkybox(InputAction.CallbackContext context)
    {
        StartCoroutine(m.GetWeatherXML_1(m.OnXMLDataLoaded));

        RenderSettings.skybox = sunnySkybox;
    }

    public void ChangeToParisSkybox(InputAction.CallbackContext context)
    {
        StartCoroutine(m.GetWeatherXML_2(m.OnXMLDataLoaded));

        RenderSettings.skybox = snowySkybox;
    }

    public void ChangeToTokyoSkybox(InputAction.CallbackContext context)
    {
        StartCoroutine(m.GetWeatherXML_3(m.OnXMLDataLoaded));

        RenderSettings.skybox = rainySkybox;
        StartCoroutine(LerpLightIntensity(0.3f, 2f));
    }

    public void ChangeToAthensSkybox(InputAction.CallbackContext context)
    {
        StartCoroutine(m.GetWeatherXML_4(m.OnXMLDataLoaded));

        RenderSettings.skybox = cloudySkybox;
    }

    public void ChangeToStockholmSkybox(InputAction.CallbackContext context)
    {
        StartCoroutine(m.GetWeatherXML_5(m.OnXMLDataLoaded));

        RenderSettings.skybox = snowySkybox;
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