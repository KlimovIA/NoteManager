namespace NoteManager.CommonTypes.Data.Debug
{
    internal static class Logger
    {
        /// <summary>
        /// Логгирование ошибок.
        /// </summary>
        /// <param name="className"> Наименование класса, вызывающего логгер. </param>
        /// <param name="Message"> Описание ошибки </param>
        public static async void WriteLogMessage(string className, string Message)
        {
            string formatedLogMessage = $"[{DateTime.Now}] >> ClassName: {className}; Message: {Message}";
            await Task.Run(() =>
            {
                using (StreamWriter writer = new StreamWriter("Log.txt", true))
                {
                    writer.WriteLine(formatedLogMessage);
                }
            });
        }
    }
}
