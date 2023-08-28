using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants 
{
    //To get the Ip address of the current devie
    public const string URL_GetPublicIP = "https://api.ipify.org/";
    //To get the Longitude and latiude
    public const string URL_GetGeographicData = "http://www.geoplugin.net/json.gp?ip=";
    //To get the weather info example: Temperature
    public const string URL_TemperatureAPI = "https://api.open-meteo.com/v1/forecast";

    public const string timezone = "IST";
}
