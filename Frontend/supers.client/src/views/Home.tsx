import React, { useState } from "react";
import Sidebar from "./components/Sidebar";
import Footer from "./components/Footer";
import useFocus from "../services/hooks/useFocus";
import { useSuperpoderes } from "../services/hooks/useSuperpoderes"; 
import { useSuperHerois } from "../services/hooks/useSuperHerois";   
import type { NovoHeroi } from "../services/types/NovoHeroi";
import { getTodayDateString } from "../utils/dateUtils";
import CurrencyInput from "react-currency-input-field";

const Home: React.FC = () => {
    const { inputRef } = useFocus();
    const { poderes, isLoading: isLoadingPoderes } = useSuperpoderes();
    const { criarHeroi, isLoading: isCreatingHeroi } = useSuperHerois(); 

    const [formData, setFormData] = useState<NovoHeroi>({
        nome: '',
        nomeHeroi: '',
        dataNascimento: getTodayDateString(), 
        altura: '',
        peso: '',
        superPoderes: []
    });

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
                return { ...prevState, superPoderes: poderesAtuais.filter(id => id !== poderId) };
            } else {
                return { ...prevState, superPoderes: [...poderesAtuais, poderId] };
            }
        });
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            const dadosParaEnviar = {
                ...formData,
                altura: Number(formData.altura) || 0,
                peso: Number(formData.peso) || 0,
            };
            await criarHeroi(dadosParaEnviar);
            setFormData({
                nome: '', nomeHeroi: '', dataNascimento: getTodayDateString(), altura: '', peso: '', superPoderes: []
            });
        } catch (error) {
            console.error("Falha ao criar herói:", error);
        }
    };

    const handleValueChange = (value: string | undefined, name: string | undefined) => {
        if (name) {
            setFormData(prevState => ({
                ...prevState,
                [name]: value || '',
            }));
        }
    };

    return (
        <div className="flex min-h-screen">
            <Sidebar />
            <main className="flex-1 p-14">
                <div className="flex justify-between items-center mb-8">
                    <h2 className="text-xl font-bold px-7">Novo Herói</h2>
                </div>

                <div className="max-w-5xl p-6 bg-white rounded-lg shadow-md">
                    <form className="space-y-6" onSubmit={handleSubmit}>
                        <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                            <div>
                                <label htmlFor="nome" className="block text-sm font-bold mb-2">Nome</label>
                                <input ref={inputRef} type="text" id="nome" name="nome" maxLength={120} value={formData.nome} onChange={handleChange} className="w-full px-3 py-2 bg-gray-100 border-none rounded-md focus:outline-none focus:ring-2 focus:ring-[#DD4B25]" />
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
                                <CurrencyInput
                                    id="altura"
                                    name="altura"
                                    className="w-full px-3 py-2 bg-gray-100 border-none rounded-md focus:outline-none focus:ring-2 focus:ring-[#DD4B25]"
                                    value={formData.altura}
                                    onValueChange={handleValueChange}
                                    allowNegativeValue={false}
                                    decimalScale={2} 
                                    decimalSeparator="."
                                    groupSeparator=","
                                    placeholder="Ex: 185"
                                />
                            </div>
                            <div>
                                <label htmlFor="peso" className="block text-sm font-bold mb-2">Peso (kg)</label>
                                <CurrencyInput
                                    id="peso"
                                    name="peso"
                                    className="w-full px-3 py-2 bg-gray-100 border-none rounded-md focus:outline-none focus:ring-2 focus:ring-[#DD4B25]"
                                    value={formData.peso} 
                                    onValueChange={handleValueChange}
                                    allowNegativeValue={false}
                                    groupSeparator="."
                                    placeholder="Ex: 25"
                                />
                            </div>

                        </div>

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
                            disabled={isCreatingHeroi}
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
