import React from "react";
import PokemonList from "./components/PokemonList";
import "./App.css";
import PokemonSearch from "./components/PokemonSearch";
import PokemonAdder from "./components/PokemonAdder";

// components:
// Pokemon List
// Pokemon Search
// Pokemon Adder
function App() {
    return (
        <div className="App">
            <div className="Container">
                <PokemonAdder />
                <PokemonSearch />
            </div>
        </div>
    );
}

export default App;
