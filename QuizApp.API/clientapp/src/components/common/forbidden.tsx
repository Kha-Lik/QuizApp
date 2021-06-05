import React from 'react';
import {Typography} from "@material-ui/core";

function Forbidden() {
    return (
        <div>
            <Typography variant={"h2"} color={"primary"}>403 Forbidden</Typography>
        </div>
    );
}

export default Forbidden;