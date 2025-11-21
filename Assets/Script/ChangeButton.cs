using TMPro;
using UnityEngine;

public class ChangeButton : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown cityDropdown;
    [SerializeField] private ChangeWeather changeWeather;

    private void Awake()
    {
        // Optional auto-wire if not set in inspector
        if (cityDropdown == null)
            cityDropdown = GetComponent<TMP_Dropdown>();
    }

    private void Start()
    {
        // Clear any existing options and fill from enum
        cityDropdown.ClearOptions();

        var options = new System.Collections.Generic.List<string>();
        foreach (var city in System.Enum.GetNames(typeof(ChangeWeather.CityState)))
        {
            options.Add(city);
        }
        cityDropdown.AddOptions(options);

        // Set current value based on ChangeWeather.cityState
        cityDropdown.value = (int)changeWeather.cityState;
        cityDropdown.RefreshShownValue();

        // Listen for changes
        cityDropdown.onValueChanged.AddListener(OnCityChanged);
    }

    private void OnDestroy()
    {
        cityDropdown.onValueChanged.RemoveListener(OnCityChanged);
    }

    private void OnCityChanged(int dropdownIndex)
    {
        changeWeather.cityState = (ChangeWeather.CityState)dropdownIndex;

        changeWeather.ChangeCity();
        changeWeather.changeSkyBox();
    }
}
