import {Subject} from "../appTypes";


const subjects: Subject[] = [
    {
        Id: "1",
        LecturerId: "1",
        Name: "Матан"
    },
    {
        Id: "2",
        LecturerId: "1",
        Name: "Лінал"
    },
    {
        Id: "3",
        LecturerId: "abc",
        Name: "ТРПЗ"
    },
    {
        Id: "4",
        LecturerId: "abc",
        Name: "Веб"
    },
    {
        Id: "5",
        LecturerId: "abc",
        Name: "ООП"
    },
    {
        Id: "6",
        LecturerId: "jkl",
        Name: "ОС"
    },
    {
        Id: "7",
        LecturerId: "jkl",
        Name: "ПСРЧ"
    }
]

function getAllSubjects(): Subject[] {
    return subjects;
}

function getSubjectsByLecturerId(id: string): Subject[] {
    return subjects.filter(s => s.LecturerId === id);
}

function getSubjectById(id: string): Subject | undefined {
    return subjects.find(s => s.Id === id);
}

const subjectService = {
    getAllSubjects,
    getSubjectsByLecturerId,
    getSubjectById
}

export default subjectService;