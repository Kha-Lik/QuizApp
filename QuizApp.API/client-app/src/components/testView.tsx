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
    time: { h: number, m: number, s: number };
    seconds: number;
}

class TestView extends Component<TestViewProps, TestViewState> {
    state: TestViewState;
    timer: NodeJS.Timeout | undefined = undefined;

    constructor(props: TestViewProps) {
        super(props);
        const test = testService.generateTestForTopic(props.student, props.topicId);
        this.state = {
            test,
            seconds: 0,
            time: {h: 0, m: 0, s: 0}
        }
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
                <Grid container spacing={1} className="m-2">
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
                    <Checkbox checked={answer.IsCorrect}
                              onClick={(event) => this.handleCheckboxClicked(event, answer)}/>
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

    doSubmit = (event?: React.MouseEvent<HTMLButtonElement>) => {
        const test = {...this.state.test};
        test.DateTimePassed = new Date(Date.now());
        console.log(test);
    }

    countEmptyQuestions(): number {
        return this.state.test.Questions.filter(q => q.Answers.filter(a => a.IsCorrect).length === 0).length;
    }

    secondsToTime(secs: number) {
        const hours = Math.floor(secs / (60 * 60));

        const divisor_for_minutes = secs % (60 * 60);
        const minutes = Math.floor(divisor_for_minutes / 60);

        const divisor_for_seconds = divisor_for_minutes % 60;
        const seconds = Math.ceil(divisor_for_seconds);

        const time = {
            h: hours,
            m: minutes,
            s: seconds
        };
        return time;
    }

    componentDidMount() {
        const seconds = this.state.test.Topic.TimeToPass * 60;
        const timeLeft = this.secondsToTime(seconds);
        this.timer = setInterval(this.countDown, 1000);
        this.setState({time: timeLeft, seconds});
    }

    countDown = () => {
        const seconds = this.state.seconds - 1;
        this.setState({
            time: this.secondsToTime(seconds),
            seconds: seconds,
        });

        if (seconds === 0) {
            clearInterval(this.timer as NodeJS.Timeout);
            this.doSubmit();
        }
    }

    render() {
        const {test, time} = this.state;
        const emptyQuestions = this.countEmptyQuestions();

        return (
            <Grid container>
                <Grid item xs={3}>
                    <Paper variant={"outlined"} elevation={3} className={"m-2"}>
                        <Typography variant={"h6"} className={"m-2"}>{test.Topic.Name}</Typography>
                        <Typography
                            className={"m-2"}>{"Залишилося питань: " + emptyQuestions}</Typography>
                        <Typography
                            className={"m-2"}>{"Залишилося часу: " + time.h + ":" + time.m + ":" + time.s}</Typography>
                        <div className="container my-2" style={{width: "max-content", alignContent: "center"}}>
                            <Button variant="contained"
                                    color="primary"
                                    disabled={emptyQuestions !== 0 || this.state.seconds === 0}
                                    onClick={(event) => this.doSubmit(event)}
                            >
                                <Typography>Завершити тест</Typography>
                            </Button>
                        </div>
                    </Paper>
                </Grid>
                <Grid item xs={9}>
                    <Paper variant="outlined" elevation={3} className="m-2">
                        <Grid container spacing={1}>
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