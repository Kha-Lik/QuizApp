import {AttemptViewModel, Test, Topic, User} from "../appTypes";
import attemptService from "./fakeAttemptService";
import topicService from "./fakeTopicService";
import questionService from "./fakeQuestionService";

function getTestResultsForStudent(student: User, topicId: string): Array<AttemptViewModel> {
    const {Name: topicName} = topicService.getTopicById(topicId) as Topic;
    return attemptService.getAttemptsByStudentId(student.Id).filter(a => a.TopicName === topicName);
}

function generateTestForTopic(student: User, topicId: string): Test {
    const topic = topicService.getTopicById(topicId) as Topic;
    const questions = questionService.getQuestionsByTopicid(topicId);
    questions.forEach(q => q.Answers.forEach(a => a.IsCorrect = false));

    return {
        Student: student,
        Topic: topic,
        DateTimePassed: new Date(Date.now()),
        Questions: questions,
        Score: -1
    }
}

function submitTest(test: Test): void {
    test.DateTimePassed = new Date(Date.now());
    console.log(test);
}

const testService = {
    getTestResultsForStudent,
    generateTestForTopic,
    submitTest
}

export default testService;