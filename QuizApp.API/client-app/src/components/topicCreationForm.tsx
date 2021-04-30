import React, { Fragment } from "react";
import { Topic } from "../appTypes";
import { Button, Divider, Typography } from "@material-ui/core";
import RequiredInput from "./common/requiredInput";

export interface TopicCreationFormProps {
  subjectId: string;
  onSubmit?: Function;
}

export interface TopicCreationFormState {
  topic: Topic;
}

class TopicCreationForm extends React.Component<
  TopicCreationFormProps,
  TopicCreationFormState
> {
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

  handleIdChanged = (event: any) => {
    const topic: Topic = { ...this.state.topic };
    topic.Id = event.target.value;
    this.setState({ topic });
  };

  handleNameChanged = (event: any) => {
    const topic: Topic = { ...this.state.topic };
    topic.Name = event.target.value;
    this.setState({ topic });
  };

  handleNumberChanged = (event: any) => {
    const topic: Topic = { ...this.state.topic };
    topic.TopicNumber = parseInt(event.target.value);
    this.setState({ topic });
  };

  handleTimeChanged = (event: any) => {
    const topic: Topic = { ...this.state.topic };
    topic.TimeToPass = parseInt(event.target.value);
    this.setState({ topic });
  };

  handleQuestionsChanged = (event: any) => {
    const topic: Topic = { ...this.state.topic };
    topic.QuestionsPerAttempt = parseInt(event.target.value);
    this.setState({ topic });
  };

  handleAttemptsChanged = (event: any) => {
    const topic: Topic = { ...this.state.topic };
    topic.MaxAttemptCount = parseInt(event.target.value);
    this.setState({ topic });
  };

  doSubmit = (event: any) => {
    event.preventDefault();
    const topic: Topic = { ...this.state.topic };
    topic.SubjectId = this.props.subjectId;
    console.log(topic);
    this.props.onSubmit && this.props.onSubmit();
  };

  render() {
    return (
      <Fragment>
        <Typography className="m-2" variant="h5">
          Нова тема
        </Typography>
        <Divider />
        <form noValidate autoComplete="off" onSubmit={this.doSubmit}>
          <div className="m-2">
            <RequiredInput
              id="id"
              label="Topic ID"
              onChange={this.handleIdChanged}
            />
            <RequiredInput
              id="name"
              label="Name"
              onChange={this.handleNameChanged}
            />
            <RequiredInput
              id="number"
              label="Topic number"
              onChange={this.handleNumberChanged}
            />
            <RequiredInput
              id="time"
              label="Time to pass"
              onChange={this.handleTimeChanged}
            />
            <RequiredInput
              id="questionsCount"
              label="Questions per attempt"
              onChange={this.handleQuestionsChanged}
            />
            <RequiredInput
              id="maxAttempts"
              label="Max attempt count"
              onChange={this.handleAttemptsChanged}
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

export default TopicCreationForm;
