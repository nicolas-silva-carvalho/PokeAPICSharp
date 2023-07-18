
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PokeAPi.Models;
using PokeAPi.Repositori;
using RestSharp;

namespace PokeAPi.Controllers;

public class HomeController : Controller
{
    private readonly PokemonService _pokemonService;
    public HomeController(PokemonService pokemonService)
    {
        _pokemonService = pokemonService;
    }

    public async Task<IActionResult> Index()
    {
        var pokemons = await _pokemonService.GetPokemonsCompleteAsync();
        return View(pokemons);
    }

    public async Task<IActionResult> SelectPokemon(int id)
    {
        var pokemon = await _pokemonService.GetPokemonByNameAsync(id);
        return View(pokemon);
    }
}
