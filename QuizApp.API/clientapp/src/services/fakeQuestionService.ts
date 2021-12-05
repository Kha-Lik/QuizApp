import {Question} from '../appTypes';
import AnswerModel from '../models/answer';

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
        Answers: [
            new AnswerModel("ans_1", "1", true, "3"),
            new AnswerModel("ans_2", "2", false, "3"),
            new AnswerModel("ans_3", "3", false, "3"),
        ]
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
        Answers: [
            new AnswerModel("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor" +
                " incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco " +
                "laboris nisi ut aliquip ex ea commodo consequat.",
                "4", false, "8"),
            new AnswerModel("ans_5", "5", true, "8"),
            new AnswerModel("ans_6", "6", false, "8"),
        ]
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