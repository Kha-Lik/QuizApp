import React from "react";
import {createStyles, makeStyles, Theme} from "@material-ui/core/styles";
import {Backdrop, Fade, Modal, Paper} from "@material-ui/core";
import TopicCreationForm from "./topicCreationForm";

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        modal: {
            display: "flex",
            alignItems: "center",
            justifyContent: "center",
        },
    })
);

export interface ModalTopicCreateProps {
    openState: boolean;
    doClose: Function;
    selectedSubject: string;
}

export default function ModalTopicCreate({
                                             openState,
                                             doClose,
                                             selectedSubject,
                                         }: ModalTopicCreateProps) {
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
                <Paper elevation={5} variant="outlined">
                    <TopicCreationForm
                        subjectId={selectedSubject}
                        onSubmit={() => doClose()}
                    />
                </Paper>
            </Fade>
        </Modal>
    );
}
