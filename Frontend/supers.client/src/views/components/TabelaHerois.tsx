import type { SuperHeroi } from '../../services/types/SuperHerois';

interface TabelaHeroisProps {
    herois: SuperHeroi[];
    onEdit: (heroi: SuperHeroi) => void;
    onDelete: (heroi: SuperHeroi) => void;
    onViewPowers: (heroi: SuperHeroi) => void;
}

const TabelaHerois: React.FC<TabelaHeroisProps> = ({ herois, onEdit, onDelete, onViewPowers  }) => {
    return (
        <div className="overflow-x-auto bg-white rounded-lg shadow max-h-[70vh] overflow-y-auto">
            <table className="min-w-full">
                <thead className="bg-gray-100">
                    <tr>
                        <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Nome</th>
                        <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Nome de Herói</th>
                        <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Superpoderes</th>
                        <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Ações</th>
                    </tr>
                </thead>
                <tbody className="divide-y divide-gray-200">
                    {herois.map(heroi => (
                        <tr key={heroi.id}>
                            <td className="px-6 py-4 whitespace-nowrap">{heroi.nome}</td>
                            <td className="px-6 py-4 whitespace-nowrap">{heroi.nomeHeroi}</td>
                            <td className="px-6 py-4 whitespace-nowrap">
                                <button onClick={() => onViewPowers(heroi)} className="text-sm text-blue-600 hover:underline">
                                    Ver Poderes ({heroi.superPoderes.length})
                                </button>
                            </td>
                            <td className="px-6 py-4 whitespace-nowrap text-sm font-medium">
                                <button onClick={() => onEdit(heroi)} className="text-indigo-600 hover:text-indigo-900 mr-4">Editar</button>
                                <button onClick={() => onDelete(heroi)} className="text-red-600 hover:text-red-900">Excluir</button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default TabelaHerois;