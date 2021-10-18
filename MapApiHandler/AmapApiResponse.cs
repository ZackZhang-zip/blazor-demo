using System.Collections.Generic;
using Newtonsoft.Json;

namespace blazor_demo.MapApiHandler
{
    class AmapPoiSearchResponse
    {
        public int Infocode { get; set; }
        public int Count { get; set; }
        public List<AmapPoiSearch> Pois { get; set; }
    }
 
    class AmapPoiSearch
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public object Address { get; set; }
        public string Location { get; set; }
        public string TypeCode { get; set; }
        [JsonProperty("business_area")]
        public object BizDistrict { get; set; }
        public object CityName { get; set; }
        public object AdName { get; set; }
        public object Adcode { get; set; }
    }

    class AmapAddressDecodeResponse
    {
        public int Infocode { get; set; }
        public List<AmapGeoCode> GeoCodes { get; set; }
    }
 
    class AmapGeoCode
    {
        public string Name { get; set; }
        public string Adcode { get; set; }
        public string Location { get; set; }
    }
}