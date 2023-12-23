using System.Collections.Generic;
using UnityEngine;

// Trieda pre ukladanie otazok, ktore sa nasli v danom subore. Tato trieda je potrebna pre pristup k otazkam
// (Question class musí byť obalená v nejakej inej triede, ktorá bude obsahovať dané pole prvkov ) [5]
public class QuestionList {

    public List<Question> questions;

    void Questionlist(List<Question> questions) {
        this.questions = questions;
    }

}