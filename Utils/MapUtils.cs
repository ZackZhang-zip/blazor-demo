using System;
using blazor_demo.Data;

namespace blazor_demo.Utils
{
    public static class MapUtils
    {
        public static void AmapPropertyHandler(this PropertyData d)
        {
            d.POIName = "amap";
            d.POIXY = "121.437392,31.183103";
            d.Distance = 20.ToString();
        }
    }
}