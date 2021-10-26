import React from "react";
import { Button, Divider, Input } from "semantic-ui-react";
import "../App.css";

const PokemonAdder = () => {
    const handleClick = () => {
        // todo: call api to add pokemon by type
        console.log("Add Pokemon");
    };
    return (
        <section>
            <div className="SearchContainer">
                {/* todo: i think this should repeatedly search for types */}
                {/* loop through all pokemon urls and add them to pokemon list */}
                <Input className="FilterInput" placeholder="Query for “type” required e.g. (fire, water)" />
                <Button content="Add Pokemon" primary icon="plus" onClick={handleClick} />
            </div>
            <Divider />
        </section>
    );
};

export default PokemonAdder;
