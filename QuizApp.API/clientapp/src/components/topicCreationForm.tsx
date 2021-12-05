import React, {Fragment} from "react";
import {Topic} from "../appTypes";
import {Button, Divider, Grid, Typography} from "@material-ui/core";
import RequiredInput from "./common/requiredInput";

export interface TopicCreationFormProps {
    subjectId: string;
    onSubmit?: Function;
}

export interface TopicCreationFormState {
    topic: Topic;
}

class TopicCreationForm extends React.Component<TopicCreationFormProps,
    TopicCreationFormState> {
    state: TopicCreationFormState;

    constructor(props: TopicCreationFormProps) {
        super(props);
        this.state = {
            topic: {
                Id: "string",
                SubjectId: "string",
                Name: "string",
                TopicNumber: 0,
                TimeToPass: 0,
                QuestionsPerAttempt: 0,
                MaxAttemptCount: 0,
            },
        };
    }

    handleNameChanged = (event: React.ChangeEvent<HTMLInputElement>) => {
        const topic: Topic = {...this.state.topic};
        topic.Name = event.target.value;
        this.setState({topic});
    };

    handleNumberChanged = (event: React.ChangeEvent<HTMLInputElement>) => {
        const topic: Topic = {...this.state.topic};
        topic.TopicNumber = parseInt(event.target.value);
        this.setState({topic});
    };

    handleTimeChanged = (event: React.ChangeEvent<HTMLInputElement>) => {
        const topic: Topic = {...this.state.topic};
        topic.TimeToPass = parseInt(event.target.value);
        this.setState({topic});
    };

    handleQuestionsChanged = (event: React.ChangeEvent<HTMLInputElement>) => {
        const topic: Topic = {...this.state.topic};
        topic.QuestionsPerAttempt = parseInt(event.target.value);
        this.setState({topic});
    };

    handleAttemptsChanged = (event: React.ChangeEvent<HTMLInputElement>) => {
        const topic: Topic = {...this.state.topic};
        topic.MaxAttemptCount = parseInt(event.target.value);
        this.setState({topic});
    };

    doSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const topic: Topic = {...this.state.topic};
        topic.SubjectId = this.props.subjectId;
        console.log(topic);
        this.props.onSubmit && this.props.onSubmit(topic);
    };

    render() {
        return (
            <Fragment>
                <Typography className="m-2" variant="h5">
                    Нова тема
                </Typography>
                <Divider/>
                <form noValidate autoComplete="off" onSubmit={this.doSubmit}>
                    <div className="m-2">
                        <Grid container spacing={2}>
                            <Grid item sm={12}><RequiredInput
                                id="name"
                                label="Name"
                                onChange={this.handleNameChanged}
                            /></Grid>
                        </Grid>
                        <Grid container spacing={2}>
                            <Grid item xs={12} sm={6}><RequiredInput
                                id="number"
                                label="Topic number"
                                onChange={this.handleNumberChanged}
                            /></Grid>
                            <Grid item xs={12} sm={6}><RequiredInput
                                id="time"
                                label="Time to pass"
                                onChange={this.handleTimeChanged}
                            /></Grid>
                        </Grid>
                        <Grid container spacing={2}>
                            <Grid item xs={12} sm={6}><RequiredInput
                                id="questionsCount"
                                label="Questions per attempt"
                                onChange={this.handleQuestionsChanged}
                            /></Grid>
                            <Grid item xs={12} sm={6}><RequiredInput
                                id="maxAttempts"
                                label="Max attempt count"
                                onChange={this.handleAttemptsChanged}
                            /></Grid>
                        </Grid>
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

export default TopicCreationForm;
