using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lab1.Helpers;
using Lab1.Model.FakeRepository.Abstract;

namespace Lab1.Model
{
    /// <summary>
    /// Input Parser ansvarar för att tolka och utföra de kommandon användaren matar in
    /// </summary>
    public class InputParser
    {
        /// <summary>
        /// ParserState används för att hålla reda på vilket tillstånd InputParser-objektet
        /// befinner sig i. 
        /// 
        /// Anledningen till att vi har olika tillstånd är för att veta vilka kommandon som skall finnas 
        /// tillgängliga för användaren.
        /// 
        /// Ett tillstånd skulle kunna vara att användaren har listat Users och därmed har tillgång
        /// till kommandon för att lista detaljer för en User. Ett annat tillstånd skulle kunna vara 
        /// att användaren håller på och lägger in en ny User och därmed har tillgång till kommandon
        /// för att sätta namn, etc, för användaren.
        /// 
        /// Som koden ser ut nu så finns endast två tillgängliga tillstånd, 1, som är Default State.
        /// Och -1 som är det tillståndet som InputParser går in i när programmet skall avslutas
        /// Ifall nya tillstånd implementeras skulle de kunna vara 2, 3, 4, etc.
        /// </summary>
///        private en ParserState { get; set; }

        /// <summary>
        /// Sätter ParserState till Default
        /// </summary>
        /// 
        private State ParserState;

        private enum State
        {
            Exit,
            Default
        }
        /// <summary>
        /// Initierar log över inskrivna kommandon
        /// </summary>
        private Logger LogList = new Logger();

        private Model.FakeRepository.Abstract.IRepository Repo;

        public InputParser(Model.FakeRepository.Abstract.IRepository Repo)
        {
            this.Repo = Repo;
            ParserState = State.Default;
        }

        /// <summary>
        /// Skapar en lista av objekt från klassen User
        /// </summary>
        /// <param name="Users"></param>
        private string ListofUsers(List<User> Users, int ListLimit)
        {
            var userList = new List<string>();
            foreach (User user in Users.Take(ListLimit).ToArray())
            {
                userList.Add(user.ToString());
            }
            return string.Join("\n", userList);
        }

        private State DefaultParserState
        {
            get
            {
                return State.Default;
            }
        }

        /// <summary>
        /// Sätter ParserState till Exit
        /// </summary>
       private void SetExitParserState()
        {
            ParserState = State.Exit;
        }

       private State ExitParserState
       {
           get
           {
               return State.Exit;
           }
       }
        /// <summary>
        /// Returnerar true om ParserState är Exit (eller rättare sagt -1)
        /// </summary>
        public bool IsStateExit
        {
            get
            {
                return ParserState == ExitParserState;
            }
        }

        /// <summary>
        /// Tolka input baserat på vilket tillstånd (ParserState) InputParser-objektet befinner sig i.
        /// </summary>
        /// <param name="input">Input sträng som kommer från användaren.</param>
        /// <returns></returns>

        public string ParseInput(string input)
        {
            
            LogList.Log(input);
            if (ParserState == DefaultParserState)
            {
                return ParseDefaultStateInput(input);
            }
            else if (ParserState == ExitParserState)
            {
                // Do nothing - program should exit
                return "";
            }
            else
            {
                return OutputHelper.ErrorLostState;
            }
        }         

        public string DictionaryInterfaces()
        {
            Console.WriteLine("\r\nInterfaces implemented by Dictionary:\r\n");
  
            foreach (Type tinterface in typeof(Dictionary<int, string>).GetInterfaces())
            {
              Console.WriteLine(tinterface.ToString());
            }
            return ToString();
        }

        /// <summary>
        /// Tolka och utför de kommandon som ges när InputParser är i Default State
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string ParseDefaultStateInput(string input)
        {
            input = input.ToLower();
            string result;
            switch (input)
            {
                case "?": // Inget break; eller return; => ramlar igenom till nästa case (dvs. ?/help hanteras likadant)
                case "help":
                    result = OutputHelper.RootCommandList;
                    break;
                case "list":
                    result = ListofUsers(Repo.GetUsers(), 10);
                    break;
                case "sortedlist":
                    result = ListofUsers(Repo.GetUsers().OrderBy(u => u.FullName).ToList(), 10);
                    break;
                case "listadmin":
                    result = ListofUsers(Repo.GetUsers().Where(u => u.Type == User.UserType.Admin).ToList(), 10);
                    break;
                case "log":
                    result = LogList.ToString();
                    break;
                case "interface":
                    result = "To make a class implement an interface you need to add the name of the interface after the name of the class using : as separator";
                    result += "\n\tExample below";
                    result += "\n\t    class NameofClass : NameofInterface";
                    result += "\n\t    {";
                    result += "\n\t        related code";
                    result += "\n\t     }";
                    break;
                case "dictionary":
                    result = DictionaryInterfaces();
                   break;
                case "exit":
                    ParserState = State.Exit;
                    result = OutputHelper.ExitMessage("Bye!"); // Det går bra att skicka parametrar
                    break;
                default:
                    result = OutputHelper.ErrorInvalidInput;
                    break;
            }
            return result + OutputHelper.EnterCommand;
        }
    }
}
