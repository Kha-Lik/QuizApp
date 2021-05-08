import {TextField} from "@material-ui/core";
import React from "react";

export interface InputProps {
    id: string;
    label: string;
    onChange: Function;
}

const RequiredInput = ({id, label, onChange}: InputProps) => {
    return (
        <TextField
            required
            fullWidth
            className="my-1"
            id={id}
            label={label}
            autoComplete="off"
            variant="outlined"
            onChange={(e) => onChange(e)}
        />
    );
};

export default RequiredInput;
