import React, {Fragment} from "react";
import {Subject} from "../appTypes";
import {Button, Divider, Typography} from "@material-ui/core";
import RequiredInput from "./common/requiredInput";

export interface SubjectCreationFormProps {
    onSubmit: Function;
}

export interface SubjectCreationFormState {
}

class SubjectCreationForm extends React.Component<SubjectCreationFormProps,
    SubjectCreationFormState> {
    state = {
        Id: "",
        Name: "",
    };

    handleIdChanged = (event: React.ChangeEvent<HTMLInputElement>) => {
        this.setState({Id: event.target.value});
    };

    handleNameChanged = (event: React.ChangeEvent<HTMLInputElement>) => {
        this.setState({Name: event.target.value});
    };

    doSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const subject: Subject = {
            Id: this.state.Id,
            Name: this.state.Name,
            LecturerId: "",
        };
        console.log(subject);
        this.props.onSubmit && this.props.onSubmit();
    };

    render() {
        return (
            <Fragment>
                <Typography className="m-2" variant="h5">
                    Новий предмет
                </Typography>
                <Divider/>
                <form noValidate autoComplete="off" onSubmit={this.doSubmit}>
                    <div className="m-2">
                        <RequiredInput
                            id="Id"
                            label="Subject ID"
                            onChange={this.handleIdChanged}
                        />
                        <RequiredInput
                            id="Name"
                            label="Name"
                            onChange={this.handleNameChanged}
                        />
                        <Button
                            className="my-1"
                            type="submit"
                            fullWidth
                            variant="contained"
                            color="primary"
                        >
                            Створити
                        </Button>
                    </div>
                </form>
            </Fragment>
        );
    }
}

export default SubjectCreationForm;
