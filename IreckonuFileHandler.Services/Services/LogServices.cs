using System;
using System.Collections.Generic;
using System.Text;
using IreckonuFileHandler.Core.Services;
using NLog;
namespace IreckonuFileHandler.Services.Services
{
    public class LogServices : ILogServices
    {

        private static Logger logger;
        public LogServices()
        {
            logger = LogManager.GetCurrentClassLogger();
        }
        public void LogException(Exception ex, string Operation)
        {

            logger.Error(ex, " Error Source  ::: " + Operation + " ::: " + ex.Message);

        }

        public void LogException(string ExceptionMessage, string Operation)
        {
            logger.Error(" Error Source ::: " + Operation + " ::: " + ExceptionMessage);
        }

        public void LogInformation(string Message, string Operation)
        {

            logger.Info(" Information Source :::  " + Operation + " ::: " + Message);


        }
    }
}
