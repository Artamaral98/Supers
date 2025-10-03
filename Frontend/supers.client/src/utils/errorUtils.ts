import axios, { AxiosError } from 'axios';

interface ApiErrorResponse {
  erros: string[];
}

export function getErrorMessage(error: unknown): string[] {
  if (axios.isAxiosError(error)) {
    const axiosError = error as AxiosError<ApiErrorResponse>;
    if (axiosError.response && axiosError.response.data) {
      if (axiosError.response.data.erros && axiosError.response.data.erros.length > 0) {
        return axiosError.response.data.erros;
      }
    }
  }

  if (error instanceof Error) {
    return [error.message];
  }

  return ['Ocorreu um erro inesperado.'];
}