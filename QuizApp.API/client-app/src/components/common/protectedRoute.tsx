import React from "react";
import {Redirect, Route, RouteProps} from "react-router-dom";
import auth from "../../services/authService";


const ProtectedRoute = ({path, component: Component, render, role, ...rest}: RouteProps & { role: string }) => {
    return (
        <Route
            {...rest}
            render={props => {
                if (!(auth.getCurrentUser().Role = role))
                    return (
                        <Redirect
                            to={{
                                pathname: "/forbidden",
                                state: {from: props.location}
                            }}
                        />
                    );
                return Component ? <Component {...props} /> : (render as Function)(props);
            }}
        />
    );
};

export default ProtectedRoute;
