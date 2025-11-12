using UnityEngine;

public class ChangeWeather : MonoBehaviour
{
    /*public Material orlandoSkybox;
    public Material orlandoSkybox1;
    public Material orlandoSkybox2;
    public Material orlandoSkybox3;
    public Material orlandoSkybox4;*/
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
        //RenderSettings.skybox = orlandoSkybox1;
        directionalLight.color = Color.blue;
        //lightRotation = new Vector3(50f, -30f, 0f);
       //transform.localEulerAngles = lightRotation;

    }

    public void ChangeToOrlandoSkybox1()
    {
        //RenderSettings.skybox = orlandoSkybox1;
        directionalLight.color = Color.blue;
        lightRotation = new Vector3(50f, -30f, 0f);
        transform.localEulerAngles = lightRotation;
    }

    public void ChangeToOrlandoSkybox2()
    {
        //RenderSettings.skybox = orlandoSkybox2;
        directionalLight.color = Color.blue;
        lightRotation = new Vector3(50f, -30f, 0f);
        transform.localEulerAngles = lightRotation;
    }

    public void ChangeToOrlandoSkybox3()
    {
        //RenderSettings.skybox = orlandoSkybox3;
        directionalLight.color = Color.blue;
        lightRotation = new Vector3(50f, -30f, 0f);
        transform.localEulerAngles = lightRotation;
    }

    public void ChangeToOrlandoSkybox4()
    {
        //RenderSettings.skybox = orlandoSkybox4;
        directionalLight.color = Color.blue;
        lightRotation = new Vector3(50f, -30f, 0f);
        transform.localEulerAngles = lightRotation;
    }
}
