import { Dialog, DialogPanel, DialogTitle, Transition } from '@headlessui/react';
import { Fragment, useState, useEffect } from 'react';
import { useSuperpoderes } from '../../../services/hooks/useSuperpoderes';
import type { SuperHeroi } from '../../../services/types/SuperHerois';
import type { NovoHeroi } from '../../../services/types/NovoHeroi';

interface ModalProps {
    isOpen: boolean;
    onClose: () => void;
    onUpdate: (id: number, data: NovoHeroi) => Promise<void>;
    heroi: SuperHeroi;
    isLoading: boolean;
}

const ModalAtualizarHeroi: React.FC<ModalProps> = ({ isOpen, onClose, onUpdate, heroi, isLoading }) => {
    const { poderes, isLoading: isLoadingPoderes } = useSuperpoderes();
    const [formData, setFormData] = useState<NovoHeroi>({
        nome: '',
        nomeHeroi: '',
        dataNascimento: '', 
        altura: '',
        peso: '',
        superPoderes: []
    });


    useEffect(() => {
        if (heroi) {
            let dataFormatada = ''; 
            
            if (heroi.dataNascimento && !heroi.dataNascimento.startsWith('0001-01-01')) {
                dataFormatada = heroi.dataNascimento.split('T')[0];
            }

            const idsDosPoderesAtuais = poderes.length > 0 ? poderes
                .filter(poder => heroi.superPoderes.includes(poder.nome))
                .map(poder => poder.id) : [];

            setFormData({
                nome: heroi.nome,
                nomeHeroi: heroi.nomeHeroi,
                dataNascimento: dataFormatada,
                altura: heroi.altura,
                peso: heroi.peso,
                superPoderes: idsDosPoderesAtuais
            });
        }
    }, [heroi, poderes, isOpen]); 

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setFormData(prevState => ({ ...prevState, [name]: value }));
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
            await onUpdate(heroi.id, dadosParaEnviar); 
            onClose(); 
        } catch (error) {
            console.error("Falha ao atualizar o herói:", error);
        }
    };

    return (
        <Transition appear show={isOpen} as={Fragment}>
            <Dialog as="div" className="relative z-50" onClose={onClose}>
                <div className="fixed inset-0 bg-black/25 backdrop-blur-sm" aria-hidden="true" />
                <div className="fixed inset-0 w-screen overflow-y-auto">
                    <div className="flex min-h-full items-center justify-center p-4">
                        <DialogPanel as="form" onSubmit={handleSubmit} className="w-full max-w-3xl transform space-y-6 overflow-hidden rounded-2xl bg-white p-6 text-left align-middle shadow-xl transition-all">
                            <DialogTitle as="h3" className="text-lg font-bold leading-6 text-gray-900 mb-4">
                                Editar Herói: {heroi.nomeHeroi}
                            </DialogTitle>
                            
                            <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                                <div><label htmlFor="nome" className="block text-sm font-bold mb-2">Nome</label><input type="text" id="nome" name="nome" value={formData.nome} onChange={handleChange} className="w-full px-3 py-2 bg-gray-100 border-none rounded-md" /></div>
                                 <div><label htmlFor="nomeHeroi" className="block text-sm font-bold mb-2">Nome de Herói</label><input type="text" id="nomeHeroi" name="nomeHeroi" value={formData.nomeHeroi} onChange={handleChange} className="w-full px-3 py-2 bg-gray-100 border-none rounded-md" /></div>
                                        <div><label htmlFor="dataNascimento" className="block text-sm font-bold mb-2">Data de Nascimento</label><input type="date" id="dataNascimento" name="dataNascimento" value={formData.dataNascimento} onChange={handleChange} className="w-full px-3 py-2 bg-gray-100 border-none rounded-md" /></div>
                                        <div><label htmlFor="altura" className="block text-sm font-bold mb-2">Altura (m)</label><input type="number" step="0.01" id="altura" name="altura" value={formData.altura} onChange={handleChange} className="w-full px-3 py-2 bg-gray-100 border-none rounded-md" /></div>
                                        <div><label htmlFor="peso" className="block text-sm font-bold mb-2">Peso (kg)</label><input type="number" step="0.1" id="peso" name="peso" value={formData.peso} onChange={handleChange} className="w-full px-3 py-2 bg-gray-100 border-none rounded-md" /></div>
                            </div>
                             <div>
                                        <h3 className="text-md font-bold mb-2">Superpoderes</h3>
                                        {isLoadingPoderes ? <p>Carregando poderes...</p> : (
                                            <div className="grid grid-cols-2 md:grid-cols-3 gap-4">{poderes.map(poder => (<div key={poder.id} className="flex items-center"><input type="checkbox" id={`update-poder-${poder.id}`} checked={formData.superPoderes.includes(poder.id)} onChange={() => handleCheckboxChange(poder.id)} className="h-4 w-4 rounded" /><label htmlFor={`update-poder-${poder.id}`} className="ml-2 block text-sm">{poder.nome}</label></div>))}</div>
                                        )}
                             </div>

                            <div className="mt-6 flex justify-end gap-4">
                                <button type="button" className="px-4 py-2 text-sm font-medium text-gray-700 bg-gray-100 border rounded-md hover:bg-gray-200" onClick={onClose} disabled={isLoading}>Cancelar</button>
                                <button type="submit" className="px-4 py-2 text-sm font-medium text-white bg-[#DD4B25] border rounded-md hover:bg-[#C64422] disabled:bg-gray-400" disabled={isLoading}>{isLoading ? 'Salvando...' : 'Salvar Alterações'}</button>
                            </div>
                        </DialogPanel>
                    </div>
                </div>
            </Dialog>
        </Transition>
    );
};

export default ModalAtualizarHeroi;