import React, {Component, Fragment} from "react";
import {BrowserRouter, Redirect, Route, Switch} from "react-router-dom";
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
import PersonalResults from "./components/personalResults";
import TestSelection from "./components/testSelection";
import TestView from "./components/testView";
import {JwtUser, User} from "./appTypes";
import ProtectedRoute from "./components/common/protectedRoute";

interface AppState {
    user: JwtUser;
    student?: User;
    topicId?: string;
}

export interface AppProps {
}

class App extends Component<AppProps, AppState> {
    state: AppState;

    constructor(props: AppProps) {
        super(props);
        this.state = {user: auth.getCurrentUser(), student: undefined, topicId: undefined}
    }

    onTestConfirmation = (student: User, topicId: string) => {
        this.setState({student, topicId});
    };

    render() {
        const {user, student, topicId} = this.state;

        return (
            <BrowserRouter>
                <Fragment>
                    <CssBaseline/>
                    {user && <NavBar user={user}/>}
                    <Container>
                        <Switch>
                            <Route path="/signIn" component={SignIn}/>
                            <Route path="/signUp" component={SignUp}/>
                            <Route path="/not-found" component={NotFound}/>
                            <ProtectedRoute path="/testCreation" component={SubjectTopicTable} role={"Lecturer"}/>
                            <ProtectedRoute path="/studentsResults" component={StudentTestResults} role={"Lecturer"}/>
                            <ProtectedRoute path="/results" component={PersonalResults} role={"Student"}/>
                            <ProtectedRoute path="/tests" role={"student"}
                                   render={(props) =>
                                       <TestSelection {...props} onTestConfirmation={this.onTestConfirmation}/>}/>
                            <ProtectedRoute path="/test" role={"Student"}
                                   render={(props) =>
                                       <TestView {...props} student={student as User} topicId={topicId as string}/>}/>
                            <Redirect from="/" to="/not-found"/>
                            <Redirect to="/not-found"/>
                        </Switch>
                    </Container>
                </Fragment>
            </BrowserRouter>
        );
    }
}

export default App;
