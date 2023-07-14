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
            ML.Pokemon pokemon = new ML.Pokemon();
            pokemon.Pokemons = new List<object>();
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
                        var readTask = resultAPI.Content.ReadAsAsync<ML.Pokemon>();
                        pokemon.Pokemons.Add(readTask.Result);
                        readTask.Wait();

                    }
                }  
            }
                return View(pokemon);
            }
            else
            {
                ML.Pokemon pokemon = new ML.Pokemon();
                pokemon.Pokemons = new List<object>();

                    
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon/");
                        var responseTask = client.GetAsync(Nombre);
                        responseTask.Wait();
                        var resultAPI = responseTask.Result;
                        if (resultAPI.IsSuccessStatusCode)
                        {
                            var readTask = resultAPI.Content.ReadAsAsync<ML.Pokemon>();
                            readTask.Wait();
                            pokemon.Pokemons.Add(readTask.Result);



                        }
                        else
                        {

                        }
                    }

                return View(pokemon);
            }
            
        }
        public IActionResult VistaDetalle(string Name)
        {
            ML.Pokemon pokemon = new ML.Pokemon();
            pokemon.Pokemons = new List<object>();
            pokemon.stats = new List<object>();
            pokemon.types = new List<object>();
            


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
                    foreach (dynamic resultItem in readTask.Result.drinks)
                    {
                        //ML.Coctel resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Coctel>(resultItem);
                        ML.Coctel resultItemList = new ML.Coctel();
                        resultItemList.strDrink = resultItem.strDrink;
                        resultcoctel.Objects.Add(resultItemList);
                    }

                }
                else
                {

                }
            }
            return PartialView(pokemon);
        }
    }
}