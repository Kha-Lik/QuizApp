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

class App extends Component {
    state = {user: null};

    componentDidMount() {
        const user = true; //auth.getCurrentUser();
        this.setState({user});
    }

    render() {
        const {user} = this.state;

        return (
            <Fragment>
                <CssBaseline/>
                {user && <NavBar/>}
                <Container>
                    <BrowserRouter>
                        <Switch>
                            <Route path="/signin" component={SignIn}/>
                            <Route path="/signup" component={SignUp}/>
                            <Route path="/not-found" component={NotFound}/>
                            <Route path="/subjectTopictable" component={SubjectTopicTable}/>
                            <Redirect from="/" to="/not-found"/>
                            <Redirect to="/not-found"/>
                        </Switch>
                    </BrowserRouter>
                </Container>
            </Fragment>
        );
    }
}

export default App;
