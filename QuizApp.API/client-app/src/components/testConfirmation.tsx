import React from 'react';
import {Link as RouterLink} from "react-router-dom";
import {Topic, User} from "../appTypes";
import {Button, createStyles, Grid, makeStyles, TextField, Theme, Typography} from "@material-ui/core";
import testService from "../services/fakeTestService";

export interface TestConfirmationProps {
    topic: Topic;
    student: User;
    onTestConfirmation: Function;
}

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
            title: {
                display: 'none',
                [theme.breakpoints.up('sm')]: {
                    display: 'block',
                },
                width: "100%",
                textAlign: "center"
            }
        }
    ),
);

function TestConfirmation({topic, student, onTestConfirmation}: TestConfirmationProps) {
    const {title} = useStyles();
    const attempts = countAttempts(student, topic);

    return (
        <div className="m-2">
            <Typography variant="h4" className={`${title} my-2`}>{topic.Name}</Typography>
            <Grid container spacing={1} alignItems="flex-end" xs={12}>
                <Grid item xs={6}>
                    <Typography>Час на проходження</Typography>
                </Grid>
                <Grid item xs={6}>
                    <TextField variant="outlined" size="small" disabled id="timeToPass"
                               defaultValue={topic.TimeToPass + " хв"}/>
                </Grid>
                <Grid item xs={6}>
                    <Typography>Кількість питань</Typography>
                </Grid>
                <Grid item xs={6}>
                    <TextField variant="outlined" size="small" disabled id="timeToPass"
                               defaultValue={topic.QuestionsPerAttempt}/>
                </Grid>
                <Grid item xs={6}>
                    <Typography>Доступні спроби</Typography>
                </Grid>
                <Grid item xs={6}>
                    <TextField variant="outlined" size="small" disabled id="timeToPass"
                               defaultValue={attempts}/>
                </Grid>
            </Grid>
            {!attempts && <Typography className={`${title} my-2`} color="error">Ви використали всі спроби</Typography>}
            <div className="container my-2" style={{width: "max-content", alignContent: "center"}}>
                <Button variant="contained"
                        color="primary"
                        disabled={attempts === 0}
                        component={RouterLink}
                        onClick={(event: React.MouseEvent<HTMLAnchorElement, MouseEvent>) => onTestConfirmation(event)}
                        to="/test"
                >
                    <Typography>Підтвердити</Typography>
                </Button>
            </div>
        </div>
    );
}

function countAttempts(student: User, topic: Topic): number {
    const attemptCount = testService.getTestResultsForStudent(student, topic.Id).length;
    return topic.MaxAttemptCount - attemptCount;
}

export default TestConfirmation;