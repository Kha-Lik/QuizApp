import React, {ChangeEvent, Component} from 'react';
import {Paper} from '@material-ui/core';
import FilteredList from "./common/filteredList";
import {AttemptViewModel, User} from '../appTypes';
import userService from "../services/fakeuserService";
import Table from "./common/table";
import {Column, SortColumn} from "./common/commonTypes";
import attemptService from "../services/fakeAttemptService";
import _ from "lodash";

export interface StudentTestResultsState {
    students: User[];
    filteredStudents: User[];
    attempts: AttemptViewModel[];
    filteredAttempts: Array<AttemptViewModel>;
    columns: Array<Column>;
    sortColumn: SortColumn;
}

export interface StudentTestResultsProps {
}

class StudentTestResults extends Component<StudentTestResultsProps, StudentTestResultsState> {
    state: StudentTestResultsState;

    constructor(props: StudentTestResultsProps) {
        super(props);
        const students = userService.getStudents();
        const attempts = attemptService.getAllAttempts();
        const columns: Column[] = [
            {
                path: "StudentName",
                label: "Студент"
            },
            {
                path: "SubjectName",
                label: "Предмет"
            },
            {
                path: "TopicName",
                label: "Тема"
            },
            {
                path: "Score",
                label: "Бал"
            },
            {
                path: "DateTime",
                label: "Дата проходження"
            }
        ];
        const sortColumn: SortColumn = {path: "StudentName", order: "asc"};
        this.state = {
            students,
            filteredStudents: [...students],
            attempts,
            filteredAttempts: [...attempts],
            columns,
            sortColumn
        };
    }

    render() {
        const {filteredStudents, columns, sortColumn, filteredAttempts} = this.state;

        return (
            <Paper variant="outlined" elevation={3} className="m-2">
                <div className="row m-2">
                    <div className="col-3 m-2">
                        <FilteredList data={filteredStudents}
                                      idProperty="Id"
                                      textProperty="Surname"
                                      secondaryTextProperty="Name"
                                      onFilterChanged={this.handleFilterChanged}
                                      onSelectionChanged={this.handleSelectionChanged}
                        />
                    </div>
                    <div className="col-auto m-2">
                        <Table columns={columns} sortColumn={sortColumn} onSort={this.handleSort}
                               data={filteredAttempts} idProperty="Id"/>
                    </div>
                </div>
            </Paper>
        );
    }

    handleFilterChanged = (event: ChangeEvent<HTMLInputElement>) => {
        const filter = event.target.value;
        const filteredStudents = this.state.students.filter(u => (u.Name + " " + u.Surname).includes(filter));
        this.setState({filteredStudents})
    };

    handleSelectionChanged = (id: string) => {
        if (!(id === "noFilter")) {
            const selectedStudent = this.state.filteredStudents.filter(u => u.Id === id)[0];
            const filteredAttempts = this.state.attempts.filter(
                a => a.StudentName.includes(selectedStudent.Name)
                    && a.StudentName.includes(selectedStudent.Surname)
            );
            this.setState({filteredAttempts});
        } else {
            const filteredAttempts = [...this.state.attempts];
            this.setState({filteredAttempts});
        }
    }

    handleSort = (sortColumn: SortColumn) => {
        this.setState({sortColumn});
        const attempts = [...this.state.filteredAttempts];
        const sorted = _.orderBy(attempts, [sortColumn.path], [sortColumn.order]);
        this.setState({filteredAttempts: sorted});
    };
}

export default StudentTestResults;