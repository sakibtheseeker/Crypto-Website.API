using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Logging
{
    public static class AppLogger
    {
        public static async Task Log(
            string message,
            LogLevelType level)
        {
            var logDir = Path.Combine(
                Directory.GetCurrentDirectory(), "Logs");

            if (!Directory.Exists(logDir))
                Directory.CreateDirectory(logDir);

            var logPath = Path.Combine(
                logDir, $"log-{DateTime.Now:yyyy-MM-dd}.txt");

            var log = $"""
                
                Level   : {level}
                Time    : {DateTime.Now:yyyy-MM-dd HH:mm:ss} 
                Message : {message}
                

                """;

            await File.AppendAllTextAsync(logPath, log);
        }


    }
}
