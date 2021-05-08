import React, { Component } from "react";
import _ from "lodash";
import {Column} from "./commonTypes";

export interface TableBodyProps {
    data: Array<any>;
    columns: Array<Column>;
    idProperty: string;
}

export interface TableBodyState {}

class TableBody extends Component<TableBodyProps, TableBodyState> {
    renderCell = (item : any, column : Column) => {
        if (column.content) return column.content(item);

        return _.get(item, column.path);
    };

    createKey = (item : any, column : Column) => {
        return _.get(item, this.props.idProperty) + (column.path || column.key);
    };

    render() {
        const { data, columns, idProperty } = this.props;
        return (
            <tbody>
            {data.map(item => (
                <tr key={_.get(item, idProperty)}>
                    {columns.map(column => (
                        <td key={this.createKey(item, column)}>
                            {this.renderCell(item, column)}
                        </td>
                    ))}
                </tr>
            ))}
            </tbody>
        );
    }
}

export default TableBody;
