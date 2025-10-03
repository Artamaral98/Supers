import { useState, useEffect } from 'react';
import api from '../api/api'; 
import type { SuperPoder } from '../types/SuperPoder';

interface UseSuperpoderesResult {
  poderes: SuperPoder[];
  isLoading: boolean;
  error: Error | null;
}

export function useSuperpoderes(): UseSuperpoderesResult {
  const [poderes, setPoderes] = useState<SuperPoder[]>([]);
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [error, setError] = useState<Error | null>(null);

  useEffect(() => {
    async function fetchPoderes() {
      try {
        setIsLoading(true);
        const response = await api.get<SuperPoder[]>('/superpoder');
        setPoderes(response.data);
      } catch (err) {
        setError(err as Error);
      } finally {
        setIsLoading(false);
      }
    }

    fetchPoderes();
  }, []);

  return { poderes, isLoading, error };
}