import React, {Fragment} from "react";
import {AppBar, Button, createStyles, makeStyles, Theme, Toolbar, Typography} from "@material-ui/core";
import {Link as RouterLink} from "react-router-dom";
import {grey, teal} from "@material-ui/core/colors"
import {JwtUser} from "../appTypes";

export interface NavBarProps {
    user: JwtUser;
}

const linkColor = grey[50];
const currentLinkColor = teal["A700"];

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        grow: {
            flexGrow: 1,
        },
        menuButton: {
            marginRight: theme.spacing(2),
        },
        title: {
            display: 'none',
            [theme.breakpoints.up('sm')]: {
                display: 'block',
            },
            width: "20%",
            textAlign: "center"
        },
        sectionDesktop: {
            display: 'none',
            [theme.breakpoints.up('md')]: {
                display: 'flex',
            },
        },
        sectionMobile: {
            display: 'flex',
            [theme.breakpoints.up('md')]: {
                display: 'none',
            },
        },
        linkItem: {
            color: linkColor
        },
        currentLink: {
            color: currentLinkColor
        }
    }),
);


function NavBar({user}: NavBarProps) {
    const {grow, title, linkItem, currentLink} = useStyles();
    const [index, setIndex] = React.useState(0);

    const handleClick = (event: React.MouseEvent<HTMLAnchorElement, MouseEvent>,
                         index: number) => {
        setIndex(index);
    }

    const renderLecturerMenu = () => (
        <Fragment>
            <Button
                onClick={(event: React.MouseEvent<HTMLAnchorElement, MouseEvent>) => handleClick(event, 1)}
                component={RouterLink}
                to="/testCreation"
            >
                <Typography variant="h6" className={index === 1 ? currentLink : linkItem}>
                    Створення тестів
                </Typography>
            </Button>
            <Button
                onClick={(event: React.MouseEvent<HTMLAnchorElement, MouseEvent>) => handleClick(event, 2)}
                component={RouterLink}
                to="/studentsResults"
            >
                <Typography variant="h6" className={index === 2 ? currentLink : linkItem}>
                    Результати студентів
                </Typography>
            </Button>
        </Fragment>
    )

    const renderStudentMenu = () => (
        <Fragment>
            <Button
                onClick={(event: React.MouseEvent<HTMLAnchorElement, MouseEvent>) => handleClick(event, 3)}
                component={RouterLink}
                to="/tests"
            >
                <Typography variant="h6" className={index === 3 ? currentLink : linkItem}>
                    Тести
                </Typography>
            </Button>
            <Button
                onClick={(event: React.MouseEvent<HTMLAnchorElement, MouseEvent>) => handleClick(event, 4)}
                component={RouterLink}
                to="/results"
            >
                <Typography variant="h6" className={index === 4 ? currentLink : linkItem}>
                    Результати
                </Typography>
            </Button>
        </Fragment>
    )
    
    const conditionalRender = () => { 
        if(user.Role === "Lecturer")    
            return  renderLecturerMenu()
        else 
            return  renderStudentMenu()
    };
    
    return (
        <AppBar position="sticky" color="primary">
            <Toolbar>
                <Typography className={title} variant="h4">QuizApp</Typography>
                {conditionalRender()}
                <div className={grow}/>
                <Button>
                    <Typography className={linkItem}>{user.Sub}</Typography>
                </Button>
            </Toolbar>
        </AppBar>
    );
}

export default NavBar;
