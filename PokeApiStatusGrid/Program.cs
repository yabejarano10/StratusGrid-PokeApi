// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using PokeApiStatusGrid;
using System.Diagnostics;


var URL = "https://pokeapi.co/api/v2/pokemon/?";
Console.WriteLine("Set the Limit:");
var limit = Console.ReadLine();
URL += $"limit={limit}";
Console.WriteLine("Set the Offset:");
var offset = Console.ReadLine();
URL += $"&offset={offset}";

using(var client = new HttpClient())
{
    var totalWeight = 0;
    var totalHeight = 0;
    Stopwatch sw = new Stopwatch();
    sw.Start();
    var response = client.GetStringAsync(URL).Result;
    var pokemons = JsonConvert.DeserializeObject<PokeList>(response);
    Dictionary<string, TypeStats> typeInfo = new Dictionary<string, TypeStats>();

    foreach (UrlsList url in pokemons.results)
    {
        var pokemonStats = client.GetStringAsync(url.url).Result;
        var stats = JsonConvert.DeserializeObject<Pokemon>(pokemonStats);
        totalWeight += stats.weight;
        totalHeight += stats.height;
        foreach (TypeList type in stats.types)
        {
            if (typeInfo.ContainsKey(type.type.name))
            {
                typeInfo[type.type.name].weight += stats.weight;
                typeInfo[type.type.name].height += stats.height;
                typeInfo[type.type.name].pokemonCount++;
            }
            else
            {
                typeInfo.Add(type.type.name, new TypeStats { pokemonCount = 1,height = stats.height, weight = stats.weight });
            }
        }
    }
    sw.Stop();
    TimeSpan timeToLoad = sw.Elapsed;
    Console.WriteLine("Average Weight: " + totalWeight/int.Parse(limit));
    Console.WriteLine("Average Height: " + totalHeight / int.Parse(limit));
    Console.WriteLine("Execution Time: {0:%h} Hours\\{0:%m} Minutes\\{0:%s} Seconds\\{0:%fff} Milliseconds ", timeToLoad);
    Console.WriteLine("-----------------------------------------------------------------------------------------");

    foreach(var element in typeInfo)
    {
        Console.WriteLine($"Average Data for {element.Key.ToUpper()} type: Weight: {element.Value.weight/ element.Value.pokemonCount} Height: {element.Value.height/ element.Value.pokemonCount}" );
    }

    Console.WriteLine();
}

