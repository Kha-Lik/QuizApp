import {Topic} from "../appTypes";


const topics: Topic[] = [
    {
        Id: "1",
        SubjectId: "1",
        Name: "Тема_1",
        TopicNumber: 1,
        TimeToPass: 10,
        QuestionsPerAttempt: 10,
        MaxAttemptCount: 2
    },
    {
        Id: "2",
        SubjectId: "2",
        Name: "Тема_2",
        TopicNumber: 1,
        TimeToPass: 10,
        QuestionsPerAttempt: 10,
        MaxAttemptCount: 2
    },
    {
        Id: "3",
        SubjectId: "3",
        Name: "Тема_3",
        TopicNumber: 1,
        TimeToPass: 10,
        QuestionsPerAttempt: 10,
        MaxAttemptCount: 2
    },
    {
        Id: "4",
        SubjectId: "4",
        Name: "Тема_4",
        TopicNumber: 1,
        TimeToPass: 10,
        QuestionsPerAttempt: 10,
        MaxAttemptCount: 2
    },
    {
        Id: "5",
        SubjectId: "5",
        Name: "Тема_5",
        TopicNumber: 1,
        TimeToPass: 10,
        QuestionsPerAttempt: 10,
        MaxAttemptCount: 2
    },
    {
        Id: "6",
        SubjectId: "6",
        Name: "Тема_6",
        TopicNumber: 1,
        TimeToPass: 10,
        QuestionsPerAttempt: 10,
        MaxAttemptCount: 2
    },
    {
        Id: "7",
        SubjectId: "7",
        Name: "Тема_7",
        TopicNumber: 1,
        TimeToPass: 10,
        QuestionsPerAttempt: 10,
        MaxAttemptCount: 2
    }
]

function getAllTopics(): Topic[] {
    return topics;
}

function getTopicsBySubjectId(id: string): Topic[] {
    return topics.filter(t => t.SubjectId === id);
}

function getTopicById(id : string) :Topic | undefined{
    return topics.find(t => t.Id === id);
}

const topicService = {
    getAllTopics,
    getTopicsBySubjectId,
    getTopicById
}

export default topicService;