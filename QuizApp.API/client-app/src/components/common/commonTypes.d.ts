export interface Column {
    path: string;
    label: string;
    content?: Function;
    key?: string;
}
export interface SortColumn{
    path: string;
    order: boolean | "asc" | "desc";
}