import React from 'react';
import {fade, createStyles, makeStyles, Theme} from '@material-ui/core/styles';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import {InputBase, List} from '@material-ui/core';

export interface FilteredListProps {
    data: Array<any>;
    idProperty: string;
    textProperty: string;
    secondaryTextProperty?: string;
    onFilterChanged: Function;
    onSelectionChanged: Function;
}

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        root: {
            width: '100%',
            height: 400,
            maxWidth: 300,
            backgroundColor: theme.palette.background.paper,
        },
        search: {
            position: 'relative',
            borderRadius: theme.shape.borderRadius,
            backgroundColor: fade(theme.palette.common.black, 0.15),
            '&:hover': {
                backgroundColor: fade(theme.palette.common.black, 0.25),
            },
            width: '100%',
        },
        inputRoot: {
            color: 'inherit',
        },
        inputInput: {
            padding: theme.spacing(1, 1, 1, 0),
            // vertical padding + font size from searchIcon
            paddingLeft: "5%",
            transition: theme.transitions.create('width'),
            width: '100%',
            [theme.breakpoints.up('md')]: {
                width: '20ch',
            },
        },
    }),
);

export default function FilteredList(props: FilteredListProps) {
    const {data, idProperty, textProperty, secondaryTextProperty, onFilterChanged, onSelectionChanged} = props;

    const classes = useStyles();
    const [selectedIndex, setSelectedIndex] = React.useState("");

    const handleListItemClick = (
        event: React.MouseEvent<HTMLDivElement, MouseEvent>,
        index: string,
    ) => {
        setSelectedIndex(index);
        onSelectionChanged(index);
    };

    return (
        <div className={classes.root}>
            <List>
                <div className={classes.search}>
                    <InputBase
                        placeholder="Search…"
                        classes={{
                            root: classes.inputRoot,
                            input: classes.inputInput,
                        }}
                        inputProps={{ 'aria-label': 'search' }}
                        onChange={(event) => onFilterChanged(event)}
                    />
                </div>
                <ListItem button onClick={(event) =>
                              handleListItemClick(event, "noFilter")}>
                    <ListItemText
                        className="btn btn-secondary"
                        primary="Скинути вибір"/>
                </ListItem>
                {data.map(item =>
                    <ListItem button selected={selectedIndex === item[`${idProperty}`]}
                              onClick={(event) =>
                                  handleListItemClick(event, item[`${idProperty}`])}>
                        <ListItemText
                            primary={item[`${textProperty}`]+(secondaryTextProperty && " "+item[`${secondaryTextProperty}`])}/>
                    </ListItem>)}
            </List>
        </div>
    );
}