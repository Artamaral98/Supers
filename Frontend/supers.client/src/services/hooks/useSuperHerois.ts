import { useState, useEffect, useCallback } from 'react';
import { toast } from 'react-hot-toast';
import { getHerois, createHeroi } from '../superHeroi/SuperHeroiService';
import type { SuperHeroi } from '../types/SuperHerois';
import type { NovoHeroi } from '../types/NovoHeroi';
import { getErrorMessage } from '../../utils/errorUtils';

export function useSuperHerois() {
  const [herois, setHerois] = useState<SuperHeroi[]>([]);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<Error | null>(null);

  const fetchHerois = useCallback(async () => {
    try {
      setIsLoading(true);
      const data = await getHerois();
      setHerois(data);
    } catch (err) {
      setError(err as Error);
      const errorMessages = getErrorMessage(err);
      errorMessages.forEach(message => toast.error(message));
    } finally {
      setIsLoading(false);
    }
  }, []);

  useEffect(() => {
    fetchHerois();
  }, [fetchHerois]);

  const criarHeroi = async (novoHeroi: NovoHeroi) => {
    try {
      setIsLoading(true);
      await createHeroi(novoHeroi);
      toast.success('Herói cadastrado com sucesso!');
      await fetchHerois();
    } catch (err) {
      setError(err as Error);
      const errorMessages = getErrorMessage(err);
      errorMessages.forEach(message => toast.error(message));
      throw err;
    } finally {
      setIsLoading(false);
    }
  };

  return {
    herois,
    isLoading,
    error,
    criarHeroi,
  };
}