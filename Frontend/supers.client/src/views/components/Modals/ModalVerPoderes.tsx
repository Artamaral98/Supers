import { Dialog, DialogPanel, DialogTitle, Transition } from '@headlessui/react';
import { Fragment } from 'react';
import type { SuperHeroi } from '../../../services/types/SuperHerois'; 

interface ModalProps {
    isOpen: boolean;
    onClose: () => void;
    heroi: SuperHeroi;
}

const ModalVerPoderes: React.FC<ModalProps> = ({ isOpen, onClose, heroi }) => {
    return (
        <Transition appear show={isOpen} as={Fragment}>
            <Dialog as="div" className="relative z-50" onClose={onClose}>
                <div className="fixed inset-0 bg-black/25 backdrop-blur-sm" aria-hidden="true" />

                <div className="fixed inset-0 w-screen overflow-y-auto">
                    <div className="flex min-h-full items-center justify-center p-4">
                        <DialogPanel className="w-full max-w-md transform overflow-hidden rounded-2xl bg-white p-6 text-left align-middle shadow-xl transition-all">
                            <DialogTitle as="h3" className="text-lg font-bold leading-6 text-gray-900">
                                Poderes de {heroi.nomeHeroi}
                            </DialogTitle>
                            <div className="mt-4">
                                {heroi.superPoderes.length > 0 ? (
                                    <ul className="list-disc list-inside space-y-2">
                                        {heroi.superPoderes.map((poder, index) => (
                                            <li key={index} className="text-sm text-gray-600">{poder}</li>
                                        ))}
                                    </ul>
                                ) : (
                                    <p className="text-sm text-gray-500">Este herói não possui superpoderes cadastrados.</p>
                                )}
                            </div>

                            <div className="mt-6">
                                <button
                                    type="button"
                                    className="inline-flex justify-center rounded-md border border-transparent bg-blue-100 px-4 py-2 text-sm font-medium text-blue-900 hover:bg-blue-200 focus:outline-none"
                                    onClick={onClose}
                                >
                                    Fechar
                                </button>
                            </div>
                        </DialogPanel>
                    </div>
                </div>
            </Dialog>
        </Transition>
    );
};

export default ModalVerPoderes;