using UnityEngine;

// Trieda pre ukladanie informacii o otazke. Obsahuje zakladne informacie: id otazky, text a moznosti otazky a index spravnej moznosti
// [4]
[System.Serializable]
public class Question {

    // Premenne
    [SerializeField]
    private int id;

    [SerializeField]
    private string question;

    [SerializeField]
    private string[] options;

    [SerializeField]
    private int correctOption;


    // Konstruktor triedy
    public Question(int id, string question, string[] options, int correctOption) {

        this.id = id;
        this.question = question;
        this.options = options;
        this.correctOption = correctOption;

    }


    // Getters, setters
    public void setId(int id) {
        this.id = id;
    }

    public int getId() {
        return id;
    }

    public void setQuestion(string question) {
        this.question = question;
    }

    public string getQuestion() {
        return question;
    }

    public void setOptions(string[] options) {
        this.options = options;
    }

    public string[] getOptions() {
        return options;
    }
    
    public void setCorrectOption(int correctOption) {
        this.correctOption = correctOption;
    }

    public int getCorrectOption() {
        return correctOption;
    }

}