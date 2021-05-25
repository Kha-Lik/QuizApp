import React from "react";
import {createStyles, makeStyles, Theme} from "@material-ui/core/styles";
import {Backdrop, Fade, Modal, Paper} from "@material-ui/core";
import QuestionCreationForm from "./questionCreationForm";

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        modal: {
            display: "flex",
            alignItems: "center",
            justifyContent: "center",
        },
    })
);

export interface ModalQuestionCreateProps {
    openState: boolean;
    doClose: Function;
    selectedTopic: string;
}

export default function ModalQuestionCreate({
                                                openState,
                                                doClose,
                                                selectedTopic,
                                            }: ModalQuestionCreateProps) {
    const classes = useStyles();

    return (
        <Modal
            id="topicCreation"
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
                <Paper elevation={5} variant="outlined" style={{width: "80%"}}>
                    <QuestionCreationForm
                        topicId={selectedTopic}
                        onSubmit={doClose}
                    />
                </Paper>
            </Fade>
        </Modal>
    );
}
