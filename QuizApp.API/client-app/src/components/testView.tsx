import React, {Component} from 'react';
import {Answer, Question, Test, User} from "../appTypes";
import {Button, Checkbox, Divider, Grid, Paper, Typography} from "@material-ui/core";
import testService from "../services/fakeTestService";

export interface TestViewProps {
    student: User;
    topicId: string;
}

interface TestViewState {
    test: Test;
}

class TestView extends Component<TestViewProps, TestViewState> {
    state: TestViewState;

    constructor(props: TestViewProps) {
        super(props);
        this.state = {test: testService.generateTestForTopic(props.student, props.topicId)}
    }

    renderQuestion(question: Question): JSX.Element {
        return (
            <>
                <Grid item sm={12}>
                    <Divider color="primary"/>
                </Grid>
                <Grid item sm={12} className={"mx-2"}>
                    <Typography>{question.QuestionNumber + ". " + question.QuestionText}</Typography>
                </Grid>
                <Grid container xs={12} spacing={1} className="m-2">
                    {question.Answers.map(a => this.renderAnswer(a))}
                    <Grid item sm={12}>
                        <Divider variant={"middle"}/>
                    </Grid>
                </Grid>
            </>
        );
    }

    renderAnswer(answer: Answer): JSX.Element {
        return (
            <>
                <Grid item sm={12}>
                    <Divider variant={"middle"}/>
                </Grid>
                <Grid item sm={1} className={"ml-1"} style={{alignContent: "center", display: "flex"}}>
                    <Checkbox checked={answer.IsCorrect} onClick={(event) => this.handleCheckboxClicked(event, answer)}/>
                </Grid>
                <Grid item sm={10} className={"mr-1"}>
                    <Typography>{answer.AnswerText}</Typography>
                </Grid>
            </>
        );
    };

    handleCheckboxClicked = (event: React.MouseEvent<HTMLButtonElement>, answer: Answer) => {
        const test = {...this.state.test};
        const questions = [...test.Questions];
        const questionIndex = questions.indexOf(questions.filter(q => q.Answers.includes(answer))[0]);
        const ans = {...answer};
        ans.IsCorrect = !ans.IsCorrect;
        const answerIndex = questions[questionIndex].Answers.indexOf(answer);
        questions[questionIndex].Answers[answerIndex] = ans;
        test.Questions = questions;
        this.setState({test});
    }

    handleSubmit = (event: React.MouseEvent<HTMLButtonElement>) => {
        const test = {...this.state.test};
        test.DateTimePassed = new Date(Date.now());
        console.log(test);
    }

    render() {
        const {test} = this.state;
        return (
            <Grid container xs={12}>
                <Grid item xs={3}>
                    <Paper variant={"outlined"} elevation={3} className={"m-2"}>
                        <Typography variant={"h6"} className={"m-2"}>{test.Topic.Name}</Typography>
                        <Typography
                            className={"m-2"}>{"Залишилося питань: " + test.Topic.QuestionsPerAttempt}</Typography>
                        <Typography
                            className={"m-2"}>{"Залишилося часу: " + test.Topic.TimeToPass + " хв."}</Typography>
                        <div className="container my-2" style={{width: "max-content", alignContent: "center"}}>
                            <Button variant="contained"
                                    color="primary"
                                    onClick={(event) => this.handleSubmit(event)}
                            >
                                <Typography>Завершити тест</Typography>
                            </Button>
                        </div>
                    </Paper>
                </Grid>
                <Grid item xs={9}>
                    <Paper variant="outlined" elevation={3} className="m-2">
                        <Grid container spacing={1} sm={12}>
                            {test.Questions.map(q => this.renderQuestion(q))}
                            <Grid item sm={12}>
                                <Divider color="primary"/>
                            </Grid>
                        </Grid>
                    </Paper>
                </Grid>
            </Grid>
        );
    };
}

export default TestView;