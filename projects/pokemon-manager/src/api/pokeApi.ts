import axios, { AxiosError } from "axios";
import { Pokemon } from "../types";

const baseUrl = "https://pokeapi.co/api/v2";
const query = {
    pokemon: "pokemon",
};
const getPokemonByType = async (pokemon: number): Promise<Pokemon> => {
    const url = `${baseUrl}/${query.pokemon}/${pokemon}`;
    try {
        const response = await axios.get<Pokemon>(url);
        console.log(response.data);
        return response.data;
    } catch (err) {
        throw err;
    }
};

const PokeApi = {
    getPokemonByType,
};
export default PokeApi;
