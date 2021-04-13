A large River Troll stands in your way.
What will you do?
-> start_story
===start_story
* [Push him] You push him.
        He does not react... ->start_story
* [Wave at him] You wave at him to get his attention.
        He doesn't seem to notice you... ->start_story
* [So what is your deal?] You ask: "Hey what's your deal?"
        He responds: "Only those who are worthy may pass here!"

        * * [So am I worthy?]You respond: "Okay... So am I worthy?"

        He responds: "Answer three to pass thee"


        * * * [What must I answer?] You ask: "Okay, so what must I answer"

        ->question1

===incorrect
He responds: "Incorrect, you must start from the beggining"
+ Try again
->question1
+ Leave
- -> END

===question1
 He asks: "What is the chemical formula for water?"

        + [O2H] You answer: "O2H"
                ->incorrect
        + [CO2] You answer: "CO2"
                 ->incorrect
        + [H2O] You answer: "H2O"
                He responds: "Correct!! Water is made of two Hydrogens and one Oxygen!"
                + + [Continue]
                ->question2
        + [OH] You answer: "OH"
                 ->incorrect


===question2
He asks: "About what percentage of the Earth's surface is made up of water?"

        +[10%] You answer: "10%"
                ->incorrect
        +[25%] You answer: "25%"
                ->incorrect
        +[50%] You answer: "50%"
                ->incorrect
        +[70%] You answer: "70%"
                He responds: "Correct!! 71% of the Earth's surface is water!"
                + + [Continue]
                ->question3

                
===question3
He asks: "What is the boiling point of water in standard conditions?"


        +[100 degrees C] You answer: "100 degrees C"
                 He responds: "Correct!! The boiling point of water is 100 degrees C!"
                 + + [Continue]
                 ->finished
        +[212 degrees F] You answer: "212 degrees F"
                He responds: "Correct!! The boiling point of water is 212 degrees F!"
                 + + [Continue]
                 ->finished
        +[373K] You answer: "373K"
                He responds: "Correct!! The boiling point of water is 373 Kelvin!"
                 + + [Continue]
                 ->finished


===finished
He responds: "Congratulations! You have gotten all the questions right! You may pass my bridge!"
        *Leave
placeholder #AllCorrect
- ->END