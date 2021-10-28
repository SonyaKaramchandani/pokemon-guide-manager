import React, { useState } from "react";
import PokemonList from "./PokemonList";
import { Input } from "semantic-ui-react";
import { Pokemon } from "../types";
import "../App.css";

export interface ProcessedPokemon extends Pokemon {
  isSaved: boolean;
}

interface PokemonSearchProps {
  savedPokemon: Pokemon[];
  foundPokemon: Pokemon[];
  handleRemove: (pokemon: ProcessedPokemon) => void;
}

const PokemonSearch: React.FC<PokemonSearchProps> = ({ foundPokemon, savedPokemon, handleRemove }) => {
  const processedSavedPokemon = savedPokemon.map((a) => {
    return { ...a, isSaved: true };
  });
  const processedFoundPokemon = foundPokemon.map((a) => {
    return { ...a, isSaved: false };
  });

  const allPokemon: ProcessedPokemon[] = [...(processedFoundPokemon.length ? processedFoundPokemon : processedSavedPokemon)];

  const [filterQuery, setFilterQuery] = useState("");

  const filteredPokemon = allPokemon.filter((pokemon) => pokemon.name.toLowerCase().includes(filterQuery.toLowerCase()));

  const handleChange = (value: string) => {
    setFilterQuery(value);
  };

  return (
    <section>
      <div className="SearchContainer">
        <Input icon="search" iconPosition="left" placeholder="Search Pokemon" onChange={(e) => handleChange(e.target.value)} value={filterQuery} />
        {`${filteredPokemon.length} of ${allPokemon.length} Pokemon`}
      </div>
      <PokemonList filteredPokemon={filteredPokemon} handleRemove={handleRemove} />
    </section>
  );
};

export default PokemonSearch;
