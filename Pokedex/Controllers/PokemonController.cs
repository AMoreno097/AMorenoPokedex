using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Linq;

namespace Pokedex.Controllers
{
    public class PokemonController : Controller
    {
        public IActionResult GetAll(string Nombre)
        {
            if( Nombre == null) {
                ML.Result resultPokemons = new ML.Result();
                resultPokemons.Objects = new List<object>();
            for (int i = 1; i <= 40; i++) { 
                string num = i.ToString();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon/");
                    var responseTask = client.GetAsync(num);
                    responseTask.Wait();
                    var resultAPI = responseTask.Result;
                    if (resultAPI.IsSuccessStatusCode)
                    {
                        var readTask = resultAPI.Content.ReadAsAsync<dynamic>();
                        readTask.Wait();

                        ML.Pokemon pokemonItemList = new ML.Pokemon();

                        pokemonItemList.Name = readTask.Result.name;
                        pokemonItemList.Id = readTask.Result.id;
                        pokemonItemList.Sprites = new ML.Sprites();
                        pokemonItemList.Sprites.front_default = readTask.Result.sprites.other.home.front_default;
                        resultPokemons.Objects.Add(pokemonItemList);
                    }
                }  
            }
               ML.Pokemon pokemon = new ML.Pokemon();
                pokemon.Pokemons = resultPokemons.Objects;
                return View(pokemon);
            }
            else
            {
                ML.Result resultPokemons = new ML.Result();
                resultPokemons.Objects = new List<object>();

                    
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon/");
                        var responseTask = client.GetAsync(Nombre);
                        responseTask.Wait();
                        var resultAPI = responseTask.Result;
                        if (resultAPI.IsSuccessStatusCode)
                        {
                        var readTask = resultAPI.Content.ReadAsAsync<dynamic>();
                        readTask.Wait();

                        ML.Pokemon pokemonItemList = new ML.Pokemon();

                        pokemonItemList.Name = readTask.Result.name;
                        pokemonItemList.Id = readTask.Result.id;
                        pokemonItemList.Sprites = new ML.Sprites();
                        pokemonItemList.Sprites.front_default = readTask.Result.sprites.other.home.front_default;
                        resultPokemons.Objects.Add(pokemonItemList);



                        }
                        else
                        {

                        }
                    }

                ML.Pokemon pokemon = new ML.Pokemon();
                pokemon.Pokemons = resultPokemons.Objects;
                return View(pokemon);
            }
            
        }
        public IActionResult VistaDetalle(string Name)
        {
            ML.Result resultPokemons = new ML.Result();
            resultPokemons.Objects = new List<object>();

           
            

            using (var client = new HttpClient())
            {
           
                client.BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon/");
                var responseTask = client.GetAsync(Name);
                responseTask.Wait();
                var resultAPI = responseTask.Result;
                if (resultAPI.IsSuccessStatusCode)
                {
                    var readTask = resultAPI.Content.ReadAsAsync<dynamic>();
                    readTask.Wait();

                    ML.Pokemon pokemonItemList = new ML.Pokemon();

                    pokemonItemList.Name = readTask.Result.name;
                    pokemonItemList.Id = readTask.Result.id;
                    pokemonItemList.Sprites = new ML.Sprites();
                    pokemonItemList.Sprites.front_default = readTask.Result.sprites.other.home.front_default;
                    pokemonItemList.stats = new ML.stats();
                    pokemonItemList.stats.Stats = new List<object>();
                    pokemonItemList.types = new ML.types();
                    pokemonItemList.types.Types = new List<object>();
                    foreach (dynamic item in readTask.Result.stats)
                    {
                        ML.stats statsItemList = new ML.stats();

                        statsItemList.base_stat = item.base_stat;
                        statsItemList.name = item.stat.name;
                        pokemonItemList.stats.Stats.Add(statsItemList);
                    }
                    foreach(dynamic item in readTask.Result.types)
                    {
                        ML.types typesItemList = new ML.types();

                        typesItemList.slot = item.slot;
                        typesItemList.name = item.type.name;
                        pokemonItemList.types.Types.Add(typesItemList);
                    }

                    resultPokemons.Objects.Add(pokemonItemList);
                    
                }
                else
                {

                }
            }
           ML.Pokemon pokemon = new ML.Pokemon();
           pokemon.Pokemons = resultPokemons.Objects;
           return PartialView(pokemon);
        }
    }
}