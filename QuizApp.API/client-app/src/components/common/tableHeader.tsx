import React, {Component} from "react";
import {Column, SortColumn} from "./commonTypes";

export interface TableHeaderProps {
    columns: Array<Column>;
    sortColumn: SortColumn;
    onSort: Function;
}

class TableHeader extends Component<TableHeaderProps> {
    raiseSort = (path: string) => {
        const sortColumn = {...this.props.sortColumn};
        if (sortColumn.path === path)
            sortColumn.order = sortColumn.order === "asc" ? "desc" : "asc";
        else {
            sortColumn.path = path;
            sortColumn.order = "asc";
        }
        this.props.onSort(sortColumn);
    };

    renderSortIcon = (column: Column) => {
        const {sortColumn} = this.props;

        if (column.path !== sortColumn.path) return null;
        if (sortColumn.order === "asc") return <i className="fa fa-sort-up"/>;
        return <i className="fa fa-sort-down"/>;
    };

    render() {
        return (
            <thead>
            <tr>
                {this.props.columns.map(column => (
                    <th
                        className="clickable"
                        key={column.path || column.key}
                        onClick={() => this.raiseSort(column.path)}
                    >
                        {column.label} {this.renderSortIcon(column)}
                    </th>
                ))}
            </tr>
            </thead>
        );
    }
}

export default TableHeader;
