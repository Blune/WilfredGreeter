using System;
using System.IO;

namespace WilfredGreeter
{
    public class MessageLoader
    {
        private readonly string _messagePath;

        public MessageLoader(string messagePath)
        {
            _messagePath = messagePath;
        }

        public string LoadMessage()
        {
            string message;
            try
            {
                using (var fileStream = new FileStream(_messagePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (var reader = new StreamReader(fileStream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (Exception)
            {
                message = "Hi there!"; //default if file is not found to avoid crash!
            }

            return message;

        }
    }
}
