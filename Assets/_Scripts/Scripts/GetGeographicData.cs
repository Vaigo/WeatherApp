using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System;

//To fetch the Public ip and from there it will fetch the latitude and longitude
public class GetGeographicData : MonoBehaviour
{
    public Action<string, string> OnGeoDataFetched;  // An event that can be subscribed to with two string parameters

    [HideInInspector]
    public string PublicIP;  // A variable to store the public IP address
    [HideInInspector]
    public string Latitude;  // A variable to store latitude data
    [HideInInspector]
    public string Longitude;  // A variable to store longitude data

   
    void Start()
    {
        StartCoroutine(GetGeographicDataFromIP());  // Start fetching geographic data from the IP address
    }

    IEnumerator GetGeographicDataFromIP()
    {
        // Send a web request to get the public IP address
        using (UnityWebRequest request = UnityWebRequest.Get(Constants.URL_GetPublicIP))
        {
            request.timeout = 10;
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                // Store the fetched public IP address and proceed to fetch geographic data
                PublicIP = request.downloadHandler.text.Trim();
                yield return StartCoroutine(GetGeographicDataFromPlugin());
            }
            else
            {
                Debug.LogError($"Failed to get public IP: {request.downloadHandler.text}");
            }
        }
    }

    IEnumerator GetGeographicDataFromPlugin()
    {
        // Send a web request to get geographic data based on the public IP address
        using (UnityWebRequest request = UnityWebRequest.Get(Constants.URL_GetGeographicData + PublicIP))
        {
            request.timeout = 1;
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                // Deserialize the response to get latitude and longitude, then trigger the event
                GeoAPIResponse geoData = JsonConvert.DeserializeObject<GeoAPIResponse>(request.downloadHandler.text);
                Latitude = geoData.Latitude;
                Longitude = geoData.Longitude;

                Debug.Log($"IP: {PublicIP}");
                Debug.Log($"Latitude: {Latitude}");
                Debug.Log($"Longitude: {Longitude}");
                OnGeoDataFetched?.Invoke(Latitude, Longitude);
            }
            else
            {
                Debug.LogError($"Failed to get geographic data: {request.downloadHandler.text}");
            }
        }
    }
}
