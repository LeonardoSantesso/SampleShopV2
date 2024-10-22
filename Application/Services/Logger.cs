namespace Application.Services;

    public class Logger : ILogger
    {
        public Logger()
        {
        }

        public void WriteToLog(string log)
        {
            Console.Write(log);
        }
    }

