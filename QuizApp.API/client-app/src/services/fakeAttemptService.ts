import {Attempt, AttemptViewModel} from "../appTypes";
import userService from "./fakeuserService";
import topicService from "./fakeTopicService";
import subjectService from "./fakeSubjectService";

const attempts : Attempt[] = [
    {
        Id: "a",
        StudentId: "def",
        TopicId: "1",
        Score: 228,
        DateTime: new Date(2021, 5, 7, 12, 21)
    },
    {
        Id: "b",
        StudentId: "def",
        TopicId: "2",
        Score: 144,
        DateTime: new Date(2021, 5, 7, 12, 51)
    },
    {
        Id: "c",
        StudentId: "ghi",
        TopicId: "3",
        Score: 101,
        DateTime: new Date(2021, 5, 6, 13, 31)
    },
    {
        Id: "d",
        StudentId: "ghi",
        TopicId: "4",
        Score: 9,
        DateTime: new Date(2021, 5, 6, 13, 0)
    },
    {
        Id: "e",
        StudentId: "def",
        TopicId: "5",
        Score: 91,
        DateTime: new Date(2021, 5, 8, 14, 48)
    },
    {
        Id: "f",
        StudentId: "ghi",
        TopicId: "6",
        Score: 19,
        DateTime: new Date(2021, 5, 5, 2, 28)
    },
]

function getAllAttempts() : AttemptViewModel[]{
    return attempts.map(a => mapToViewModel(a));
}

function getAttemptsByStudentId(id : string) : AttemptViewModel[]{
    return attempts.filter(a => a.StudentId === id).map(a => mapToViewModel(a));
}

function mapToViewModel(attempt : Attempt) : AttemptViewModel{
    const attemptVM : AttemptViewModel = {} as AttemptViewModel;
    const student = userService.getStudentById(attempt.StudentId);
    const topic = topicService.getTopicById(attempt.TopicId);

    attemptVM.Id = attempt.Id;
    attemptVM.StudentName = student?.Name + " " + student?.Surname;
    attemptVM.TopicName = topic?.Name as string;
    attemptVM.SubjectName = subjectService.getSubjectById(topic?.SubjectId as string)?.Name as string;
    attemptVM.Score = attempt.Score;
    attemptVM.DateTime = attempt.DateTime.toLocaleString();

    return attemptVM;
}

const attemptService = {
    getAllAttempts,
    getAttemptsByStudentId
}

export default attemptService;