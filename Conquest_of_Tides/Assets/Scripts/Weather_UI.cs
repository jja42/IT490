using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weather_UI : MonoBehaviour
{
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
        Mist,
        Clear_Sky
    }

    public void SetWeather(string weather_type, int temp, int humid, int visibility, float wind_speed)
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
    }

    #region Get_Weather_Vars
    WeatherType GetWeather(string weather_type)
    {
        switch (weather_type)
        {
            case "rain":
                return WeatherType.Rain;
            case "thunderstorm":
                return WeatherType.Thunderstorm;
            case "snow":
                return WeatherType.Snow;
            case "mist":
                return WeatherType.Mist;
            case "clear sky":
                return WeatherType.Clear_Sky;
            default:
                return WeatherType.Clear_Sky;
        }
    }

    Temperature GetTemperature(int temp)
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
                Weather_Manager.instance.turn_damage = 10;
                return "Ships will take Damage each Turn";
            case WeatherType.Snow:
                return "Snowfall";
            case WeatherType.Mist:
                Weather_Manager.instance.retreat_decrease = true;
                return "Decreased Retreat Cost";
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
                Weather_Manager.instance.resistance_reduction = 10;
                return "All Resistances Reduced";
            case Temperature.Warm:
                return "No Effect";
            case Temperature.Hot:
                Weather_Manager.instance.weakness_enhancement = 10;
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
                Weather_Manager.instance.move_damage_reduction = 10;
                return "All Move Damage Reduced";
            case Humidity.Normal:
                return "No Effect";
            case Humidity.High:
                Weather_Manager.instance.increased_move_cost = true;
                return "Increased Move Cost";
            default:
                return "No Effect";
        }
    }

    string GetVisibilityEffect(float visibility)
    {
        Weather_Manager.instance.accuracy = visibility / 100;
        return "Accuracy of Moves set to "+ visibility.ToString() + "%";
    }

    string GetWindSpeedEffect(WindSpeed wind_speed)
    {
        switch (wind_speed)
        {
            case WindSpeed.Calm:
                return "No Effect";
            case WindSpeed.Breeze:
                Weather_Manager.instance.double_draw = true;
                return "Draw One Additional Card Per Turn";
            case WindSpeed.Windy:
                Weather_Manager.instance.double_fortify = true;
                return "Fortifications are Doubled when Applied";
            default:
                return "No Effect";
        }
    }
    #endregion
}
