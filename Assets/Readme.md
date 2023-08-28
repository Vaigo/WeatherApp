###################################################################################################

# Simple Weather App Readme

Hey there! Welcome to the Simple Weather App project. This ain't no fancy documentation, just a quick rundown of what's going on in this code. So let's dive in!

## Project Overview

This project fetches some geographic and weather data using Unity and C#. It's all about getting the current temperature based on your IP address location. Nothing too complicated, just a bit of API magic.

## GetGeographicData

In this part of the code, we're fetching the geographic data based on your public IP address. Here's what's happening:

1. We send a web request to `api.ipify.org` to get your public IP address.
2. With that IP address, we then fetch your latitude and longitude using `geoplugin.net`.
3. We store all this juicy data and shout it out through an event for others to hear.

## GetTemperatureData

This part focuses on grabbing the temperature data for your location. Here's the scoop:

1. We're subscribed to the `OnGeoDataFetched` event from the previous part.
2. Once we get your location (latitude and longitude), we construct a URL and send a web request to the temperature API (`api.open-meteo.com`).
3. We decode the JSON response, grab today's max temperature, and display it with some UI flair.

## Constants

Just a bunch of URLs and a timezone string. No magic here, just plain old constants.

## JSON Classes

These classes are our buddies for parsing the JSON data we get from the APIs. They help make sense of the data and provide structure.


