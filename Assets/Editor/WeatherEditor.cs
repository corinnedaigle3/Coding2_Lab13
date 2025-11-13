using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ChangeWeather))]
public class WeatherEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ChangeWeather changeWeather = (ChangeWeather)target;

        if (GUI.changed)
        {
            changeWeather.ChangeCity();
        }
    }
}
