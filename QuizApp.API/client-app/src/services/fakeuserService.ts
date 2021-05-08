import { User } from "../appTypes";

const users : User[] = [
    {
        Id: "abc",
        Name: "Kolia",
        Surname: "Vaskevych",
        Role: "Lecturer",
    },
    {
        Id: "def",
        Name: "Pashka",
        Surname: "Pustik",
        Role: "Student",
    },
    {
        Id: "ghi",
        Name: "Oleh",
        Surname: "Petrenko",
        Role: "Student",
    },
]

function getStudents() : User[]{
    return users.filter(u => u.Role === "Student");
}

function getLecturers() : User[]{
    return users.filter(u => u.Role === "Lecturer");
}

function getStudentById(id : string) : User | undefined{
    return users.filter(u => u.Role === "Student").find(u => u.Id === id);
}
function getLecturerById(id : string) : User | undefined{
    return users.filter(u => u.Role === "Lecturer").find(u => u.Id === id);
}

const userService = {
    getStudents,
    getLecturers,
    getStudentById,
    getLecturerById
}

export default userService;