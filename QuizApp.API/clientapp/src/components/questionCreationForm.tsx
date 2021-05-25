import React, {Fragment} from "react";
import {Question} from "../appTypes";
import {Button, Checkbox, Divider, Grid, Paper, Typography,} from "@material-ui/core";
import RequiredInput from "./common/requiredInput";

export interface QuestionCreationFormProps {
    topicId: string;
    onSubmit?: Function;
}

export interface QuestionCreationFormState {
    question: Question;
    firstAnswerCheck: boolean;
    secondAnswerCheck: boolean;
    thirdAnswerCheck: boolean;
}

class QuestionCreationForm extends React.Component<QuestionCreationFormProps,
    QuestionCreationFormState> {
    state: QuestionCreationFormState;

    constructor(props: QuestionCreationFormProps) {
        super(props);
        this.state = {
            question: {
                Id: "",
                TopicId: "",
                QuestionNumber: 0,
                QuestionText: "",
                Answers: [
                    {
                        Id: "firstAnswer",
                        QuestionId: "",
                        AnswerText: "",
                        IsCorrect: false,
                    },
                    {
                        Id: "secondAnswer",
                        QuestionId: "",
                        AnswerText: "",
                        IsCorrect: false,
                    },
                    {
                        Id: "thirdAnswer",
                        QuestionId: "",
                        AnswerText: "",
                        IsCorrect: false,
                    },
                ],
            },
            firstAnswerCheck: false,
            secondAnswerCheck: false,
            thirdAnswerCheck: false,
        };
    }

    handleTextChanged = (event: React.ChangeEvent<HTMLInputElement>) => {
        const question: Question = {...this.state.question};
        question.QuestionText = event.target.value;
        this.setState({question});
    };

    handleNumberChanged = (event: React.ChangeEvent<HTMLInputElement>) => {
        const question: Question = {...this.state.question};
        question.QuestionNumber = parseInt(event.target.value);
        this.setState({question});
    };

    handleAnswerChanged = (event: React.ChangeEvent<HTMLInputElement>) => {
        const answerText = event.target.value;
        const id = event.target.id;
        const answers = [...this.state.question.Answers];
        const answer = answers.filter((a) => a.Id === id)[0];
        const index = answers.indexOf(answer);
        answers[index] = {...answers[index]};
        answers[index].AnswerText = answerText;
        const question = {...this.state.question};
        question.Answers = answers;
        this.setState({question});
    };

    handleAnswerChecked = (event: React.ChangeEvent<HTMLInputElement>) => {
        const flag = event.target.checked;
        const id = event.target.id;
        const state: any = {...this.state};
        state[id] = flag;
        this.setState(state);
    };

    doSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const question = {...this.state.question};
        question.Answers.forEach((answer) => {
            answer.QuestionId = question.Id;
            answer.IsCorrect = (this.state as any)[`${answer.Id}Check`];
        });
        question.TopicId = this.props.topicId;
        console.log(question);
        this.props.onSubmit && this.props.onSubmit(question);
    };

    render() {
        const {
            firstAnswerCheck,
            secondAnswerCheck,
            thirdAnswerCheck,
        } = this.state;

        return (
            <Fragment>
                <Typography className="m-2" variant="h6">
                    Нове питання
                </Typography>
                <Divider/>
                <form noValidate autoComplete="off" onSubmit={this.doSubmit}>
                    <div className="m-1">
                        <Grid container spacing={2} xs={12} style={{alignContent: "center"}}>
                            <Grid item xs={12} className="ml-2">
                                <RequiredInput
                                    id="number"
                                    label="Question number"
                                    onChange={this.handleNumberChanged}
                                />
                            </Grid>
                            <Grid item xs={12} className="ml-2">
                                <RequiredInput
                                    id="text"
                                    label="Question's text"
                                    onChange={this.handleTextChanged}
                                />
                            </Grid>
                        </Grid>
                        <Paper variant="outlined" elevation={3} className="m-2">
                            <div className="row" style={{alignContent: "right"}}>
                                <div className="col-9">
                                    <Typography className="mx-3 my-2" variant="h6">
                                        Відповіді
                                    </Typography>
                                </div>
                                <div className="col-3" style={{textAlign: "center"}}>
                                    <Typography className="m-2" variant="h6">
                                        Правильна
                                    </Typography>
                                </div>
                            </div>
                            <Divider/>
                            <div className="row mx-sm-n1">
                                <div className="col-10">
                                    <RequiredInput
                                        id="firstAnswer"
                                        label="First answer"
                                        onChange={this.handleAnswerChanged}
                                    />
                                </div>
                                <div className="col-1 my-auto">
                                    <Checkbox
                                        id="firstAnswerCheck"
                                        checked={firstAnswerCheck}
                                        onChange={this.handleAnswerChecked}
                                        inputProps={{"aria-label": "first answer checkbox"}}
                                    />
                                </div>
                            </div>
                            <div className="row mx-sm-n1">
                                <div className="col-10">
                                    <RequiredInput
                                        id="secondAnswer"
                                        label="Second answer"
                                        onChange={this.handleAnswerChanged}
                                    />
                                </div>
                                <div className="col-1 my-auto">
                                    <Checkbox
                                        id="secondAnswerCheck"
                                        checked={secondAnswerCheck}
                                        onChange={this.handleAnswerChecked}
                                        inputProps={{"aria-label": "second answer checkbox"}}
                                    />
                                </div>
                            </div>
                            <div className="row mx-sm-n1">
                                <div className="col-10">
                                    <RequiredInput
                                        id="thirdAnswer"
                                        label="Third answer"
                                        onChange={this.handleAnswerChanged}
                                    />
                                </div>
                                <div className="col-1 my-auto">
                                    <Checkbox
                                        id="thirdAnswerCheck"
                                        checked={thirdAnswerCheck}
                                        onChange={this.handleAnswerChecked}
                                        inputProps={{"aria-label": "third answer checkbox"}}
                                    />
                                </div>
                            </div>
                        </Paper>
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

export default QuestionCreationForm;
