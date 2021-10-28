import React from "react";
import { Icon, Table } from "semantic-ui-react";
import { titleCase } from "../common/helpers";
import { ProcessedPokemon } from "./PokemonSearch";

interface PokemonListProps {
  filteredPokemon: ProcessedPokemon[];
  handleRemove: (pokemon: ProcessedPokemon) => void;
}

const PokemonList: React.FC<PokemonListProps> = ({ filteredPokemon, handleRemove }) => {
  return (
    <Table>
      <Table.Header>
        <Table.Row>
          <Table.HeaderCell>Sprite</Table.HeaderCell>
          <Table.HeaderCell>Name</Table.HeaderCell>
          <Table.HeaderCell>Type(s)</Table.HeaderCell>
          <Table.HeaderCell>Number of Moves</Table.HeaderCell>
          <Table.HeaderCell />
        </Table.Row>
      </Table.Header>
      <Table.Body>
        {filteredPokemon.map((pokemon, index) => (
          <Table.Row key={index}>
            <Table.Cell>
              <img src={pokemon.sprites.front_default} alt={pokemon.name} />
            </Table.Cell>
            <Table.Cell>
              <>
                <a href={pokemon.species.url} target="_blank" rel="noopener noreferrer">
                  <Icon name="share square outline" color="blue" />
                </a>
                {titleCase(pokemon.name)}
              </>
            </Table.Cell>
            <Table.Cell>{pokemon.types.map((type) => type.type.name).join(" > ")}</Table.Cell>
            <Table.Cell>{pokemon.moves.length}</Table.Cell>
            <Table.Cell>{pokemon.isSaved && <Icon name="x" color="red" link onClick={() => handleRemove(pokemon)} />}</Table.Cell>
          </Table.Row>
        ))}
      </Table.Body>
    </Table>
  );
};

export default PokemonList;
