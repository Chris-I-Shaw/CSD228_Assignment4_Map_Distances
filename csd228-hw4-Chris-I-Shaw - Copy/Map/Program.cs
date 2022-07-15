namespace Map;

public class Program
{
    public static void Main(string[] args)
    {
        DistanceCalculator dc = new DistanceCalculator();
        Console.WriteLine(dc.CitiesCoord["Arlington"]);
        Console.WriteLine(dc.CityDistance("Arlington", "Olympia"));
        Console.WriteLine(dc.TotalDistance("Arlington", "Redmond", "Renton", "Sammamish"));
        dc.ClosestCities("Seattle", 10).ForEach(Console.WriteLine);
        Console.WriteLine("=====================");
        dc.ClosestCities("Vancouver", 10).ForEach(Console.WriteLine);
    }
}
