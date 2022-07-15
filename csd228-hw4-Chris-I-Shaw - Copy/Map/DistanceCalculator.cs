namespace Map;

public class DistanceCalculator : IDistanceCalculator
{
    public Dictionary<string, Tuple<double, double>> CitiesCoord { get; init; }

    public DistanceCalculator(string filePath = "Resources/wa_cities.csv")
    {
        CitiesCoord = new Dictionary<string, Tuple<double, double>>();
        String[] fileContents = ReadFile(filePath);

        for (int i = 1; i < fileContents.Length; i++)
        {
            string[] noCommas = fileContents[i].Split(',');
            string cityName = noCommas[0];
            double lat = Convert.ToDouble(noCommas[1]);
            double lon = Convert.ToDouble(noCommas[2]);
            var coord = new Tuple<double, double>(lat, lon);

            CitiesCoord.Add(cityName, coord);
        }
    }

    private string[] ReadFile(string path)
    {
        return File.ReadAllLines(path);
    }

    private double Distance(double lon1, double lat1, double lon2, double lat2)
    {
        // Earth's radius in miles
        double R = 3963;
        double phi1 = lat1 * Math.PI / 180; // φ, λ in radians
        double phi2 = lat2 * Math.PI / 180;
        double deltaPhi = (lat2 - lat1) * Math.PI / 180;
        double deltaLambda = (lon2 - lon1) * Math.PI / 180;

        double a = Math.Sin(deltaPhi / 2) * Math.Sin(deltaPhi / 2) +
                Math.Cos(phi1) * Math.Cos(phi2) *
                Math.Sin(deltaLambda / 2) * Math.Sin(deltaLambda / 2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return R * c; // in miles
    }

    public double CityDistance(string city1, string city2)
    {
        double city1Lat = 0;
        double city1Lon = 0;
        double city2Lat = 0;
        double city2Lon = 0;

        foreach (KeyValuePair<string, Tuple<double, double>> city in CitiesCoord)
        {
            if (city.Key == city1)
            {
                city1Lat = city.Value.Item1;
                city1Lon = city.Value.Item2;
            }
            if (city.Key == city2)
            {
                city2Lat = city.Value.Item1;
                city2Lon = city.Value.Item2;
            }
        }
        return Distance(city1Lon, city1Lat, city2Lon, city2Lat);
    }

    public List<string> ClosestCities(string city, int num)
    {
        var citiesWithinDistance = new List<string>();
        var cityPQ = new PriorityQueue<string, double>();

        foreach (KeyValuePair<string, Tuple<double, double>> cityInList in CitiesCoord)
        {
            if (city != cityInList.Key)
            {
                double cityDistance = CityDistance(city, cityInList.Key);
                cityPQ.Enqueue(cityInList.Key, cityDistance);
            }
        }
        for (int i = 0; i < num; i++)
            citiesWithinDistance.Add(cityPQ.Dequeue());

        return citiesWithinDistance;
    }

    public double TotalDistance(params string[] cities)
    {
        double totalDistance = 0.0;

        if (cities.Length == 1)
            return 0;

        for (int i = 0, j = 1; i < cities.Length - 1; i++, j++)
            totalDistance += CityDistance(cities[i], cities[j]);

        return totalDistance;
    }
}