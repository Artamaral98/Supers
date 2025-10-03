import type { SuperPoder } from './SuperPoder';

export interface SuperHeroi {
  id: number;
  nome: string;
  nomeHeroi: string;
  dataNascimento: string; 
  altura: number;
  peso: number;
  criadoEm: string;
  superPoderes: string[];
}