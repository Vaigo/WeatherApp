using Newtonsoft.Json;
using System.Collections.Generic;

public class Daily
{
    public List<double> temperature_2m_max { get; set; }
}

public class Root
{
    public Daily daily { get; set; }
}

class GeoAPIResponse
{
    [JsonProperty("geoplugin_latitude")] public string Latitude { get; set; }
    [JsonProperty("geoplugin_longitude")] public string Longitude { get; set; }
}