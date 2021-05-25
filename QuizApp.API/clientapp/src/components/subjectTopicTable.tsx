import React, {Component} from "react";
import {Divider, Paper, Typography} from "@material-ui/core";
import {Question, Subject, Topic} from "../appTypes";
import ListGroup from "./common/listGroup";
import subjectService from "../services/fakeSubjectService";
import topicService from "../services/fakeTopicService";
import questionService from "./../services/fakeQuestionService";
import ModalSubjectCreate from "./modalSubjectCreate";
import ModalTopicCreate from "./modalTopicCreate";
import ModalQuestionCreate from "./modalQuestionCreate";

export interface SubjectTopicTableProps {
}

export interface SubjectTopicTableState {
    subjects: Subject[];
    topics: Topic[];
    questions: Question[];
    subjectFormOpen: boolean;
    topicFormOpen: boolean;
    questionFormOpen: boolean;
    selectedSubjectId: string;
    selectedTopicId: string;
}

class SubjectTopicTable extends Component<SubjectTopicTableProps,
    SubjectTopicTableState> {
    state = {
        subjects: [],
        topics: [],
        questions: [],
        subjectFormOpen: false,
        topicFormOpen: false,
        questionFormOpen: false,
        selectedSubjectId: "",
        selectedTopicId: "",
    };

    componentDidMount() {
        const subjects = subjectService.getAllSubjects();
        this.setState({subjects});
    }

    handleSubjectSelectionChanged = (id: string) => {
        const topics = topicService.getTopicsBySubjectId(id);
        const questions: Question[] = [];
        this.setState({topics, questions, selectedSubjectId: id});
    };

    handleTopicSelectionChanged = (id: string) => {
        const questions = questionService.getQuestionsByTopicid(id);
        this.setState({questions, selectedTopicId: id});
    };

    handleSubjectFormOpen = () => {
        this.setState({subjectFormOpen: true});
    };

    handleSubjectFormClose = (subject: Subject) => {
        const subjects = [...(this.state.subjects as Array<Subject>)];
        subjects.push(subject);
        this.setState({subjectFormOpen: false, subjects});
    };

    handleTopicFormOpen = () => {
        this.setState({topicFormOpen: true});
    };

    handleTopicFormClose = (topic: Topic) => {
        const topics = [...(this.state.topics as Array<Topic>)];
        topics.push(topic);
        this.setState({topicFormOpen: false, topics});
    };

    handleQuestionFormOpen = () => {
        this.setState({questionFormOpen: true});
    };

    handleQuestionFormClose = (question: Question) => {
        const questions = [...(this.state.questions as Array<Question>)];
        questions.push(question);
        this.setState({questionFormOpen: false, questions});
    };
    
    handleSubjectDelete = (id: string) => {
        const subjects = [...(this.state.subjects as Array<Subject>).filter(s => s.Id !== id)];
        this.setState({subjects});
    };

    handleTopicDelete = (id: string) => {
        const topics = [...(this.state.subjects as Array<Topic>).filter(s => s.Id !== id)];
        this.setState({topics});
    };

    handleQuestionDelete = (id: string) => {
        const questions = [...(this.state.subjects as Array<Question>).filter(s => s.Id !== id)];
        this.setState({questions});
    }

    render() {
        const {subjects, topics, questions} = this.state;

        return (
            <Paper variant="outlined" elevation={0} className="m-2">
                <div className="row mx-auto">
                    <div className="col-3">
                        <Typography variant="h6" color="inherit">
                            Предмети
                        </Typography>
                    </div>
                    <div className="col-3">
                        <Typography variant="h6" color="inherit">
                            Теми
                        </Typography>
                    </div>
                    <div className="col">
                        <Typography variant="h6" color="inherit">
                            Питання
                        </Typography>
                    </div>
                </div>
                <Divider/>
                <div className="row">
                    <div className="col-3">
                        <Paper>
                            <ListGroup
                                data={subjects}
                                contentProperty="Name"
                                idProperty="Id"
                                createButton={true}
                                deleteButton={true}
                                onCreate={this.handleSubjectFormOpen}
                                onDelete={this.handleSubjectDelete}
                                onSelectionChanged={this.handleSubjectSelectionChanged}
                            />
                        </Paper>
                    </div>
                    <div className="col-3">
                        {!(topics.length === 0) && (
                            <Paper>
                                <ListGroup
                                    data={topics}
                                    contentProperty="Name"
                                    idProperty="Id"
                                    createButton={true}
                                    deleteButton={true}
                                    onCreate={this.handleTopicFormOpen}
                                    onDelete={this.handleTopicDelete}
                                    onSelectionChanged={this.handleTopicSelectionChanged}
                                />
                            </Paper>
                        )}
                    </div>
                    <div className="col">
                        {!(questions.length === 0) && (
                            <Paper>
                                <ListGroup
                                    data={questions}
                                    contentProperty="QuestionText"
                                    idProperty="Id"
                                    createButton={true}
                                    deleteButton={true}
                                    onDelete={this.handleQuestionDelete}
                                    onCreate={this.handleQuestionFormOpen}
                                />
                            </Paper>
                        )}
                    </div>
                </div>
                <ModalSubjectCreate
                    openState={this.state.subjectFormOpen}
                    doClose={this.handleSubjectFormClose}
                />
                <ModalTopicCreate
                    openState={this.state.topicFormOpen}
                    doClose={this.handleTopicFormClose}
                    selectedSubject={this.state.selectedSubjectId}
                />
                <ModalQuestionCreate
                    openState={this.state.questionFormOpen}
                    doClose={this.handleQuestionFormClose}
                    selectedTopic={this.state.selectedTopicId}
                />
            </Paper>
        );
    }
}

export default SubjectTopicTable;
