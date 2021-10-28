import React from "react";
import "./App.css";
import PokemonAdder from "./components/PokemonAdder";

// DONE ● A user should be able to type in a query (e.g. “water”), the app should fetch “water” type
// Pokemon and display them in the table.
// DONE ● A user should be able to click a button “Add Pokemon” to persist their search results in
// their browser’s localStorage.When the page is refreshed, the saved Pokemon
// should appear (replacing any search results that might be showing).
// DONE ● The user should be able to use a search bar to filter their search results or persisted data
// by the Pokemon’s name.
// DONE ● A share icon for each Pokemon should be included. When clicked, it should open a new
// tab to the Pokemon’s unique page (e.g. “https://pokeapi.co/api/v2/pokemon/ditto”).
// ● Once saved, Pokemon rows should also be able to be deleted from the
// localStorage.
function App() {
    return (
        <div className="App">
            <div className="Container">
                <PokemonAdder />
            </div>
        </div>
    );
}

export default App;
