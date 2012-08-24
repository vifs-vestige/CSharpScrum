using System;
using System.Collections.Generic;
using System.Text;
using FileWriter.Handles;
using FileWriter.Objects;

namespace FileWriter
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            MainClass mainClass = new MainClass();
            mainClass.Run();
        }

        bool _isRunning;
        static ConsoleColor _questionColor = ConsoleColor.White;
        static ConsoleColor _errorColor = ConsoleColor.Red;
        static List<Action<IFileHandle>> _menu;
        static List<string> _actions;
        List<string> Actions
        {
            get
            {
                if (_actions == null || (_actions != null && _actions.Count <= 0))
                {
                    _actions = new List<string>();
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < _menu.Count; i += 1)
                    {
                        _actions.Add(_menu[i].Method.Name);
                    }
                }
                return _actions;
            }
        }

        public MainClass()
        {
            _menu = new List<Action<IFileHandle>>()
            {
                {Create},
                {Update},
                {Delete},
                {Exit},
            };
        }

        public void Run()
        {
            _isRunning = true;
            var menu = Actions;
            var openPrompt = GetPrompt(MessageDictionary.Prompt.Open);
            var openError = GetErr(MessageDictionary.Error.Invalid);
            var prompt = GetPrompt(MessageDictionary.Prompt.WhatToDo);
            var errorMessage = GetErr(MessageDictionary.Error.NotAChoice);
            while (_isRunning)
            {
                string path = AskFor(openPrompt, openError, _questionColor, _errorColor);
                IFileHandle fileHandle = FileHandle.GetInstance(path);
                while (_isRunning)
                {
                    int choice = DisplayChoices<string>(prompt, errorMessage, menu, _questionColor, _errorColor);
                    _menu[choice].Invoke(fileHandle);
                    (GetPrompt(MessageDictionary.Prompt.Done)).PrntLine(_questionColor);
                }
            }
        }

        void Create(IFileHandle fileHandle)
        {
            var invalidInputError = GetErr(MessageDictionary.Error.Invalid);
            string firstName = AskFor(GetPrompt(MessageDictionary.Prompt.Firstname), invalidInputError, _questionColor, _errorColor);
            string lastName = AskFor(GetPrompt(MessageDictionary.Prompt.Lastname), invalidInputError, _questionColor, _errorColor);
            string email = AskFor(GetPrompt(MessageDictionary.Prompt.Email), invalidInputError, _questionColor, _errorColor);
            IInstanceAssembler personInstance = Person.GetInstance(firstName + "," + lastName + "," + email);
            fileHandle.Create(personInstance.ToString());
        }

        string AskFor(string promptMessage, string errorMessage, ConsoleColor promptColor = ConsoleColor.Gray, ConsoleColor errorColor = ConsoleColor.Gray)
        {
            string value = null;
            int count = 0;
            do
            {
                if (count >= 1)
                {
                    (errorMessage).PrntLine(errorColor);
                    count = 0;
                }
                value = (promptMessage).ReadLn(promptColor);
                count += 1;
            } while (String.IsNullOrWhiteSpace(value.Trim()));
            return value;
        }

        void Delete(IFileHandle fileHandle)
        {
            var values = fileHandle.Read();
            if (values.Count > 0)
            {
                int choice = DisplayChoices<string>(GetPrompt(MessageDictionary.Prompt.Delete), GetErr(MessageDictionary.Error.NotAChoice), values, _questionColor, _errorColor);
                values.RemoveAt(choice);
                fileHandle.Delete(values);
            }
            else
            {
                (GetErr(MessageDictionary.Error.EmptyFile)).PrntLine(_errorColor);
            }
        }

        void Update(IFileHandle fileHandle)
        {
            var values = fileHandle.Read();
            if (values.Count > 0)
            {
                int choice = DisplayChoices<string>(GetPrompt(MessageDictionary.Prompt.Update), GetErr(MessageDictionary.Error.NotAChoice), values, promptColor: _questionColor, errorColor: _errorColor);
                var valueChoices = values[choice];
                var person = Person.GetInstance(valueChoices);
                var properties = person.GetCurrentValues();
                int propertyChoice = DisplayChoices<string>(GetPrompt(MessageDictionary.Prompt.Property), GetErr(MessageDictionary.Error.NotAChoice), properties, _questionColor, _errorColor);
                var currentValue = properties[propertyChoice];
                var newValue = (AskFor(GetPrompt(MessageDictionary.Prompt.Value), GetErr(MessageDictionary.Error.Invalid),_questionColor, _errorColor));
                person.UpdateValue(propertyChoice, newValue);
                values[choice] = person.ToString();
                fileHandle.Update(values);
            }
            else
            {
                (GetErr(MessageDictionary.Error.EmptyFile)).PrntLine(_errorColor);
            }
        }

        void Open(IFileHandle fileHandle)
        {
            string path = (GetPrompt(MessageDictionary.Prompt.Open)).ReadLn(_questionColor);
            fileHandle.Open(path);
        }

        void Exit(IFileHandle fileHandle)
        {
            fileHandle.Dispose();
            _isRunning = false;
        }

        int DisplayChoices<T>(string promptMessage, string errorMessage, List<T> choices, ConsoleColor promptColor = ConsoleColor.Gray, ConsoleColor errorColor = ConsoleColor.Gray, string choiceFormat = "{0}) {1}", bool subtractOne = true)
        {
            int choice = -1;
            if (choices.Count > 0)
            {
                int count = 0;
                if (choiceFormat == null)
                {
                    choiceFormat = "{0}) {1}";
                }
                int subtractionValue = 0;
                if (subtractOne)
                {
                    subtractionValue = 1;
                }
                int numOfChoices = choices.Count + subtractionValue;
                do
                {
                    if (count >= 1)
                    {
                        (errorMessage).PrntLine(errorColor);
                        count = 0;
                    }
                    (promptMessage).PrntLine(promptColor);
                    for (int i = subtractionValue; i < numOfChoices; i += 1)
                    {
                        (String.Format(choiceFormat, i, choices[i - subtractionValue])).PrntLine(promptColor);
                    }
                    try
                    {
                        choice = Int32.Parse(Console.ReadLine());
                    }
                    catch
                    {
                    }
                    count += 1;
                } while (choice <= 0 || choice >= numOfChoices);
                choice = choice - subtractionValue;
            }
            return choice;
        }

        string GetPrompt(MessageDictionary.Prompt prompt)
        {
            return MessageDictionary.GetPrompt(prompt);
        }

        string GetErr(MessageDictionary.Error error)
        {
            return MessageDictionary.GetErr(error);
        }
    }

    static class ConsoleHelper
    {
        public static string ReadLn(this string toPrint, ConsoleColor foreColor = ConsoleColor.Gray)
        {
            toPrint.PrntLine(foreColor);
            string response = Console.ReadLine();
            return response;
        }

        public static void PrntLine(this string text, ConsoleColor foreColor = ConsoleColor.Gray)
        {
            ForeColor(foreColor);
            Console.WriteLine(text);
            Reset();
        }

        public static void ForeColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        public static void Reset()
        {
            Console.ResetColor();
        }
    }
}
