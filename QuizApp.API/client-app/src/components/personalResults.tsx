import React, {ChangeEvent, Component} from 'react';
import {Paper} from '@material-ui/core';
import FilteredList from "./common/filteredList";
import {AttemptViewModel, Subject} from '../appTypes';
import Table from "./common/table";
import {Column, SortColumn} from "./common/commonTypes";
import attemptService from "../services/fakeAttemptService";
import _ from "lodash";
import auth from "../services/authService";
import subjectService from "../services/fakeSubjectService";

export interface PersonalResultsState {
    subjects: Subject[];
    filteredSubjects: Subject[];
    attempts: AttemptViewModel[];
    filteredAttempts: Array<AttemptViewModel>;
    columns: Array<Column>;
    sortColumn: SortColumn;
}

export interface PersonalResultsProps {
}

class PersonalResults extends Component<PersonalResultsProps, PersonalResultsState> {
    state: PersonalResultsState;

    constructor(props: PersonalResultsProps) {
        super(props);
        const subjects = subjectService.getAllSubjects();
        const attempts = attemptService.getAttemptsByStudentId(auth.getCurrentUser().NameIdentifier);
        const columns: Column[] = [
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
        const sortColumn: SortColumn = {path: "SubjectName", order: "asc"};
        this.state = {
            subjects,
            filteredSubjects: [...subjects],
            attempts,
            filteredAttempts: [...attempts],
            columns,
            sortColumn
        };
    }

    render() {
        const {filteredSubjects, columns, sortColumn, filteredAttempts} = this.state;

        return (
            <Paper variant="outlined" elevation={3} className="m-2">
                <div className="row m-2">
                    <div className="col-3 m-2">
                        <FilteredList data={filteredSubjects}
                                      idProperty="Id"
                                      textProperty="Name"
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
        const filteredSubjects = this.state.subjects.filter(u => (u.Name).includes(filter));
        this.setState({filteredSubjects})
    };

    handleSelectionChanged = (id: string) => {
        if (!(id === "noFilter")) {
            const selectedSubject = this.state.filteredSubjects.filter(u => u.Id === id)[0];
            const filteredAttempts = this.state.attempts.filter(
                a => a.SubjectName.includes(selectedSubject.Name)
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

export default PersonalResults;