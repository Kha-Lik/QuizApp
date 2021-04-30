import React, { Component } from "react";
import {
  Backdrop,
  Divider,
  Fade,
  Modal,
  Paper,
  Typography,
} from "@material-ui/core";
import { Subject, Topic, Question } from "../appTypes";
import ListGroup from "./common/listGroup";
import subjectService from "../services/fakeSubjectService";
import topicService from "../services/fakeTopicService";
import questionService from "./../services/fakeQuestionService";
import SubjectCreationForm from "./subjectCreationForm";
import TopicCreationForm from "./topicCreationForm";

export interface SubjectTopicTableProps {}

export interface SubjectTopicTableState {
  subjects: Subject[];
  topics: Topic[];
  questions: Question[];
  subjectFormOpen: boolean;
  topicFormOpen: boolean;
  selectedSubjectId: string;
}

class SubjectTopicTable extends Component<
  SubjectTopicTableProps,
  SubjectTopicTableState
> {
  state = {
    subjects: [],
    topics: [],
    questions: [],
    subjectFormOpen: false,
    topicFormOpen: false,
    selectedSubjectId: "",
  };

  componentDidMount() {
    const subjects = subjectService.getAllSubjects();
    this.setState({ subjects });
  }

  handleSubjectSelectionChanged = (id: string) => {
    const topics = topicService.getTopicsBySubjectId(id);
    const questions: Question[] = [];
    this.setState({ topics, questions, selectedSubjectId: id });
  };

  handleTopicSelectionChanged = (id: string) => {
    const questions = questionService.getQuestionsByTopicid(id);
    this.setState({ questions });
  };

  handleSubjectFormOpen = () => {
    this.setState({ subjectFormOpen: true });
  };

  handleSubjectFormClose = () => {
    this.setState({ subjectFormOpen: false });
  };

  handleTopicFormOpen = () => {
    this.setState({ topicFormOpen: true });
  };

  handleTopicFormClose = () => {
    this.setState({ topicFormOpen: false });
  };

  render() {
    const { subjects, topics, questions } = this.state;
    const modalStyle = {
      display: "flex",
      alignItems: "center",
      justifyContent: "center",
    };

    return (
      <Paper variant="outlined" elevation={0} className="m-2">
        <div className="row mx-auto">
          <div className="col-2">
            <Typography variant="h6" color="inherit">
              Предмети
            </Typography>
          </div>
          <div className="col-2">
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
        <Divider />
        <div className="row">
          <div className="col-2">
            <Paper>
              <ListGroup
                data={subjects}
                contentProperty="Name"
                idProperty="Id"
                createButton={true}
                deleteButton={true}
                onCreate={this.handleSubjectFormOpen}
                onDelete={(id: string) => console.log(`Subject ${id} deleted`)}
                onSelectionChanged={this.handleSubjectSelectionChanged}
              ></ListGroup>
            </Paper>
          </div>
          <div className="col-2">
            {!(topics.length === 0) && (
              <Paper>
                <ListGroup
                  data={topics}
                  contentProperty="Name"
                  idProperty="Id"
                  createButton={true}
                  deleteButton={true}
                  onCreate={this.handleTopicFormOpen}
                  onDelete={(id: string) => console.log(`Topic ${id} deleted`)}
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
                  onDelete={(id: string) =>
                    console.log(`Question ${id} deleted`)
                  }
                  onCreate={() => console.log("Question creation")}
                />
              </Paper>
            )}
          </div>
        </div>
        <Modal
          id="topicCreation"
          style={modalStyle}
          open={this.state.topicFormOpen}
          onClose={this.handleTopicFormClose}
          closeAfterTransition
          BackdropComponent={Backdrop}
          BackdropProps={{
            timeout: 500,
          }}
        >
          <Fade in={this.state.topicFormOpen}>
            <Paper elevation={5} variant="outlined">
              <TopicCreationForm
                subjectId={this.state.selectedSubjectId}
                onSubmit={this.handleTopicFormClose}
              />
            </Paper>
          </Fade>
        </Modal>
        <Modal
          id="subjecCreation"
          style={modalStyle}
          open={this.state.subjectFormOpen}
          onClose={this.handleSubjectFormClose}
          closeAfterTransition
          BackdropComponent={Backdrop}
          BackdropProps={{
            timeout: 500,
          }}
        >
          <Fade in={this.state.subjectFormOpen}>
            <Paper elevation={5} variant="outlined">
              <SubjectCreationForm onSubmit={this.handleSubjectFormClose} />
            </Paper>
          </Fade>
        </Modal>
      </Paper>
    );
  }
}

export default SubjectTopicTable;
