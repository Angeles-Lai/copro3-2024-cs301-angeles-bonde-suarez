using System;
using System.Data.SqlClient;

namespace DatabaseConnectionApp
{
    public abstract class character // Abstract class
    {
        public abstract void username();
        public abstract void physicalCharacteristic();
        public abstract bool mole();
        public abstract void clothes();
        public abstract void wands();
        public abstract void stats();
        public abstract void Features();
    }

    public class PlayersCharacter : character
    {
        public string characterName;
        public string EyeColor { get; set; }
        public string HairColor { get; set; }
        public string FaceShape { get; set; }
        public string BodyShape { get; set; }
        public string HairLength { get; set; }
        public string lipColor { get; set; }
        public string skinTone { get; set; }
        public string height { get; set; }
        public string Gender { get; set; }
        public bool HasMole { get; set; }

        public string outfit { get; set; }
        public string wand { get; set; }

        private const int MaxPoints = 20;
        public int remainingPoints = MaxPoints;

        public int Intelligence { get; set; }
        public int Luck { get; set; }
        public int Strength { get; set; }
        public int Endurance { get; set; }
        public int Speed { get; set; }

        public override void username()
        {
            Console.WriteLine("Character Creation Started...");
            do
            {
                Console.Write("\nEnter Your Character's Name: ");
                characterName = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(characterName))
                {
                    Console.WriteLine("Validation error: Character name is required. Please enter a valid name.");
                    continue;
                }

            
                if (CheckIfCharacterExists(characterName))
                {
                    Console.WriteLine("This name is already taken. Please choose a different name.");
                }
                else
                {
                    break; 
                }

            } while (string.IsNullOrWhiteSpace(characterName) || CheckIfCharacterExists(characterName));

            // Console.WriteLine($"Character created successfully! Name: {characterName}");
        }

        private bool CheckIfCharacterExists(string name)
        {
            string DBconnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=C:\USERS\ANGELES\SOURCE\TASKPERFORMANCE\WIZARD&WITCH\WIZARD&WITCH\CHARACTER.MDF;Integrated Security=True;";

            using (SqlConnection connectToSql = new SqlConnection(DBconnection))
            {
                try
                {
                    string query = "SELECT COUNT(1) FROM dbo.CHARACTER_INFO WHERE CharacterName = @CharacterName";
                    using (SqlCommand cmd = new SqlCommand(query, connectToSql))
                    {
                        cmd.Parameters.AddWithValue("@CharacterName", name);

                        connectToSql.Open();
                        int count = (int)cmd.ExecuteScalar();

                        return count > 0; 
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error checking if character exists: " + e.Message);
                    return false;
                }
            }
        }

        static void seperator()
        {
            for (int i = 0; i < 45; i++)
            {
                Console.Write("=");
            }
        }

        static void invalidmessage()
        {
            Console.WriteLine("\nInvalid Input, Please try again");
        }

        public override void physicalCharacteristic()
        {
            EyeColor = physicalCharacteristic("Choose Eye Color:", new string[] { "BLACK", "BROWN", "BLUE", "GREEN", "WHITE" });
            HairColor = physicalCharacteristic("Choose Hair Color:", new string[] { "BLACK", "BROWN", "BLUE", "GREEN", "WHITE" });
            FaceShape = physicalCharacteristic("Choose Face Shape:", new string[] { "OVAL", "ROUND", "HEART", "DIAMOND", "RECTANGULAR" });
            BodyShape = physicalCharacteristic("Choose Body Shape:", new string[] { "RECTANGULAR", "HOUR GLASS", "APPLE", "PEAR", "TRIANGLE" });
            HairLength = physicalCharacteristic("Choose Hair Length:", new string[] { "BALD", "MEDIUM", "SHORT", "LONG", "WAIST LENGTH" });
            lipColor = physicalCharacteristic("Choose Lip Color:", new string[] { "NATURAL", "GREEN", "PINK", "RED", "VIOLET" });
            skinTone = physicalCharacteristic("Choose Skin Tone:", new string[] { "PORCELAIN", "TAN", "OLIVE", "BEIGE", "CHOCOLATE" });
            height = physicalCharacteristic("Choose Height:", new string[] { "VERY SHORT (120-149cm)", "SHORT (150-164cm)", "AVERAGE (165-179 cm)", "TALL (180-199 cm)", "VERY TALL (200-250 cm)" });
            Gender = physicalCharacteristic("Choose Gender:", new string[] { "FEMALE", "MALE", "HETERO", "HOMO", "NON-BINARY" });
        }

        private string physicalCharacteristic(string message, string[] options)
        {
            Console.WriteLine("\n" + message);

            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"[{i + 1}] {options[i]}");
            }
            Console.Write("Enter The Number Of Your Choice: ");
            int chosen;
            while (!int.TryParse(Console.ReadLine(), out chosen) || chosen < 1 || chosen > options.Length)
            {
                invalidmessage();
                Console.Write("Enter The Number Of Your Choice: ");
            }
            seperator();
            return options[chosen - 1];
        }

        public override bool mole()
        {
            while (true)
            {
                Console.WriteLine("\nAdd a mole underneath the right eye?\n[1] YES\n[2] NO");
                Console.Write("Enter (1 or 2): ");
                string choice = Console.ReadLine();

                if (choice.Equals("1"))
                {
                    HasMole = true;
                    return true;
                }
                else if (choice.Equals("2"))
                {
                    HasMole = false;
                    return false;
                }
                else
                {
                    invalidmessage();
                }
            }
        }

        public override void clothes()
        {
            (string name, string description)[] options =
            {
                ("Mystic Robes of the Arcane", "Flowing robes adorned with glowing runes, perfect for spellcasters seeking ultimate power."),
                ("Shadow Cloak of Secrets", "A dark, hooded cloak that blends into the night, ideal for stealthy wizards and witches."),
                ("Battle-Enchanted Armor", "Magical armor that combines protection with charm, suited for warriors of the arcane arts."),
                ("Ethereal Silk Ensemble", "Lightweight, shimmering garments that radiate elegance, perfect for enchanting others."),
                ("Elemental Tunic and Cape", "A vibrant outfit channeling the power of fire, water, earth, and air, for nature-bound spellcasters.")
            };

            string chosenOutfit = clothes("Choose Your Wizard/Witch Outfit", options);
        }

        public override void wands()
        {
            (string name, string description)[] options =
            {
                ("Elderwood Wand", "Crafted from ancient elder trees, this wand is known for its unmatched power and wisdom."),
                ("Phoenix Feather Core Wand", "Infused with a phoenix feather, it grants incredible versatility and resilience."),
                ("Dragon Heartstring Wand", "A fiery wand with a fierce temper, ideal for bold and daring spellcasters."),
                ("Willow Wand of Healing", "Made from willow, this wand excels in protective and restorative magic."),
                ("Obsidian Crystal Wand", "Forged from volcanic glass, it channels dark and mysterious energy for intricate spells")
            };

            string wandoption = wands("Choose Your Magical Wand:", options);
            seperator();
        }

        private string clothes(string question, (string name, string description)[] options)
        {
            seperator();
            Console.WriteLine($"\n{question}:");

            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"\n[{i + 1}] {options[i].name}");
                Console.WriteLine($"\n-{options[i].description}\n");
            }

            Console.Write("Enter The Number Of Your Choice: ");
            int chosen;

            while (!int.TryParse(Console.ReadLine(), out chosen) || chosen < 1 || chosen > options.Length)
            {
                Console.WriteLine("Invalid choice. Please enter a valid number between 1 and 5.");
                Console.Write("Enter The Number Of Your Choice: ");
            }

            outfit = options[chosen - 1].name;
            return outfit;
        }

        private string wands(string question, (string name, string description)[] options)
        {
            seperator();
            Console.WriteLine($"\n{question}:");

            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"\n[{i + 1}] {options[i].name}");
                Console.WriteLine($"\n-{options[i].description}\n");
            }

            Console.Write("Enter The Number Of Your Choice: ");
            int chosen;

            while (!int.TryParse(Console.ReadLine(), out chosen) || chosen < 1 || chosen > options.Length)
            {
                Console.WriteLine("Invalid choice. Please enter a valid number between 1 and 5.");
                Console.Write("Enter The Number Of Your Choice: ");
            }

            wand = options[chosen - 1].name;
            return wand;
        }

        public override void stats()
        {

            int totalAllocatedPoints = 0;
            Console.WriteLine("\nChoose the stat you want to allocate points to:\n" +
            "Rules:\nYou must distribute all 20 points across the stats you pick.\n" +
            "The system will keep asking until the total allocation equals 20 points.\n" +
            "Note: Each stat must have a value between 0 and 7, so allocate wisely.");
            seperator();
            Console.WriteLine("\n[1] Intelligence \n- Your ability to solve problems and master spells.\n");
            Console.WriteLine("[2] Luck \n- The probability of favorable outcomes in unpredictable situations.\n");
            Console.WriteLine("[3] Strength \n- The measure of your physical power and combat prowess.\n");
            Console.WriteLine("[4] Endurance \n- Your capacity to withstand damage and sustain physical effort.\n");
            Console.Write("[5] Speed \n- Your agility and swiftness in both movement and reaction.\n");

            while (remainingPoints > 0)
            {
                Console.WriteLine($"\nRemaining Points: {remainingPoints}");
                Console.Write("Choose a stat you want to put points to (1-5): ");

                if (int.TryParse(Console.ReadLine(), out int chosenCategory) && chosenCategory >= 1 && chosenCategory <= 5)
                {
                    Console.Write("Enter points: ");
                    if (int.TryParse(Console.ReadLine(), out int points) && points > 0 && points <= remainingPoints)
                    {
                        if (points > 7)
                        {
                            seperator();
                            Console.WriteLine("\nWarning: You have allocated more than 7 points to a stat. \nPlease ensure each stat is within the limit of 0 to 7.");
                            seperator();
                        }
                        else
                        {
                            stats(chosenCategory, points);
                            remainingPoints -= points;
                            totalAllocatedPoints += points;
                            update();
                        }
                    }
                    else
                    {
                        seperator();
                        Console.WriteLine("\nInvalid Points!");
                        seperator();
                    }
                }
                else
                {
                    seperator();
                    Console.WriteLine("\nWarning: Please select a number between 1-5");
                    seperator();
                }
            }
        }

        private void stats(int category, int points)
        {
            if (points < 0 || points > 7)
            {
                Console.WriteLine("\nPoints must be between 0 and 7.");
                return;
            }

            switch (category)
            {
                case 1:
                    if (Intelligence + points > 7)
                    {
                        Console.WriteLine("Intelligence cannot exceed 7.");
                        remainingPoints += points;
                    }
                    else Intelligence += points;
                    break;
                case 2:
                    if (Luck + points > 7) { Console.WriteLine("Luck cannot exceed 7."); remainingPoints += points; }
                    else Luck += points;
                    break;
                case 3:
                    if (Strength + points > 7) { Console.WriteLine("Strength cannot exceed 7."); remainingPoints += points; }
                    else Strength += points;
                    break;
                case 4:
                    if (Endurance + points > 7) { Console.WriteLine("Endurance cannot exceed 7."); remainingPoints += points; }
                    else Endurance += points;
                    break;
                case 5:
                    if (Speed + points > 7) { Console.WriteLine("Speed cannot exceed 7."); remainingPoints += points; }
                    else Speed += points;
                    break;
                default:
                    Console.WriteLine("\nInvalid category.");
                    break;
            }
            seperator();
        }

        private void update()
        {
            Console.WriteLine("\nUPDATED STATS:\n");
            Console.WriteLine($"Intelligence: {Intelligence}");
            Console.WriteLine($"Luck: {Luck}");
            Console.WriteLine($"Strength: {Strength}");
            Console.WriteLine($"Endurance: {Endurance}");
            Console.WriteLine($"Speed: {Speed}");
            seperator();
        }

        public override void Features()
        {
            seperator();
            Console.WriteLine($"\nName: {characterName}");
            Console.WriteLine("\nPHYSICAL APPEARANCE\n");
            Console.WriteLine($"Eye Color: {EyeColor}");
            Console.WriteLine($"Hair Color: {HairColor}");
            Console.WriteLine($"Face Shape: {FaceShape}");
            Console.WriteLine($"Body Shape: {BodyShape}");
            Console.WriteLine($"Hair Length: {HairLength}");
            Console.WriteLine($"Lip Color: {lipColor}");
            Console.WriteLine($"Skin Tone: {skinTone}");
            Console.WriteLine($"Height: {height}");
            Console.WriteLine($"Gender: {Gender}");
            bool moleResult = HasMole;
            Console.Write($"Mole underneath the right eye: {moleResult}\n");

            seperator();
            Console.WriteLine("\nCLOTHES AND WAND:\n");
            Console.WriteLine($"Outfit: {outfit}");
            Console.WriteLine($"Wand: {wand}");

            seperator();
            Console.WriteLine("\nSTATS:\n");
            Console.WriteLine($"Intelligence: {Intelligence}");
            Console.WriteLine($"Luck: {Luck}");
            Console.WriteLine($"Strength: {Strength}");
            Console.WriteLine($"Endurance: {Endurance}");
            Console.WriteLine($"Speed: {Speed}");
            seperator();

         //   Console.WriteLine("\nCharacter Created Successfully!");

            SaveCharacterToDatabase(); 
        }
        public void ResetCharacter()
        {
            Intelligence = 0;
            Luck = 0;
            Strength = 0;
            Endurance = 0;
            Speed = 0;
            remainingPoints = MaxPoints;
        }

        private void SaveCharacterToDatabase()
        {
            string DBconnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=C:\USERS\ANGELES\SOURCE\TASKPERFORMANCE\WIZARD&WITCH\WIZARD&WITCH\CHARACTER.MDF;Integrated Security=True;";

            using (SqlConnection connectToSql = new SqlConnection(DBconnection))
            {
                try
                {
                    string insertQueryString = @"
                INSERT INTO dbo.CHARACTER_INFO
                (CharacterName, EyeColor, HairColor, FaceShape, BodyShape, HairLength, LipColor, SkinTone, Height, Gender, HasMole, Outfit, Wand, Intelligence, Luck, Strength, Endurance, Speed) 
                VALUES (@CharacterName, @EyeColor, @HairColor, @FaceShape, @BodyShape, @HairLength, @LipColor, @SkinTone, @Height, @Gender, @HasMole, @Outfit, @Wand, @Intelligence, @Luck, @Strength, @Endurance, @Speed)";

                    using (SqlCommand insertDataPuhliz = new SqlCommand(insertQueryString, connectToSql))
                    {
                        insertDataPuhliz.Parameters.AddWithValue("@CharacterName", characterName);
                        insertDataPuhliz.Parameters.AddWithValue("@EyeColor", EyeColor); // Ensure this is not null
                        insertDataPuhliz.Parameters.AddWithValue("@HairColor", HairColor);
                        insertDataPuhliz.Parameters.AddWithValue("@FaceShape", FaceShape);
                        insertDataPuhliz.Parameters.AddWithValue("@BodyShape", BodyShape);
                        insertDataPuhliz.Parameters.AddWithValue("@HairLength", HairLength);
                        insertDataPuhliz.Parameters.AddWithValue("@LipColor", lipColor);
                        insertDataPuhliz.Parameters.AddWithValue("@SkinTone", skinTone);
                        insertDataPuhliz.Parameters.AddWithValue("@Height", height);
                        insertDataPuhliz.Parameters.AddWithValue("@Gender", Gender);
                        insertDataPuhliz.Parameters.AddWithValue("@HasMole", HasMole);
                        insertDataPuhliz.Parameters.AddWithValue("@Outfit", outfit);
                        insertDataPuhliz.Parameters.AddWithValue("@Wand", wand);
                        insertDataPuhliz.Parameters.AddWithValue("@Intelligence", Intelligence);
                        insertDataPuhliz.Parameters.AddWithValue("@Luck", Luck);
                        insertDataPuhliz.Parameters.AddWithValue("@Strength", Strength);
                        insertDataPuhliz.Parameters.AddWithValue("@Endurance", Endurance);
                        insertDataPuhliz.Parameters.AddWithValue("@Speed", Speed);

                        // Open connection and execute insert query
                        connectToSql.Open();
                        insertDataPuhliz.ExecuteNonQuery();
                        ResetCharacter();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error while saving the character data to the database: " + e.Message);
                }
            }
        }

    }
}
