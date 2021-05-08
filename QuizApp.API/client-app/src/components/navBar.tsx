import React from "react";
import {AppBar, Button, Toolbar, Typography} from "@material-ui/core";
import {BrowserRouter, Link as RouterLink} from "react-router-dom";
import grey from "@material-ui/core/colors/grey"

export interface NavBarProps {
}

const primary = grey[50];

function NavBar() {
    return (
        <BrowserRouter>
            <AppBar position="sticky" color="primary">
                <Toolbar variant="dense">
                    <Typography className="mr-2" variant="h4">QuizApp</Typography>
                    <Button
                        component={RouterLink}
                        to="/subjectTopicTable"
                    >
                        <Typography variant="h6" style={{color: primary}}>
                            Tests
                        </Typography>
                    </Button>
                    <Button
                        component={RouterLink}
                        to="/studentTestResults"
                    >
                        <Typography variant="h6" style={{color: primary}}>
                            Results
                        </Typography>
                    </Button>
                </Toolbar>
            </AppBar>
        </BrowserRouter>
    );
}

export default NavBar;
