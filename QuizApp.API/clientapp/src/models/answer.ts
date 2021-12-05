import {Answer} from "../appTypes";

export default class AnswerModel implements Answer {
    AnswerText: string;
    Id: string;
    IsCorrect: boolean;
    QuestionId: string;

    constructor(answerText: string,
                id: string,
                isCorrect: boolean,
                questionId: string) {
        this.Id = id;
        this.AnswerText = answerText;
        this.IsCorrect = isCorrect;
        this.QuestionId = questionId;
    }
}