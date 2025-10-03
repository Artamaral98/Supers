import type { NovoHeroi } from '../types/NovoHeroi';
import type { RespostaSucesso } from '../types/RespostaSucesso';
import type { SuperHeroi } from '../types/SuperHerois';
import api from '../api/api';

export const getHerois = async () => {
    const response = await api.get<{ herois: SuperHeroi[] }>('/superheroi/ListarTodos');
    return response.data.herois;
};

export const createHeroi = async (data: NovoHeroi) => {
    const response = await api.post<SuperHeroi>('/superheroi', data);
    return response.data;
};

export const updateHeroi = async (id: number, data: NovoHeroi) => {
    const response = await api.put<RespostaSucesso<SuperHeroi>>(`/superheroi/${id}`, data);
    return response.data; 
};

export const deleteHeroi = async (id: number) => {
    const response = await api.delete<{ mensagem: string }>(`/superheroi/${id}`);
    return response.data;
};