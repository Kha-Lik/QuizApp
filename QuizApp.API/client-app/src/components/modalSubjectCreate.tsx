import React from "react";
import {createStyles, makeStyles, Theme} from "@material-ui/core/styles";
import {Backdrop, Fade, Modal, Paper} from "@material-ui/core";
import SubjectCreationForm from "./subjectCreationForm";

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        modal: {
            display: "flex",
            alignItems: "center",
            justifyContent: "center",
        },
    })
);

export interface ModalSubjectCreateProps {
    openState: boolean;
    doClose: Function;
}

export default function ModalSubjectCreate({
                                               openState,
                                               doClose,
                                           }: ModalSubjectCreateProps) {
    const classes = useStyles();

    return (
        <Modal
            id="subjecCreation"
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
                    <SubjectCreationForm onSubmit={() => doClose()}/>
                </Paper>
            </Fade>
        </Modal>
    );
}
