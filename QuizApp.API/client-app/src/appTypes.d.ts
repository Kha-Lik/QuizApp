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
    Answers: Array<Answer>
}

export interface Answer {
    Id: string,
    QuestionId: string,
    AnswerText: string,
    IsCorrect: boolean
}

export interface Attempt {
    Id: string;
    StudentId: string;
    TopicId: string;
    Score: number;
    DateTime: Date;
}

export interface AttemptViewModel {
    Id: string;
    StudentName: string;
    TopicName: string;
    SubjectName: string;
    Score: number;
    DateTime: string;
}

export interface JwtUser {
    Sub: string;
    Jti: string;
    NameIdentifier: string;
    Role: string;
}

export interface User {
    Id: string;
    Name: string;
    Surname: string;
    Role: string;
}

export interface Test {
    Student: User;
    Topic: Topic;
    DateTimePassed: Date;
    Score: number;
    Questions: Array<Question>;
}
