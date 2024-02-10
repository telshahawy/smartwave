using System;
using System.Collections.Generic;
using System.Text;

namespace SW.Framework.Utilities
{
    public static class GoogleMapsUtility
    {
        public static string LocationUrl(float Latitude, float Longtitude)
        {
            // this url is according to latest google api documentation at the moment 03/2021
            // https://developers.google.com/maps/documentation/urls/get-started

            StringBuilder url = new StringBuilder();

            url.Append("https://www.google.com/maps/search/?api=1"); // Base Google Search Url
            url.Append("&map_action=pano");                          // Show pin on the map
            url.Append($"&query={Latitude},{Longtitude}");           // Coordinates on the map

            return url.ToString();
        }
        public static string AreaName(float Latitude, float Longtitude, string googleMapAPIKey)
        {
            // this url is according to latest google api documentation at the moment 03/2021
            // https://developers.google.com/maps/documentation/geocoding/overview#reverse-example

            // https://maps.googleapis.com/maps/api/geocode/json?latlng=40.714224,-73.961452&key=YOUR_API_KEY

            if (string.IsNullOrEmpty(googleMapAPIKey))
                return "";

            StringBuilder url = new StringBuilder();

            url.Append("https://maps.googleapis.com/maps/api/geocode/json"); // Base Google Search Url
            url.Append($"?latlng={Latitude},{Longtitude}");           // Coordinates on the map
            url.Append($"&key={googleMapAPIKey}");

            return url.ToString();

            return "Dummy Area name";

        }
    }
}
