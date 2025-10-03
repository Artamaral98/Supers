import type { RouteObject } from "react-router-dom";
import Home from "../views/Home";
import Herois from "../views/Herois";

const routes: RouteObject[] = [
    {
        path: '/',
        element: <Home />
    },
    {
        path: '/herois',
        element: <Herois />
    },
];

export default routes;
