namespace Map;

public interface IDistanceCalculator
{
    public double CityDistance(string city1, string city2);
    public List<string> ClosestCities(string city, int num);
    public double TotalDistance(string[] cities);
}