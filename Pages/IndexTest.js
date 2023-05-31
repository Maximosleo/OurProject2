export class Question {
    title;
    answer0;
    answer1;
    answer2;
    answer3;
    correctIndex;

    constructor(title1, answer0, answer1, answer2, answer3, correctIndex1) {
        this.title = title1;
        this.answer0 = answer0;
        this.answer1 = answer1;
        this.answer2 = answer2;
        this.answer3 = answer3;
        this.correctIndex = correctIndex1;
    }

}