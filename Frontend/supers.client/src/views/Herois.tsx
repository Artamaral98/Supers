import { useState } from 'react';
import { useSuperHerois } from '../services/hooks/useSuperHerois';
import type { SuperHeroi } from '../services/types/SuperHerois';
import Sidebar from '../views/components/Sidebar';
import Footer from '../views/components/Footer';
import TabelaHerois from '../views/components/TabelaHerois';
import ModalConfirmarExclusao from '../views/components/Modals/ModalConfirmarExclusao';
import ModalAtualizarHeroi from '../views/components/Modals/ModalAtualizarHeroi';
import ModalVerPoderes from '../views/components/Modals/ModalVerPoderes';

const Herois: React.FC = () => {
    const { herois, isLoading, excluirHeroi, atualizarHeroi } = useSuperHerois();

    const [isDeleteModalOpen, setDeleteModalOpen] = useState(false);
    const [isUpdateModalOpen, setUpdateModalOpen] = useState(false);
    const [isViewPowersModalOpen, setViewPowersModalOpen] = useState(false);
    const [selectedHero, setSelectedHero] = useState<SuperHeroi | null>(null);

    const handleOpenDeleteModal = (heroi: SuperHeroi) => {
        setSelectedHero(heroi);
        setDeleteModalOpen(true);
    };

    const handleOpenUpdateModal = (heroi: SuperHeroi) => {
        setSelectedHero(heroi);
        setUpdateModalOpen(true);
    };

    const handleOpenViewPowersModal = (heroi: SuperHeroi) => {
        setSelectedHero(heroi);
        setViewPowersModalOpen(true);
    };

    const handleCloseModals = () => {
        setSelectedHero(null);
        setDeleteModalOpen(false);
        setUpdateModalOpen(false);
        setViewPowersModalOpen(false);
    };

    const handleConfirmDelete = async () => {
        if (selectedHero) {
            await excluirHeroi(selectedHero.id);
            handleCloseModals();
        }
    };

    return (
        <div className="flex min-h-screen bg-gray-50">
            <Sidebar />
            
            <main className="flex-1 p-8 md:p-12 flex flex-col">
                <h2 className="text-2xl font-bold text-gray-800 mb-8">Lista de Heróis</h2>

                <div className="flex-grow">
                    {isLoading && <p className="text-center text-gray-500">Carregando heróis...</p>}

                    {!isLoading && herois.length > 0 && (
                        <TabelaHerois
                            herois={herois}
                            onEdit={handleOpenUpdateModal}
                            onDelete={handleOpenDeleteModal}
                            onViewPowers={handleOpenViewPowersModal}
                        />
                    )}

                    {!isLoading && herois.length === 0 && (
                        <div className="text-center py-10 px-6 bg-white rounded-lg shadow">
                            <h3 className="text-lg font-medium text-gray-700">Nenhum herói cadastrado</h3>
                            <p className="mt-1 text-sm text-gray-500">Cadastre o primeiro herói na página inicial.</p>
                        </div>
                    )}
                </div>

                <Footer />
            </main>

            {selectedHero && (
                <ModalConfirmarExclusao
                    heroi={selectedHero}
                    isOpen={isDeleteModalOpen}
                    onClose={handleCloseModals}
                    onConfirm={handleConfirmDelete}
                    isLoading={isLoading}
                />
            )}

            {selectedHero && (
                <ModalAtualizarHeroi
                    heroi={selectedHero}
                    isOpen={isUpdateModalOpen}
                    onClose={handleCloseModals}
                    onUpdate={atualizarHeroi}
                    isLoading={isLoading}
                />
            )}

            {selectedHero && (
                <ModalVerPoderes
                    heroi={selectedHero}
                    isOpen={isViewPowersModalOpen}
                    onClose={handleCloseModals}
                />
            )}
        </div>
    );
};

export default Herois;