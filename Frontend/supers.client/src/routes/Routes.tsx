import type { RouteObject } from "react-router-dom";
import Home from "../views/Home";

const routes: RouteObject[] = [
    {
        path: '/',
        element: <Home />
    },
    // {
    //     path: '/usuarios',
    //     element: <Users />
    // },
    // {
    //     path: '/nova-transacao',
    //     element: <NewTransaction />
    // },
    // {
    //     path: '/transacoes',
    //     element: <Transactions />
    // },
    // {
    //     path: '/consulta-de-totais',
    //     element: <Total />
    // },
    // {
    //     path: '*',
    //     element: <NotFound />
    // }

];

export default routes;
