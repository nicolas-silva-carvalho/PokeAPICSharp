using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PokeAPi.Models;
using RestSharp;

namespace PokeAPi.Repositori;
public class PokemonService
{
    public async Task<List<Pokemon>> GetPokemonsCompleteAsync()
    {
        var client = new RestClient("https://pokeapi.co/api/v2/pokemon?limit=20&offset=0");
        RestRequest request = new RestRequest("", Method.Get);

        var response = await client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            var jsonResponse = response.Content;
            var pokemonApiResponse = JsonConvert.DeserializeObject<PokemonApiResponse>(jsonResponse);
            var pokemons = new List<Pokemon>();
            foreach (var result in pokemonApiResponse.Results)
            {
                var pokemonResponse = await client.ExecuteAsync(new RestRequest(result.Url, Method.Get));
                if (pokemonResponse.IsSuccessful)
                {
                    var pokemonJsonResponse = pokemonResponse.Content;
                    var pokemonDetails = JsonConvert.DeserializeObject<Pokemon>(pokemonJsonResponse);
                    pokemons.Add(pokemonDetails);
                }
            }

            return pokemons;

        }

        else
        {
            throw new Exception($"Erro na solicitação: {response.StatusCode}");
        }

    }

    public async Task<Pokemon> GetPokemonByNameAsync(int id)
    {
        var client = new RestClient($"https://pokeapi.co/api/v2/pokemon/{id}");
        RestRequest request = new RestRequest("", Method.Get);

        var response = await client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            var jsonResponse = response.Content;
            var pokemonApiResponse = JsonConvert.DeserializeObject<PokemonApiResponse>(jsonResponse);
            var pokemonResponse = await client.ExecuteAsync(new RestRequest(pokemonApiResponse.Url, Method.Get));
            
            var pokemonJsonResponse = pokemonResponse.Content;
            var pokemonDetails = JsonConvert.DeserializeObject<Pokemon>(pokemonJsonResponse);
            return pokemonDetails;
        }

        else
        {
            throw new Exception($"Erro na solicitação: {response.StatusCode}");
        }
    }
}

