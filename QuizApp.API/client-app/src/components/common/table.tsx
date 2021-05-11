import React from "react";
import TableHeader from "./tableHeader";
import TableBody from "./tableBody";
import {Column, SortColumn} from "./commonTypes";

export interface TableProps {
    columns: Array<Column>;
    sortColumn: SortColumn;
    onSort: Function;
    data: Array<any>;
    idProperty: string;
}

const Table = ({columns, sortColumn, onSort, data, idProperty}: TableProps) => {
    return (
        <table className="table">
            <TableHeader columns={columns} sortColumn={sortColumn} onSort={onSort}/>
            <TableBody columns={columns} data={data} idProperty={idProperty}/>
        </table>
    );
};

export default Table;