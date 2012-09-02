using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1.Model
{
    class Logger
    {
        private List<string> LogList;
        private int ListLimit = 10;
        

        public Logger()
        {
            LogList = new List<string>();
        }
        
        public void Log(string msg)
        {
            LogList.Add(msg);
            LogListLimit();

        }
        private void LogListLimit() 
        {
            while (LogList.Count > ListLimit)
            {
                LogList.RemoveAt(0);
            }
        }

        public override string ToString()
        {
            return string.Join("\n", LogList.ToArray());
        }
    }
}