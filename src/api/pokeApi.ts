import axios from "axios";
import { Pokemon, Type, Types } from "../types";

const baseUrl = "https://pokeapi.co/api/v2";

const getPokemonByType = async (type: string): Promise<Type> => {
  const url = `${baseUrl}/type/${type}`;
  try {
    const response = await axios.get<Type>(url);
    return response.data;
  } catch (err) {
    throw err;
  }
};

const getPokemon = async (url: string): Promise<Pokemon> => {
  try {
    const response = await axios.get<Pokemon>(url);
    return response.data;
  } catch (err) {
    throw err;
  }
};

const getAllTypes = async (): Promise<Types> => {
  const url = `${baseUrl}/type`;
  try {
    const response = await axios.get<Types>(url);
    return response.data;
  } catch (err) {
    throw err;
  }
};

const PokeApi = {
  getPokemonByType,
  getPokemon,
  getAllTypes,
};
export default PokeApi;
