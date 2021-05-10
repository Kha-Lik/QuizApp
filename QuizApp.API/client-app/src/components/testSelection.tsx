import React, {Component} from 'react';
import {Button, Divider, Paper, Typography} from "@material-ui/core";
import ListGroup from "./common/listGroup";
import subjectService from "../services/fakeSubjectService";
import topicService from "../services/fakeTopicService";
import userService from "../services/fakeuserService";
import {Subject, Topic, User} from '../appTypes';
import ModalTestConfirmation from "./modalTestConfirmation";
import auth from "../services/authService";

export interface TestSelectionProps {
    onTestConfirmation: Function;
}

interface TestSelectionState {
    lecturers: Array<User>;
    subjects: Array<Subject>;
    topics: Array<Topic>;
    selectedLecturerId: string;
    selectedSubjectId: string;
    selectedTopicId: string;
    testConfirmationModalOpen: boolean;
    student: User
};

class TestSelection extends Component<TestSelectionProps, TestSelectionState> {
    state : TestSelectionState

    constructor(props : TestSelectionProps) {
        super(props);
        const student = userService.getStudentById(auth.getCurrentUser().NameIdentifier) as User;
        this.state = {
            lecturers: [],
            subjects: [],
            topics: [],
            selectedLecturerId: "",
            selectedSubjectId: "",
            selectedTopicId: "",
            testConfirmationModalOpen: false,
            student: student
        };
    }

    componentDidMount() {
        const lecturers = userService.getLecturers();
        this.setState({lecturers});
    }

    handleLecturerSelectionChanged = (id : string) => {
        const subjects = subjectService.getSubjectsByLecturerId(id);
        const topics : Topic[] = [];
        const selectedTopicId = "";
        this.setState({subjects, topics, selectedLecturerId: id, selectedTopicId});
    }

    handleSubjectSelectionChanged = (id: string) => {
        const topics = topicService.getTopicsBySubjectId(id);
        const selectedTopicId = "";
        this.setState({topics, selectedSubjectId: id, selectedTopicId});
    };

    handleTopicSelectionChanged = (id: string) => {
        this.setState({selectedTopicId: id});
    };

    handleTestConfirmationModalOpen = () => {
        this.setState({testConfirmationModalOpen: true});
    };

    handleTestConfirmationModalClose = () => {
        this.setState({testConfirmationModalOpen: false});
    };

    doTestConfirmation = (event: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
        this.props.onTestConfirmation(this.state.student, this.state.selectedTopicId)
    };

    render() {
        const {lecturers, subjects, topics, selectedTopicId, student} = this.state;

        return (
            <Paper variant="outlined" elevation={0} className="m-2">
                <div className="row mx-auto">
                    <div className="col-3">
                        <Typography variant="h6" color="inherit">
                            Викладачі
                        </Typography>
                    </div>
                    <div className="col-3">
                        <Typography variant="h6" color="inherit">
                            Предмети
                        </Typography>
                    </div>
                    <div className="col">
                        <Typography variant="h6" color="inherit">
                            Теми
                        </Typography>
                    </div>
                </div>
                <Divider/>
                <div className="row">
                    <div className="col-3">
                        <Paper>
                            <ListGroup
                                data={lecturers}
                                contentProperty="Surname"
                                secondaryTextProperty="Name"
                                idProperty="Id"
                                onSelectionChanged={this.handleLecturerSelectionChanged}
                            />
                        </Paper>
                    </div>
                    <div className="col-3">
                        <Paper>
                            <ListGroup
                                data={subjects}
                                contentProperty="Name"
                                idProperty="Id"
                                onSelectionChanged={this.handleSubjectSelectionChanged}
                            />
                        </Paper>
                    </div>
                    <div className="col">
                        {!(topics.length === 0) && (
                            <Paper>
                                <ListGroup
                                    data={topics}
                                    contentProperty="Name"
                                    idProperty="Id"
                                    onSelectionChanged={this.handleTopicSelectionChanged}
                                />
                            </Paper>
                        )}
                    </div>
                </div>
                <div className="container my-2" style={{width: "max-content", alignContent: "center"}}>
                    <Button variant="contained"
                            color="primary"
                            disabled={selectedTopicId === ""}
                            onClick={(event) => this.handleTestConfirmationModalOpen()}
                    >
                        <Typography>Пройти тест</Typography>
                    </Button>
                </div>
                <ModalTestConfirmation
                    openState={this.state.testConfirmationModalOpen}
                    doClose={this.handleTestConfirmationModalClose}
                    student={student}
                    topic={topics.filter((t : Topic) => t.Id === selectedTopicId)[0]}
                    onTestConfirmation={this.doTestConfirmation}
                />
            </Paper>
        );
    }
}

export default TestSelection;
