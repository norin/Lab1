using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lab1.Helpers;
using Lab1.Model;
using Lab1.Model.FakeRepository.Abstract;
using Lab1.Model.FakeRepository;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;

            string input;

            Repository Repo = new Repository();
            
            InputParser inputParser = new InputParser(Repo);
           
            string parseResult;
            /// inputParser.SetDefaultParserState(); // Se Beskrivning av ParserState i InputParser-klassen.
           
            OutputHelper.Put(OutputHelper.GreetingMessage);
            
            while (!exit)
            {
                // Hämta input från användaren
                input = InputHelper.GetUserInput();

                // Tolka Användarens input och tilldela resultatet av tolkningen till parseResult.
                parseResult = inputParser.ParseInput(input);

                // Skriv ut resultatet från tolkningen
                OutputHelper.Put(parseResult);

                // Avsluta programmet om inputParser är i tillståndet "Exit"
                if (inputParser.IsStateExit)
                    exit = true;
            }
        }
    }
}
