# CSD228-HW4 Distance calculator
## Introduction
In this exercise we are going to write a class called `DistanceCalculator` that provides an easy way to calculate the distance between a set of cities on a planar map. The class, upon instantiation, will read a CSV file that contains the geographical coordinates of all cities in Washington state and provide the APIs to a certain queries.

## The CSV file
A very simple way of storing data in a file is via Comma Separated Values (CSV). This structure is easy to be parsed since the number of values in each row is known beforehand. The classes constructor will receive the path to a file on disk and read the file's contents into a property of type Dictionary called `CitiesCoord`.
    
    Dictionary<string, Tuple<Double, Double>> CitiesCoord {get; init;}

The file `wa_cities.csv` is located in the Resources folder. The default path in the constructor should look for this path to load the file and construct the dictionary. Use the `string.Split` method to split each row by comma.

**Note** that the first line in `wa_cities.csv` includes the column names and you should skip it.

## Methods
There are 3 public methods that are defined in `IDistanceCalculatro` interface which `DistanceCalculator` needs to implement. 

    public double CityDistance(string city1, string city2);
    public List<string> ClosestCities(string city, int k);
    public double TotalDistance(string[] cities);

* `CityDistance` receives the name of 2 cities and returns the pythagorean distance of the 2 cities
* `ClosestCities` given a city finds K closest cities to it and returns the list of the names of those cities
* `TotalDistance` given an array of cities (could be multiple parameters) returns the total distance traveling from the first one to the last one

There is a private method that calculates the distance between 2 geographical coordinates based on the [Haversine formula](https://en.wikipedia.org/wiki/Haversine_formula). You can find the implementation of this formula in various languages online as well.

## Ideas and hints
The most involved method in this class is `ClosestCities`. For this to work efficiently, you need to find a way to calculate the distance from a given city to all others. This would take an O(n) computation effort to do. After that, we need to find the K closest ones. You can either [sort the list](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.sort?view=net-6.0) which is O(n log n) time complexity and return the first K elements, or use the [PriorityQueue](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.priorityqueue-2?view=net-6.0) collection and add the city and distance pair to it on the go which would also take O(n log n) and "dequeue" K items from it.