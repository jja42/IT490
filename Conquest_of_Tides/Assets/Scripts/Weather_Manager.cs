using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weather_Manager : MonoBehaviour
{
    public static Weather_Manager instance;
    public int turn_damage;
    public bool typeless_cost;
    public int resistance_reduction;
    public int weakness_enhancement;
    public int move_damage_reduction;
    public int decreased_hp;
    public bool double_draw;
    public bool double_fortify;
    public Image Weather_Img;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
    public GameObject Weather_Popup;
    public Text Weather_Type;
    public Text Temp;
    public Text Humid;
    public Text Visibility;
    public Text Wind_Speed;
    public WeatherType w_weathertype;
    public Temperature w_temperature;
    public Humidity w_humidity;
    public float w_visibility;
    public WindSpeed w_windspeed;

    public enum Temperature
    {
        Cold,
        Warm,
        Hot
    }
    public enum Humidity
    {
        Low,
        Normal,
        High
    }

    public enum WindSpeed
    {
        Calm,
        Breeze,
        Windy
    }
    public enum WeatherType
    {
        Rain,
        Thunderstorm,
        Snow,
        Cloudy,
        Clear_Sky
    }

    public void SetWeather(string weather_type, float temp, int humid, int visibility, float wind_speed)
    {
        w_weathertype = GetWeather(weather_type);
        w_temperature = GetTemperature(temp);
        w_humidity = GetHumidity(humid);
        w_visibility = GetVisibility(visibility);
        w_windspeed = GetWindSpeed(wind_speed);
        GenerateWeather();
    }

    public void GenerateWeather()
    {
        Weather_Type.text = "Weather Type (" + w_weathertype + ") - " + GetWeatherTypeEffect(w_weathertype);
        Temp.text = "Temperature (" + w_temperature + ") - " + GetTemperatureEffect(w_temperature);
        Humid.text = "Humidity (" + w_humidity + ") - " + GetHumidityEffect(w_humidity);
        Visibility.text = "Visibility (" + w_visibility.ToString() + "%) - " + GetVisibilityEffect(w_visibility);
        Wind_Speed.text = "Wind Speed (" + w_windspeed + ") - " + GetWindSpeedEffect(w_windspeed);
        Weather_Img.sprite = Resources.Load<Sprite>("temp_assets/" + w_weathertype);
    }

    #region Get_Weather_Vars
    WeatherType GetWeather(string weather_type)
    {
        switch (weather_type)
        {
            case "Rain":
                return WeatherType.Rain;
            case "Thunderstorm":
                return WeatherType.Thunderstorm;
            case "Snow":
                return WeatherType.Snow;
            case "Clouds":
                return WeatherType.Cloudy;
            case "Clear":
                return WeatherType.Clear_Sky;
            default:
                return WeatherType.Clear_Sky;
        }
    }

    Temperature GetTemperature(float temp)
    {
        if (temp < 50)
            return Temperature.Cold;
        if (temp > 80)
            return Temperature.Hot;
        return Temperature.Warm;
    }

    Humidity GetHumidity(int humid)
    {
        if (humid < 30)
            return Humidity.Low;
        if (humid > 50)
            return Humidity.High;
        return Humidity.Normal;
    }

    float GetVisibility(int visibility)
    {
        return (visibility / 100);
    }

    WindSpeed GetWindSpeed(float wind_speed)
    {
        if (wind_speed > 25)
            return WindSpeed.Windy;
        if (wind_speed > 15)
            return WindSpeed.Breeze;
        return WindSpeed.Calm;

    }
    #endregion
    #region Get_Weather_Effects
    string GetWeatherTypeEffect(WeatherType weather_type)
    {
        switch (weather_type)
        {
            case WeatherType.Rain:
                return "Rainfall";
            case WeatherType.Thunderstorm:
                turn_damage = 10;
                return "Ships will take Damage each Turn";
            case WeatherType.Snow:
                return "Snowfall";
            case WeatherType.Cloudy:
                typeless_cost = true;
                return "All Costs are Typeless";
            case WeatherType.Clear_Sky:
                return "No Effect";
            default:
                return "No Effect";
        }
    }

    string GetTemperatureEffect(Temperature temp)
    {
        switch (temp)
        {
            case Temperature.Cold:
                resistance_reduction = 10;
                return "All Resistances Reduced";
            case Temperature.Warm:
                return "No Effect";
            case Temperature.Hot:
                weakness_enhancement = 10;
                return "All Weaknesses Increased";
            default:
                return "No Effect";
        }
    }

    string GetHumidityEffect(Humidity humidity)
    {
        switch (humidity)
        {
            case Humidity.Low:
                move_damage_reduction = 10;
                return "All Move Damage Reduced";
            case Humidity.Normal:
                return "No Effect";
            case Humidity.High:
                decreased_hp = 10;
                return "Decreased HP";
            default:
                return "No Effect";
        }
    }

    string GetVisibilityEffect(float visibility)
    {
        return "Accuracy of Moves set to " + visibility.ToString() + "%";
    }

    string GetWindSpeedEffect(WindSpeed wind_speed)
    {
        switch (wind_speed)
        {
            case WindSpeed.Calm:
                return "No Effect";
            case WindSpeed.Breeze:
                double_draw = true;
                return "Draw One Additional Card Per Turn";
            case WindSpeed.Windy:
                double_fortify = true;
                return "Fortifications are Doubled when Applied";
            default:
                return "No Effect";
        }
    }
    #endregion
    public void EnableWeatherUI()
    {
        Weather_Popup.SetActive(true);
    }
    public void DisableWeatherUI()
    {
        Weather_Popup.SetActive(false);
    }
    public void GetData()
    {
        StartCoroutine(WebRequest.instance.Api_Request());
    }
    public void GetHistoricalData(string date)
    {
        StartCoroutine(WebRequest.instance.Historical_Api_Request(date));
    }
}
