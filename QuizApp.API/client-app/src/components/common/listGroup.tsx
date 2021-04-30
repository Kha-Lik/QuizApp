import React from "react";
import {
  Button,
  IconButton,
  List,
  ListItem,
  ListItemSecondaryAction,
  ListItemText,
} from "@material-ui/core";
import AddIcon from "@material-ui/icons/Add";
import DeleteIcon from "@material-ui/icons/Delete";

export interface ListGroupProps {
  data: any[];
  contentProperty: string;
  idProperty: string;
  createButton: boolean;
  deleteButton: boolean;
  onDelete?: Function;
  onCreate?: Function;
  createButtonText?: string;
  onSelectionChanged?: Function;
}

export interface ListGroupState {}

class ListGroup extends React.Component<ListGroupProps, ListGroupState> {
  state = { value: "" };

  render() {
    const {
      data,
      contentProperty,
      idProperty,
      createButton,
      deleteButton,
      onCreate,
      onDelete,
      createButtonText,
      onSelectionChanged,
    } = this.props;

    return (
      <List>
        {data.map((item) => (
          <ListItem
            key={item[idProperty]}
            button
            divider
            onClick={() => {
              this.setState({ value: item[idProperty] });
              onSelectionChanged && onSelectionChanged(item[idProperty]);
            }}
            selected={this.state.value === item[idProperty] ? true : false}
          >
            <ListItemText primary={item[contentProperty]} />
            {deleteButton &&
              this.renderDeleteButton(onDelete, item[idProperty])}
          </ListItem>
        ))}
        {createButton && this.renderCreateButton(onCreate, createButtonText)}
      </List>
    );
  }

  renderCreateButton(handler: Function | undefined, text: string | undefined) {
    return (
      <ListItem>
        <Button
          variant="contained"
          color="primary"
          startIcon={<AddIcon />}
          onClick={() => handler && handler()}
          className="mx-auto"
        >
          {text ? text : "Додати"}
        </Button>
      </ListItem>
    );
  }

  renderDeleteButton(handler: Function | undefined, key: string) {
    return (
      <ListItemSecondaryAction onClick={() => handler && handler(key)}>
        <IconButton edge="end" aria-label="delete">
          <DeleteIcon color="error" />
        </IconButton>
      </ListItemSecondaryAction>
    );
  }
}

export default ListGroup;
