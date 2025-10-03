import { useRoutes } from "react-router-dom";
import routes from "../routes/Routes";

const Router = () => {
    return useRoutes(routes);
};

export default Router;
