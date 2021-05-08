import React, {Component, Fragment} from "react";
import {BrowserRouter, Route, Redirect, Switch} from "react-router-dom";
import {Container, CssBaseline} from "@material-ui/core";
import NavBar from "./components/navBar";
import SignIn from "./components/signIn";
import SignUp from "./components/signUp";
import NotFound from "./components/notFound";
import SubjectTopicTable from "./components/subjectTopicTable";
import auth from "./services/authService";
import "bootstrap/dist/css/bootstrap.css";
import "@fortawesome/fontawesome-free/css/all.css"
import StudentTestResults from "./components/studentTestResults";

class App extends Component {
    state = {user: null};

    componentDidMount() {
        const user = true; //auth.getCurrentUser();
        this.setState({user});
    }

    render() {
        const {user} = this.state;

        return (
            <BrowserRouter>
                <Fragment>
                    <CssBaseline/>
                    {user && <NavBar/>}
                    <Container>
                        <Fragment><Switch>
                            <Route path="/signin" component={SignIn}/>
                            <Route path="/signup" component={SignUp}/>
                            <Route path="/not-found" component={NotFound}/>
                            <Route path="/subjectTopicTable" component={SubjectTopicTable}/>
                            <Route path="/studentTestResults" component={StudentTestResults}/>
                            <Redirect from="/" to="/not-found"/>
                            <Redirect to="/not-found"/>
                        </Switch></Fragment>
                    </Container>
                </Fragment>
            </BrowserRouter>
        );
    }
}

export default App;
