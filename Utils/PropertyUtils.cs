using System;
using NetTopologySuite.Geometries;
using blazor_demo.Data;
using blazor_demo.MapApiHandler;

namespace blazor_demo.Utils
{
    public static class PropertyUtils
    {
        public static void PropertyApiHandle(this PropertyData d, IMapApiHandler handler)
        {
            handler.Process(d);
            d.CalculateDistance();
        }

        public static void CalculateDistance(this PropertyData d)
        {
            try
            {
                if(!string.IsNullOrEmpty(d.LngLat) && !string.IsNullOrEmpty(d.POIXY))
                {
                    Point p1 = GeoUtils.CreatePoint(d.LngLat);
                    Point p2 = GeoUtils.CreatePoint(d.POIXY);

                    d.Distance = (p1.Distance(p2) * 1000).ToString();
                }
                else d.Distance = string.Empty;
            }
            catch(Exception ex)
            {
                d.Distance = string.Empty;
                Console.WriteLine($"Error ({d.PropertyName}): {ex.Message}");
            }
            
        }
    }
}