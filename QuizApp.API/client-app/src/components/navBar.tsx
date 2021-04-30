import React from "react";
import { AppBar, Link, Toolbar, Typography } from "@material-ui/core";
import { BrowserRouter, NavLink } from "react-router-dom";

export interface NavBarProps {}

function NavBar() {
  return (
    <BrowserRouter>
      <AppBar position="sticky" color="primary">
        <Toolbar variant="dense">
          <Typography variant="h4">QuizApp</Typography>
          <Link
            component={NavLink}
            className="nav-item nav-link"
            to="/subjectTopicTable"
            color="inherit"
            underline="none"
          >
            <Typography variant="h6" color="inherit">
              Tests
            </Typography>
          </Link>
          <Link
            component={NavLink}
            className="nav-item nav-link"
            to="/studentTestResults"
            color="inherit"
            underline="none"
          >
            <Typography variant="h6" color="inherit">
              Results
            </Typography>
          </Link>
        </Toolbar>
      </AppBar>
    </BrowserRouter>
  );
}

export default NavBar;
