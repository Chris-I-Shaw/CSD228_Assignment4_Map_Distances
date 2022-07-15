using System;
using System.Collections.Generic;
using Xunit;
namespace Map.Test;

public class UnitTests
{
    DistanceCalculator dc;
    public UnitTests()
    {
        const string PATH = "../../../../Map/Resources/wa_cities.csv";
        dc = new DistanceCalculator(PATH);

    }
    [Fact]
    public void ConstructionTest()
    {
        Assert.NotEmpty(dc.CitiesCoord);
        Assert.Equal(626, dc.CitiesCoord.Keys.Count);
        Assert.Equal(Tuple.Create<double, double>(48.65, -118.73), dc.CitiesCoord["Republic"]);
        Assert.Equal(Tuple.Create<double, double>(47.38, -122.72), dc.CitiesCoord["Stansberry Lake"]);
    }

    [Fact]
    public void DistanceTest()
    {
        Assert.Equal(33.920535704042486, dc.CityDistance("Arlington", "Redmond"));
        Assert.Equal(215.19726804483227, dc.CityDistance("Vancouver", "Bellingham"));
        Assert.Equal(229.72337164609323, dc.CityDistance("Spokane", "Seattle"));
    }

    [Fact]
    public void TotalDistanceTest()
    {
        Assert.Equal(58.99467938882991, dc.TotalDistance("Arlington", "Redmond", "Renton", "Sammamish"));
        Assert.Equal(321.7453597423663, dc.TotalDistance("Vancouver", "Olympia", "Ridgefield", "Bothell"));
        Assert.Equal(17.118526825856407, dc.TotalDistance("Snohomish", "Kirkland"));
        Assert.Equal(0, dc.TotalDistance("Snohomish"));
    }

    [Fact]
    public void ClosestCitiesTest()
    {
        List<string> cities = dc.ClosestCities("Seattle", 10);
        Assert.NotEmpty(cities);
        Assert.Equal(10, cities.Count);
        string[] expected = {"Medina",
        "Hunts Point town",
        "Clyde Hill",
        "Yarrow Point town",
        "Mercer Island",
        "Beaux Arts Village town",
        "White Center",
        "Boulevard Park",
        "Bainbridge Island",
        "Kirkland"};
        Assert.Equal(expected, cities.ToArray());
        expected = new string[] {"Minnehaha",
        "Walnut Grove",
        "Five Corners",
        "Hazel Dell",
        "Orchards"};
        cities = dc.ClosestCities("Vancouver", 5);
        Assert.Equal(expected, cities);

    }
}