using NGeoHash;

namespace Sulmar.EFCore.Models
{

    

    public class Coordinate : Base
    {

        public Coordinate()
        {

        }

        public Coordinate(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }


        // dotnet add package NGeoHash
        public Coordinate(string geohash)
        {
            var decoded = GeoHash.Decode(geohash);

            Latitude = decoded.Coordinates.Lat;
            Longitude = decoded.Coordinates.Lon;
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string ToGeoHash() => GeoHash.Encode(Latitude, Longitude);


    }
}





