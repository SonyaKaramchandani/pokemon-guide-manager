import React, { useEffect, useState } from "react";
import PokemonList from "./PokemonList";
import { Input } from "semantic-ui-react";
import { Pokemon } from "../types";
import PokeApi from "../api/pokeApi";
import "../App.css";

const PokemonSearch = () => {
    const [searchQuery, setSearchQuery] = useState("");

    const [savedPokemon, setSavedPokemon] = useState<Pokemon[]>([]);
    useEffect(() => {
        PokeApi.getPokemonByType(Math.floor(Math.random() * 100 + 1)).then((r) => {
            setSavedPokemon([...savedPokemon, r]);
            // todo: how to load the full data from local storage (only takes strings)
            localStorage.setItem("name", r.name);
        });
    }, []);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setSearchQuery(e.target.value);
    };

    const filteredPokemon = savedPokemon.filter((pokemon) => pokemon.name.toLowerCase().includes(searchQuery.toLowerCase()));

    // todo: add x button to clear search results
    return (
        <section>
            <div className="SearchContainer">
                <Input icon="search" iconPosition="left" placeholder="Search Pokemon" onChange={(e) => handleChange(e)} value={searchQuery} />
                {`${filteredPokemon.length} of ${savedPokemon.length} Pokemon`}
            </div>
            <PokemonList filteredPokemon={filteredPokemon} />
        </section>
    );
};

export default PokemonSearch;
