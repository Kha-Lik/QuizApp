import {Question} from '../appTypes';

const questions: Question[] = [
    {
        Id: "1",
        TopicId: "1",
        QuestionNumber: 1,
        QuestionText: "Question_1",
        Answers: []
    },
    {
        Id: "2",
        TopicId: "2",
        QuestionNumber: 1,
        QuestionText: "Question_2",
        Answers: []
    },
    {
        Id: "3",
        TopicId: "3",
        QuestionNumber: 1,
        QuestionText: "Question_3",
        Answers: []
    },
    {
        Id: "4",
        TopicId: "4",
        QuestionNumber: 1,
        QuestionText: "Question_4",
        Answers: []
    },
    {
        Id: "5",
        TopicId: "5",
        QuestionNumber: 1,
        QuestionText: "Question_5",
        Answers: []
    },
    {
        Id: "6",
        TopicId: "6",
        QuestionNumber: 1,
        QuestionText: "Question_6",
        Answers: []
    },
    {
        Id: "7",
        TopicId: "7",
        QuestionNumber: 1,
        QuestionText: "Question_7",
        Answers: []
    },
    {
        Id: "8",
        TopicId: "3",
        QuestionNumber: 2,
        QuestionText: "Question_8",
        Answers: []
    }
]

function getAllQuestions(): Question[] {
    return questions;
}

function getQuestionsByTopicid(id: string): Question[] {
    return questions.filter((q) => q.TopicId === id);
}

const questionService = {
    getAllQuestions,
    getQuestionsByTopicid
}

export default questionService;