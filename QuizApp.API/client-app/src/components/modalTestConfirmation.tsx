import React from "react";
import {makeStyles, Theme, createStyles} from "@material-ui/core/styles";
import {Backdrop, Fade, Modal, Paper} from "@material-ui/core";
import SubjectCreationForm from "./subjectCreationForm";
import TestConfirmation from "./testConfirmation";
import {Topic, User} from "../appTypes";

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        modal: {
            display: "flex",
            alignItems: "center",
            justifyContent: "center",
        },
    })
);

export interface ModalTestConfirmationProps {
    openState: boolean;
    doClose: Function;
    topic: Topic;
    student: User;
    onTestConfirmation: Function;
}

export default function ModalTestConfirmation({
                                                  openState,
                                                  doClose,
                                                  student,
                                                  topic, onTestConfirmation
                                              }: ModalTestConfirmationProps) {
    const classes = useStyles();

    return (
        <Modal
            id="testConfirmation"
            className={classes.modal}
            open={openState}
            onClose={() => doClose()}
            closeAfterTransition
            BackdropComponent={Backdrop}
            BackdropProps={{
                timeout: 500,
            }}
        >
            <Fade in={openState}>
                <Paper elevation={5} variant="outlined" style={{width: "max-content", maxWidth: "40%"}}>
                    <TestConfirmation student={student} topic={topic} onTestConfirmation={onTestConfirmation}/>
                </Paper>
            </Fade>
        </Modal>
    );
}
