using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeWeather : MonoBehaviour
{
    [Header("Skybox")]
    /*public Material orlandoSkybox;
    public Material parisSkybox;
    public Material tokyoSkybox;
    public Material athensSkybox;
    public Material stockholmSkybox;*/

    [Header("Light")]
    public Light directionalLight;
    public Color lightColor;
    public float targetIntensity;
    public Vector3 lightRotation;

    public WeatherManager m;

    [Header("Input Actions")]
    private InputSystem_Actions actions;
    private InputAction orlandoWeather;
    private InputAction parisWeather;
    private InputAction tokyoWeather;
    private InputAction athensWeather;
    private InputAction stockholmWeather;

    void Awake()
    {
        actions = new InputSystem_Actions();
        orlandoWeather = actions.Player.Orlando;
        parisWeather = actions.Player.Paris;
        tokyoWeather = actions.Player.Tokyo;
        athensWeather = actions.Player.Athens;
        stockholmWeather = actions.Player.Stockholm;
    }

    void OnEnable()
    {
        actions.Player.Enable(); // Enable the Action Map

        // Subscribe to the performed event
        orlandoWeather.performed += ChangeToOrlandoSkybox;
        parisWeather.performed += ChangeToParisSkybox;
        tokyoWeather.performed += ChangeToTokyoSkybox;
        athensWeather.performed += ChangeToAthensSkybox;
        stockholmWeather.performed += ChangeToStockholmSkybox;
    }

    void OnDisable()
    {
        // Unsubscribe from the event
        orlandoWeather.performed -= ChangeToOrlandoSkybox;
        parisWeather.performed -= ChangeToParisSkybox;
        tokyoWeather.performed -= ChangeToTokyoSkybox;
        athensWeather.performed -= ChangeToAthensSkybox;
        stockholmWeather.performed -= ChangeToStockholmSkybox;

        actions.Player.Disable(); // Disable the Action Map
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m = new WeatherManager();
        StartCoroutine(m.GetWeatherXML_1(m.OnXMLDataLoaded));
        
        directionalLight = GetComponent<Light>();
        //RenderSettings.skybox = orlandoSkybox;

        if (directionalLight != null)
        {
            directionalLight.color = Color.white;

            // Set the initial intensity
            directionalLight.intensity = targetIntensity;
            Debug.Log("Directional Light intensity set to: " + directionalLight.intensity);
        }
        else
        {
            Debug.LogError("Directional Light not found or assigned!");
        }
    }
    void Update()
    {
        directionalLight.transform.Rotate(Vector3.right * 10f * Time.deltaTime);
        //directionalLight.color = Color.blue;
        //ChangeToOrlandoSkybox();
    }

    public void ChangeToOrlandoSkybox(InputAction.CallbackContext context)
    {
        //RenderSettings.skybox = orlandoSkybox;
        directionalLight.color = Color.blue;

    }

    public void ChangeToParisSkybox(InputAction.CallbackContext context)
    {
        //RenderSettings.skybox = parisSkybox;
        directionalLight.color = Color.blue;
    }

    public void ChangeToTokyoSkybox(InputAction.CallbackContext context)
    {
        //RenderSettings.skybox = tokyoSkybox;
        directionalLight.color = Color.blue;
    }

    public void ChangeToAthensSkybox(InputAction.CallbackContext context)
    {
        //RenderSettings.skybox = athensSkybox;
        directionalLight.color = Color.blue;
    }

    public void ChangeToStockholmSkybox(InputAction.CallbackContext context)
    {
        //RenderSettings.skybox = stockholmSkybox;
        directionalLight.color = Color.blue;
    }
}
