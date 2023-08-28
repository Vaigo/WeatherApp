using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class GetTemperatureData : MonoBehaviour
{
    #region Private Variables
    [SerializeField]
    private TMP_Text m_simpleTxt;  // displaying temperature
    [SerializeField]
    private TMP_Text m_temperatureTxt;  // displaying temperature

    [SerializeField]
    private GetGeographicData m_getGeographicData;  // GetGeographicData script for fetching geographic data
    #endregion

    private void Start()
    {
        // Subscribe to the "OnGeoDataFetched" event and provide a function to run when geographic data is fetched
        m_getGeographicData.OnGeoDataFetched += (latitude, longitude) =>
        {
            StartCoroutine(GetTemperatureDataFromAPI(latitude, longitude));  // Start fetching temperature data
        };
    }

    private IEnumerator GetTemperatureDataFromAPI(string latitude, string longitude)
    {
        // Construct the URL for temperature data using latitude and longitude
        string requestURL = $"{Constants.URL_TemperatureAPI}?latitude={latitude}&longitude={longitude}&timezone={Constants.timezone}&daily=temperature_2m_max";

        using (UnityWebRequest request = UnityWebRequest.Get(requestURL))
        {
            request.timeout = 10;
            yield return request.SendWebRequest();  // Send the web request to fetch temperature data

            if (!string.IsNullOrEmpty(request.downloadHandler.text))
            {
                if (JsonConvert.DeserializeObject<Root>(request.downloadHandler.text) is Root response)
                {
                    if (response.daily.temperature_2m_max?.Count > 0)
                    {
                        int currentTemperature = (int)response.daily.temperature_2m_max[0];  // Get today's temperature
                        Debug.Log($"Day temp: Temperature = {currentTemperature:F1}°C");
                        m_simpleTxt.text = "Today Temperature";  // Update UI text
                        m_temperatureTxt.text= $"{currentTemperature}°C";


                    }
                    else
                    {
                        Debug.LogError("No temperature data found in the response.");
                        m_simpleTxt.text = "No temperature data found in the response.";  // Update UI text
                    }
                }
                else
                {
                    Debug.LogError("Failed to deserialize response.");
                    m_simpleTxt.text = "Failed to deserialize response.";  // Update UI text
                }
            }
            else
            {
                Debug.LogError("Empty response received.");
            }
        }
    }
}
