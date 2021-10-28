import React, { useEffect, useState } from "react";
import { Button, Divider, Input } from "semantic-ui-react";
import "../App.css";
import PokeApi from "../api/pokeApi";
import { Pokemon, Result, TypePokemon } from "../types";
import PokemonSearch, { ProcessedPokemon } from "./PokemonSearch";

const PokemonAdder = () => {
  const [searchQuery, setSearchQuery] = useState("");
  const [pokemonTypes, setPokemonTypes] = useState<Result[]>();
  const [foundPokemon, setFoundPokemon] = useState<TypePokemon[]>([]);
  const [filteredPokemon, setFilteredPokemon] = useState<Pokemon[]>([]);
  const [savedPokemon, setSavedPokemon] = useState<Pokemon[]>([]);

  const localPokemonData = JSON.parse(localStorage.getItem("savedPokemon") as string) as TypePokemon[];

  async function getSavedPokemonFromLocalStorage() {
    const loadedPokemon: Pokemon[] = [];

    if (localPokemonData) {
      const promises = localPokemonData.map((p) => PokeApi.getPokemon(p.pokemon.url));
      await Promise.all(promises).then((r) => loadedPokemon.push(...r));
      setSavedPokemon(loadedPokemon);
    }
  }

  const handleChange = async (value: string) => {
    setSearchQuery(value);

    // check if search query is a valid pokemon type before firing a request
    if (pokemonTypes && pokemonTypes.some((e) => e.name === value)) {
      const newPokemon: Pokemon[] = [];
      await PokeApi.getPokemonByType(value).then((r) => {
        setFoundPokemon(r.pokemon);
        const promises = r.pokemon.map((p) => PokeApi.getPokemon(p.pokemon.url));
        return Promise.all(promises).then((r) => newPokemon.push(...r));
      });
      setFilteredPokemon([...filteredPokemon, ...newPokemon]);
    }
  };

  const handleAdd = () => {
    localStorage.setItem("savedPokemon", JSON.stringify(foundPokemon));
  };

  const handleRemove = (pokemon: ProcessedPokemon) => {
    const newSavedPokemonData = localPokemonData.filter((p) => !(p.pokemon.name === pokemon.species.name));
    const newPokemonData = savedPokemon.filter((p) => !(p.species.name === pokemon.species.name));

    localStorage.setItem("savedPokemon", JSON.stringify(newSavedPokemonData));
    setSavedPokemon(newPokemonData);
  };

  useEffect(() => {
    PokeApi.getAllTypes().then((r) => {
      setPokemonTypes(r.results);
    });
  }, []);

  useEffect(() => {
    getSavedPokemonFromLocalStorage();
  }, []);

  return (
    <section>
      <div className="SearchContainer">
        <Input className="FilterInput" placeholder="Query for “type” required e.g. (fire, water)" onChange={(e) => handleChange(e.target.value)} value={searchQuery} />
        <Button content="Add Pokemon" primary icon="plus" disabled={!searchQuery} onClick={handleAdd} />
      </div>
      <Divider />
      <PokemonSearch foundPokemon={filteredPokemon} savedPokemon={savedPokemon} handleRemove={handleRemove} />
    </section>
  );
};

export default PokemonAdder;
