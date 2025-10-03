import type { NovoHeroi } from '../types/NovoHeroi';
import type { RespostaSucesso } from '../types/RespostaSucesso';
import type { SuperHeroi } from '../types/SuperHerois';
import api from '../api/api';

// --- Retorna a lista diretamente ---
export const getHerois = async () => {
    const response = await api.get<SuperHeroi[]>('/superheroi/ListarTodos');
    return response.data;
};

// --- Retorna o novo herói diretamente ---
export const createHeroi = async (data: NovoHeroi) => {
    const response = await api.post<SuperHeroi>('/superheroi', data);
    return response.data;
};

// --- USA O "ENVELOPE" RespostaSucesso ---
export const updateHeroi = async (id: number, data: NovoHeroi) => {
    // A API retorna o envelope com a mensagem e os dados atualizados
    const response = await api.put<RespostaSucesso<SuperHeroi>>(`/superheroi/${id}`, data);
    return response.data; 
};

// --- Retorna um objeto simples de mensagem ---
export const deleteHeroi = async (id: number) => {
    // A API retorna um objeto { mensagem: "..." }
    const response = await api.delete<{ mensagem: string }>(`/superheroi/${id}`);
    return response.data;
};