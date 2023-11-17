namespace MJU23v_D10_inl_sveng
{
    internal class Program
    {
        static List<SweEngGloss> dictionary;
        class SweEngGloss
        {
            public string word_swe, word_eng;
            public SweEngGloss(string word_swe, string word_eng)
            {
                this.word_swe = word_swe; this.word_eng = word_eng;
            }
            public SweEngGloss(string line)
            {
                string[] words = line.Split('|');
                this.word_swe = words[0]; this.word_eng = words[1];
            }

            public static void LoadFile(string[] argument, string defaultFile)
            {
                if (argument.Length == 2)
                {
                    if (!File.Exists(argument[1]) || !Path.HasExtension(argument[1]))
                    {
                        Console.WriteLine($"Error: Invalid path '{argument[1]}");
                        return;
                    }

                    using (StreamReader sr = new StreamReader(argument[1]))
                    {
                        dictionary = new List<SweEngGloss>(); // Empty it!
                        string line = sr.ReadLine();
                        while (line != null)
                        {
                            SweEngGloss gloss = new SweEngGloss(line);
                            dictionary.Add(gloss);
                            line = sr.ReadLine();
                        }
                    }
                }
                else if (argument.Length == 1)
                {
                    using (StreamReader sr = new StreamReader(defaultFile))
                    {
                        dictionary = new List<SweEngGloss>(); // Empty it!
                        string line = sr.ReadLine();
                        while (line != null)
                        {
                            SweEngGloss gloss = new SweEngGloss(line);
                            dictionary.Add(gloss);
                            line = sr.ReadLine();
                        }
                    }
                }
            }

            public static void NewWord(string[] argument)
            {
                if (dictionary == null)
                {
                    Console.WriteLine("Error: Dictionary is empty, please load a dictionary using 'load'");
                    return;
                }
                if (argument.Length == 3)
                {
                    dictionary.Add(new SweEngGloss(argument[1], argument[2]));
                }
                else if (argument.Length == 1)
                {
                    Console.WriteLine("Write word in Swedish: ");
                    string sweWord = Console.ReadLine();
                    Console.Write("Write word in English: ");
                    string engWord = Console.ReadLine();
                    dictionary.Add(new SweEngGloss(sweWord, engWord));
                }
            }
            public static void DeleteWord(string[] argument)
            {
                if (dictionary == null)
                {
                    Console.WriteLine("Error: Dictionary is empty, please load a dictionary using 'load'");
                    return;
                }
                if (argument.Length == 3)
                {
                    int index = -1;
                    for (int i = 0; i < dictionary.Count; i++)
                    {
                        SweEngGloss gloss = dictionary[i];
                        if (gloss.word_swe == argument[1] && gloss.word_eng == argument[2])
                        {
                            index = i;
                            dictionary.RemoveAt(index); // TODO: Output message if user tries to remove invalid word
                        }
                    }
                }
                else if (argument.Length == 1)
                {
                    Console.WriteLine("Write word in Swedish: ");
                    string sweWord = Console.ReadLine();
                    Console.Write("Write word in English: ");
                    string engWord = Console.ReadLine();
                    int index = -1;
                    for (int i = 0; i < dictionary.Count; i++)
                    {
                        SweEngGloss gloss = dictionary[i];
                        if (gloss.word_swe == sweWord && gloss.word_eng == engWord)
                        {
                            index = i;
                            dictionary.RemoveAt(index); // TODO: Output message if user tries to remove invalid word
                        }
                    }
                }
            }
            public static void Translate(string[] argument)
            {
                if (dictionary == null)
                {
                    Console.WriteLine("Error: Dictionary is empty, please load a dictionary using 'load'");
                    return;
                }
                if (argument.Length == 2)
                {
                    foreach (SweEngGloss gloss in dictionary)
                    {
                        if (gloss.word_swe == argument[1])
                            Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                        if (gloss.word_eng == argument[1])
                            Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                    }
                }
                else if (argument.Length == 1)
                {
                    Console.WriteLine("Write word to be translated: ");
                    string input = Console.ReadLine();
                    foreach (SweEngGloss gloss in dictionary)
                    {
                        if (gloss.word_swe == input)
                            Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                        if (gloss.word_eng == input)
                            Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            string defaultFile = "..\\..\\..\\dict\\sweeng.lis";
            Console.WriteLine("Welcome to the dictionary app!");
            string command = "";
            do
            {
                Console.Write("> ");
                string[] argument = Console.ReadLine().Split();
                command = argument[0];
                if (command == "quit")
                {
                    Console.WriteLine("Goodbye!"); // NYI: Make sure program exits
                }
                else if (command == "load")
                {
                    SweEngGloss.LoadFile(argument, defaultFile);
                }
                else if (command == "list")
                {
                    if (dictionary == null)
                    {
                        Console.WriteLine("Error: Dictionary is empty, please load a dictionary using 'load'");
                        continue;
                    }
                    foreach(SweEngGloss gloss in dictionary)
                    {
                        Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}");
                    }
                }
                else if (command == "new")
                {
                    SweEngGloss.NewWord(argument);
                }
                else if (command == "delete")
                {
                    SweEngGloss.DeleteWord(argument);
                }
                else if (command == "translate")
                {
                    SweEngGloss.Translate(argument);
                }
                else if (command == "help")
                {
                    Console.WriteLine("Available commands:");
                    Console.WriteLine(" translate - translate word");
                    Console.WriteLine(" load - load dictionary");
                    Console.WriteLine(" delete - delete word from dictionary");
                    Console.WriteLine(" new - add new word to dictionary");
                    Console.WriteLine(" list - list all words in dictionary");
                    Console.WriteLine(" quit - exit program");
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{command}'");
                }
            }
            while (command != "quit");
        }
    }
}