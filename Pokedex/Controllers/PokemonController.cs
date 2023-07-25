using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Linq;

namespace Pokedex.Controllers
{
    public class PokemonController : Controller
    {
        public IActionResult GetAll(string Nombre, string tipo)
        {
            if (Nombre == null && tipo == null)
            {     
                ML.Result resultPokemons = new ML.Result();
                resultPokemons.Objects = new List<object>();
             
                using (var client = new HttpClient())
                {
                    string Url = "https://pokeapi.co/api/v2/pokemon/?offset=0&limit=20";
                    client.BaseAddress = new Uri(Url);
                    var responseTask = client.GetAsync("");
                    responseTask.Wait();
                    var resultAPI = responseTask.Result;
                    if (resultAPI.IsSuccessStatusCode)
                    {
                        var readTask = resultAPI.Content.ReadAsAsync<dynamic>();
                        readTask.Wait();

                        ML.Pokemon pokemonItemList = new ML.Pokemon();
                        pokemonItemList.Pokemons = new List<object>();
                        
                        foreach (dynamic item in readTask.Result.results)
                        {
                            string Nombre1 = item.name;

                            using (var cliente = new HttpClient())
                            {
                                cliente.BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon/");
                                var responsetask = cliente.GetAsync(Nombre1);
                                responsetask.Wait();
                                var resultapi = responsetask.Result;
                                if (resultapi.IsSuccessStatusCode)
                                {
                                    var readtask = resultapi.Content.ReadAsAsync<dynamic>();
                                    readtask.Wait();
                                    ML.Pokemon pokemonItem = new ML.Pokemon();
                                    pokemonItem.Name = readtask.Result.name;
                                    pokemonItem.Id = readtask.Result.id;
                                    pokemonItem.Sprites = new ML.Sprites();
                                    pokemonItem.Sprites.front_default = readtask.Result.sprites.other["official-artwork"].front_default;
                                    pokemonItemList.next = readTask.Result.next;
                                    pokemonItemList.previous = readTask.Result.previous;
                                    resultPokemons.Objects.Add(pokemonItemList);
                                }
                                else
                                {

                                }
                               
                            }
                            
                        }
                    }
                }  
                ML.Pokemon pokemon = new ML.Pokemon();
                pokemon.Pokemons = resultPokemons.Objects;
                return View(pokemon);
            }
            else
            {
                if(Nombre != null)
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
                        pokemonItemList.Sprites.front_default = readTask.Result.sprites.other["official-artwork"].front_default;
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
                else
                {
                    ML.Result resultPokemons = new ML.Result();
                    resultPokemons.Objects = new List<object>();

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://pokeapi.co/api/v2/type/");
                        var responseTask = client.GetAsync(tipo);
                        responseTask.Wait();
                        var resultAPI = responseTask.Result;
                        if (resultAPI.IsSuccessStatusCode)
                        {
                            var readTask = resultAPI.Content.ReadAsAsync<dynamic>();
                            readTask.Wait();
                            foreach (dynamic item in readTask.Result.pokemon)
                            {
                                string Nombre1 = item.pokemon.name;

                                using (var cliente = new HttpClient())
                                {
                                    cliente.BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon/");
                                    var responsetask = cliente.GetAsync(Nombre1);
                                    responsetask.Wait();
                                    var resultapi = responsetask.Result;
                                    if (resultapi.IsSuccessStatusCode)
                                    {
                                        var readtask = resultapi.Content.ReadAsAsync<dynamic>();
                                        readtask.Wait();

                                        ML.Pokemon pokemonItemList = new ML.Pokemon();

                                        pokemonItemList.Name = readtask.Result.name;
                                        pokemonItemList.Id = readtask.Result.id;
                                        pokemonItemList.Sprites = new ML.Sprites();
                                        pokemonItemList.Sprites.front_default = readtask.Result.sprites.other["official-artwork"].front_default;
                                        resultPokemons.Objects.Add(pokemonItemList);



                                    }
                                    else
                                    {

                                    }
                                }
                              
                            }

                        }
                        ML.Pokemon pokemon = new ML.Pokemon();
                        pokemon.Pokemons = resultPokemons.Objects;
                        return View(pokemon);
                    } 

                }
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
                    pokemonItemList.Sprites.front_default = readTask.Result.sprites.other["official-artwork"].front_default;
                    pokemonItemList.Sprites.front_shiny = readTask.Result.sprites.other["official-artwork"].front_shiny;
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