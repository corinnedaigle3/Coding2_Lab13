using UnityEngine;

public class ChangeWeather : MonoBehaviour
{
    /*public Material orlandoSkybox;
    public Material parisSkybox;
    public Material tokyoSkybox;
    public Material athensSkybox;
    public Material stockholmSkybox;*/
    public WeatherManager m;

    public Color lightColor;
    public Vector3 lightRotation;

    public Light directionalLight;

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
        directionalLight.transform.Rotate(Vector3.right * 10f * Time.deltaTime);
        //directionalLight.color = Color.blue;
        //ChangeToOrlandoSkybox();
    }

    public void ChangeToOrlandoSkybox()
    {
        //RenderSettings.skybox = orlandoSkybox;
        directionalLight.color = Color.blue;

    }

    public void ChangeToParisSkybox()
    {
        //RenderSettings.skybox = parisSkybox;
        directionalLight.color = Color.blue;
    }

    public void ChangeToTokyoSkybox()
    {
        //RenderSettings.skybox = tokyoSkybox;
        directionalLight.color = Color.blue;
    }

    public void ChangeToAthensSkybox()
    {
        //RenderSettings.skybox = athensSkybox;
        directionalLight.color = Color.blue;
    }

    public void ChangeToStockholmSkybo4()
    {
        //RenderSettings.skybox = stockholmSkybox;
        directionalLight.color = Color.blue;
    }
}
