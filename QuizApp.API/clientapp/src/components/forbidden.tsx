import React from 'react';
import {Paper, Typography} from "@material-ui/core";

export interface ForbiddenProps {
}

function Forbidden(props: ForbiddenProps) {
    return (
        <div>
            <Paper variant={"outlined"} elevation={3} style={{width: "max-content"}}>
                <Typography variant={"h1"} color={"error"}>{"Error 403: you don't have access to this page"}</Typography>
            </Paper>
        </div>
    );
}

export default Forbidden;