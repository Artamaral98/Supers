import React from "react";
import { Link } from "react-router-dom";

const Sidebar: React.FC = () => {
    return (
        <div className="w-[300px] bg-[#DD4B25] text-white min-h-screen">
            <div className="p-12">
                <Link to='/' className="text-4xl font-bold">Gastos</Link>
                <p className="text-xs px-2">Supers</p>
            </div>

            <div className="p-4 hover:bg-[#F76A2C] transition-colors">
                <Link to='/' className="px-6">Cadastrar Super-herói</Link>
            </div>
        </div>
    )
}

export default Sidebar;
