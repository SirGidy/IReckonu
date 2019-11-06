using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IreckonuFileHandler.Core.Services
{
    public interface ILogServices
    {
        void LogException(Exception ex, string Operation);

        void LogException(string ExceptionMessage, string Operation);

        void LogInformation(string Message, string Operation);
    }
}
