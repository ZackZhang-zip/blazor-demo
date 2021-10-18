using System;
using blazor_demo.Data;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

namespace blazor_demo.Utils
{
    public static class GeoUtils
    {
        private static GeometryFactory geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

        // xy format -> lng,lat
        public static Point CreatePoint(string xy)
        {
            WKTReader reader = new WKTReader(geometryFactory.GeometryServices);
            return reader.Read($"POINT({string.Join(" ", xy.Split(','))})") as Point;
        }
    }
}