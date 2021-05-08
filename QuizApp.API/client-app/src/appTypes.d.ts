export interface Subject {
    Id: string,
    LecturerId: string,
    Name: string
}

export interface Topic {
    Id: string,
    SubjectId: string,
    Name: string,
    TopicNumber: number,
    TimeToPass: number,
    QuestionsPerAttempt: number,
    MaxAttemptCount: number
}

export interface Question {
    Id: string,
    TopicId: string,
    QuestionNumber: number,
    QuestionText: string,
    Answers: Answer[]
}

export interface Answer {
    Id: string,
    QuestionId: string,
    AnswerText: string,
    IsCorrect: boolean
}