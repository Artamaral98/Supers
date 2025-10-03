import React, { useState } from "react";
import Sidebar from "./components/Sidebar";
import Footer from "./components/Footer";
import useFocus from "../services/hooks/useFocus";
import { useSuperpoderes } from "../services/hooks/useSuperpoderes"; // Hook para buscar a lista de poderes
import { useSuperHerois } from "../services/hooks/useSuperHerois";   // Hook com a lógica do CRUD
import type { NovoHeroi } from "../services/types/NovoHeroi";

const Home: React.FC = () => {
    const { inputRef } = useFocus();

    // --- LÓGICA DO COMPONENTE ---

    // 1. Buscando os dados necessários
    const { poderes, isLoading: isLoadingPoderes } = useSuperpoderes(); // Busca a lista de poderes para os checkboxes
    const { criarHeroi, isLoading: isCreatingHeroi } = useSuperHerois(); // Pega a função de criar e o estado de loading do CRUD

    // 2. Estado para o formulário
    const [formData, setFormData] = useState<NovoHeroi>({
        nome: '',
        nomeHeroi: '',
        dataNascimento: '',
        altura: 0,
        peso: 0,
        superPoderes: [] // Armazena os IDs dos poderes selecionados
    });

    // 3. Funções para lidar com as mudanças nos inputs
    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setFormData(prevState => ({
            ...prevState,
            [name]: value
        }));
    };

    const handleCheckboxChange = (poderId: number) => {
        setFormData(prevState => {
            const poderesAtuais = prevState.superPoderes;
            if (poderesAtuais.includes(poderId)) {
                // Se já estiver selecionado, remove
                return { ...prevState, superPoderes: poderesAtuais.filter(id => id !== poderId) };
            } else {
                // Se não estiver, adiciona
                return { ...prevState, superPoderes: [...poderesAtuais, poderId] };
            }
        });
    };

    // 4. Função para lidar com o envio do formulário
    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            // Converte altura e peso para número antes de enviar
            const dadosParaEnviar = {
                ...formData,
                altura: Number(formData.altura) || 0,
                peso: Number(formData.peso) || 0,
            };
            await criarHeroi(dadosParaEnviar);
            // Limpa o formulário após o sucesso
            setFormData({
                nome: '', nomeHeroi: '', dataNascimento: '', altura: 0, peso: 0, superPoderes: []
            });
        } catch (error) {
            console.error("Falha ao criar herói:", error);
        }
    };

    // --- ESTRUTURA JSX (O SEU HTML) ---
    return (
        <div className="flex min-h-screen">
            <Sidebar />
            <main className="flex-1 p-14">
                <div className="flex justify-between items-center mb-8">
                    <h2 className="text-xl font-bold px-7">Novo Herói</h2>
                </div>

                <div className="max-w-5xl p-6 bg-white rounded-lg shadow-md">
                    <form className="space-y-6" onSubmit={handleSubmit}>
                        {/* Seus inputs de texto, agora conectados ao estado */}
                        <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                            <div>
                                <label htmlFor="nome" className="block text-sm font-bold mb-2">Nome</label>
                                <input ref={inputRef} type="text" id="nome" name="nome" value={formData.nome} onChange={handleChange} className="w-full px-3 py-2 bg-gray-100 border-none rounded-md focus:outline-none focus:ring-2 focus:ring-[#DD4B25]" />
                            </div>
                            <div>
                                <label htmlFor="nomeHeroi" className="block text-sm font-bold mb-2">Nome de Herói</label>
                                <input type="text" id="nomeHeroi" name="nomeHeroi" value={formData.nomeHeroi} onChange={handleChange} className="w-full px-3 py-2 bg-gray-100 border-none rounded-md focus:outline-none focus:ring-2 focus:ring-[#DD4B25]" />
                            </div>
                            <div>
                                <label htmlFor="dataNascimento" className="block text-sm font-bold mb-2">Data de Nascimento</label>
                                <input type="date" id="dataNascimento" name="dataNascimento" value={formData.dataNascimento} onChange={handleChange} className="w-full px-3 py-2 bg-gray-100 border-none rounded-md focus:outline-none focus:ring-2 focus:ring-[#DD4B25]" />
                            </div>
                            <div>
                                <label htmlFor="altura" className="block text-sm font-bold mb-2">Altura (cm)</label>
                                <input type="number" step="0.01" id="altura" name="altura" value={formData.altura} onChange={handleChange} className="w-full px-3 py-2 bg-gray-100 border-none rounded-md focus:outline-none focus:ring-2 focus:ring-[#DD4B25]" />
                            </div>
                             <div>
                                <label htmlFor="peso" className="block text-sm font-bold mb-2">Peso (kg)</label>
                                <input type="number" step="0.1" id="peso" name="peso" value={formData.peso} onChange={handleChange} className="w-full px-3 py-2 bg-gray-100 border-none rounded-md focus:outline-none focus:ring-2 focus:ring-[#DD4B25]" />
                            </div>
                        </div>

                        {/* Seção de Superpoderes dinâmica */}
                        <div>
                            <h3 className="text-lg font-bold mb-2">Superpoderes</h3>
                            {isLoadingPoderes ? <p>Carregando poderes...</p> : (
                                <div className="grid grid-cols-2 md:grid-cols-3 gap-4">
                                    {poderes.map(poder => (
                                        <div key={poder.id} className="flex items-center">
                                            <input
                                                type="checkbox"
                                                id={`poder-${poder.id}`}
                                                checked={formData.superPoderes.includes(poder.id)}
                                                onChange={() => handleCheckboxChange(poder.id)}
                                                className="h-4 w-4 rounded border-gray-300 text-[#DD4B25] focus:ring-[#C64422]"
                                            />
                                            <label htmlFor={`poder-${poder.id}`} className="ml-2 block text-sm text-gray-900">
                                                {poder.nome}
                                            </label>
                                        </div>
                                    ))}
                                </div>
                            )}
                        </div>

                        <button
                            type="submit"
                            disabled={isCreatingHeroi} // Desabilita o botão durante o envio
                            className="px-14 py-3 bg-[#DD4B25] text-white rounded-md hover:bg-[#C64422] transition-colors disabled:bg-gray-400"
                        >
                            {isCreatingHeroi ? 'Salvando...' : 'Salvar'}
                        </button>
                    </form>
                </div>
                <Footer />
            </main>
        </div>
    );
};

export default Home;