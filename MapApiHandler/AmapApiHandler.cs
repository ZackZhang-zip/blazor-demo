using blazor_demo.Data;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace blazor_demo.MapApiHandler
{
    public class AMapApiHandler: IMapApiHandler
    {
        public HttpClient Client{ get; set; } = new HttpClient();
        private string apikey = "e95b5b479b5bb71f8511d6102fe06ee5";
        private string tmplPoiApiUrl = "https://restapi.amap.com/v3/place/text?key={0}&city={1}&keywords={2}&children=1&offset=5&page=1&extensions=all&types={3}";
        private string tmplAddressDecodeApiUrl = "https://restapi.amap.com/v3/geocode/geo?key={0}&city={1}&address={2}";

        public void Process(PropertyData d)
        { 
            try
            {
                string searchPoiText = string.Empty;
                string searchPoiType = string.Empty;
                int searchPoiIndex = 0;

                if(!string.IsNullOrEmpty(d.PropertyName)) searchPoiText = d.PropertyName;
                if(!string.IsNullOrEmpty(d.CorrectedName)) searchPoiText = d.CorrectedName;
                if(!string.IsNullOrEmpty(d.RecommandText)) searchPoiText = d.RecommandText;

                if(!string.IsNullOrEmpty(d.RecommandText) && !string.IsNullOrEmpty(d.Remarks))
                {
                    string[] remarks = d.Remarks.Split(',');
                    searchPoiIndex = Convert.ToInt32(remarks[0].Trim());
                    if(remarks.Length > 1) searchPoiType = remarks[1].Trim();
                }

                d.POIId = string.Empty;
                d.POICity = string.Empty;
                d.POICounty = string.Empty;
                d.POIName = string.Empty;
                d.POIAddress = string.Empty;
                d.POIBizDistrict = string.Empty;
                d.POIType = string.Empty;
                d.POIXY = string.Empty;
                d.Adcode = string.Empty;

                if(!string.IsNullOrEmpty(d.Address) && string.IsNullOrEmpty(d.LngLat))
                    AddressDecode(d);

                HttpResponseMessage response = Client.GetAsync(string.Format(tmplPoiApiUrl, apikey, d.City, searchPoiText, searchPoiType)).Result;
                string content = response.Content.ReadAsStringAsync().Result;
                List<string> errors = new List<string>();
                var responseObj = JsonConvert.DeserializeObject<AmapPoiSearchResponse>(content, new JsonSerializerSettings(){
                    Error = (obj, args) => {
                        errors.Add(args.ErrorContext.Error.Message);
                        args.ErrorContext.Handled = true;
                    }
                });

                if (responseObj.Pois != null && responseObj.Pois.Count > 0)
                {
                    var poi = responseObj.Pois[searchPoiIndex];
                    d.POIId = poi.Id;
                    d.POICity = poi.CityName?.ToString() ?? string.Empty;
                    d.POICounty = poi.AdName?.ToString() ?? string.Empty;
                    d.POIName = poi.Name;
                    d.POIAddress = poi.Address?.ToString() ?? string.Empty;
                    d.POIBizDistrict = poi.BizDistrict?.ToString() ?? string.Empty;
                    d.POIType = poi.TypeCode;
                    d.POIXY = poi.Location;
                    d.Adcode = poi.Adcode?.ToString() ?? string.Empty;

                    Console.WriteLine($"PoiSearch: the office[{d.PropertyName}] is done");
                }
                else Console.WriteLine($"PoiSearch: no data for the office[{d.PropertyName}]");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PoiSearch: the office[{d.PropertyName}] cannot be resolved, Error: {ex.Message}");
            }
        }

        private void AddressDecode(PropertyData d)
        {
            try
            {
                HttpResponseMessage response = Client.GetAsync(string.Format(tmplAddressDecodeApiUrl, apikey, d.City, d.Address)).Result;
                string content = response.Content.ReadAsStringAsync().Result;
                var responseObj = JsonConvert.DeserializeObject<AmapAddressDecodeResponse>(content);
                if (responseObj.GeoCodes != null && responseObj.GeoCodes.Count > 0) 
                {
                    d.LngLat = responseObj.GeoCodes[0].Location;
                    Console.WriteLine($"AddressDecode: the address of the office[{d.PropertyName}] is decoded");
                }
                else Console.WriteLine($"AddressDecode: no decoded location from api for the office[{d.PropertyName}]");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AddressDecode: the address of the office[{d.PropertyName}] cannot be decoded, Error: {ex.Message}");
            }
        }
    }
}