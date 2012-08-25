using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileWriter.Objects
{
    sealed class MessageDictionary
    {
        [Flags]
        public enum Error
        {
            EmptyFile = 0,
            Invalid = 1 << 1,
            NotAChoice = 1 << 2,
        }

        static Dictionary<Error, string> _errorDictionary;

        static Dictionary<Error, string> Errors
        {
            get
            {
                if (_errorDictionary == null)
                {
                    _errorDictionary = new Dictionary<Error, string>()
                    {
                        {Error.EmptyFile, "There is nothing in the file"},
                        {Error.Invalid, "That isn't correct"},
                        {Error.NotAChoice, "That isn't one of the choices"},
                    };
                }
                return _errorDictionary;
            }
        }

        [Flags]
        public enum Prompt
        {
            Firstname = 0,
            Lastname = 1 << 1,
            Email = 1 << 2,
            WhatToDo = 1 << 3,
            Update = 1 << 4,
            Delete = 1 << 5,
            Open = 1 << 6,
            Done = 1 << 7,
            Property = 1 << 8,
            Value = 1 << 9,
        }

        static Dictionary<Prompt, string> _promptDictionary;

        static Dictionary<Prompt, string> Prompts
        {
            get
            {
                if (_promptDictionary == null)
                {
                    _promptDictionary = new Dictionary<Prompt, string>()
                    {
                        {Prompt.Firstname, "What is the firstname of the person you are adding?"},
                        {Prompt.Lastname, "What is the lastname of the person you are adding?"},
                        {Prompt.Email, "What is the email of the person you are adding?"},
                        {Prompt.WhatToDo, "What would you like to do with the file?"},
                        {Prompt.Update, "Which entry would you like to update?"},
                        {Prompt.Delete, "Which entry are you deleting?"},
                        {Prompt.Open, "What is the path to the file you want to open? Ex: C:\\1.txt"},
                        {Prompt.Done, "Done! What else would you like to do?"},
                        {Prompt.Property, "Which property value are you changing?"},
                        {Prompt.Value, "What will the value be?"},
                    };
                }
                return _promptDictionary;
            }
        }

        public static string GetErr(Error errorType)
        {
            string message = null;
            if (Errors.ContainsKey(errorType))
            {
                message = Errors[errorType];
            }
            return message;
        }

        public static string GetPrompt(Prompt promptType)
        {
            string message = null;
            if (Prompts.ContainsKey(promptType))
            {
                message = Prompts[promptType];
            }
            return message;
        }
    }
}
