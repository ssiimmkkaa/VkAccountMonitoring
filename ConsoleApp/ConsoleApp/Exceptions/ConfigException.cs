using System;

namespace ConsoleApp.Exceptions
{
    class ConfigException : Exception
    {
        private string _config;

        public ConfigException(string config)
        {
            _config = config;
        }

        public override string Message => $"Неверное описание атрибута конфига {_config}";

        public override string ToString()
        {
            return Message;
        }
    }
}