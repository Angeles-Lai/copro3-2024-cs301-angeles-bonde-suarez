using System;
using System.Numerics;
using MySql.Data.MySqlClient;
using Wizard_Witch;
using System.Data.SqlClient;

namespace DatabaseConnectionApp
{
    class MainMethod
    {
        public static void Main(string[] args)
        {
            
            string connectionString = "Server=localhost; Database=CharacterCreation; Uid=root; Pwd=;";
            character playerCharacter = new PlayersCharacter();

            try
            {
            
                GameOpening(playerCharacter);
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }
           
        }
        static void GameOpening(character playerCharacter)
        {
            Console.WriteLine("\n");
            seperator();
            Console.WriteLine("\n            WELCOME TO\n        MAGIC CHRONICLES");
            seperator();

            string choice = GetOpeningChoice(new string[] { "NEW GAME", "LOAD GAME", "CAMPAIGN MODE", "CREDITS", "EXIT" });
            seperator();

            switch (choice)
            {
                case "NEW GAME":
                    PlayersCharacter PC = new PlayersCharacter();

                    Console.WriteLine("\nCREATE YOUR CHARACTER");
                    playerCharacter.username();
                    playerCharacter.physicalCharacteristic();
                    playerCharacter.mole();
                    playerCharacter.clothes();
                    playerCharacter.wands();
            

                    playerCharacter.stats();
                    string characterName = ((PlayersCharacter)playerCharacter).characterName;  
                    CharacterPersonality personality = new CharacterPersonality(characterName); 
                                                                                                
                    playerCharacter.Features();
                    personality.sortCharac();
                    askagain:
                    try
                    {
                        Console.WriteLine("\nDo you want to go back to Main Menu?\n[1] YES\n[2] NO");
                        Console.Write("Enter Your Choice: ");
                        int backChoice = Convert.ToInt32(Console.ReadLine());
                        if (backChoice == 1)
                        {
                            GameOpening(playerCharacter);
                        }
                        else if (backChoice == 2)
                        {
                            Console.WriteLine("\nThank You For Playing"); Environment.Exit(0);
                        }
                        else
                        {
                            Console.Write("Invalid Input! Try again");
                            goto askagain;
                        }

                    }
                    catch (Exception ex) { 
                        goto askagain;
                    }


                    break;

                case "LOAD GAME":
                loadgame:

                    Console.WriteLine("\nLOAD GAME OPTIONS:");
                    Console.WriteLine("[1] Display All Characters");
                    Console.WriteLine("[2] Delete A Character");
                    Console.WriteLine("[3] Return To Main Menu");

                    int loadChoice;
                    while (true)
                    {
                        Console.Write("\nEnter Your Choice: ");
                        string userInput = Console.ReadLine();

                        // Validate if the input is a valid integer
                        if (int.TryParse(userInput, out loadChoice) && loadChoice >= 1 && loadChoice <= 3)
                        {
                            break; // Exit loop if valid input is entered
                        }

                        // Display error message for invalid input
                        Console.WriteLine("\nInvalid input! Please enter a number between 1 and 3.");
                    }

                    switch (loadChoice)
                    {
                        case 1: // Display all characters
                            seperator();
                            Console.WriteLine("\nALL CHARACTERS:");
                            DisplayAllCharacters();
                            Console.Write("\nPress any key to return to the Load Game menu.");
                            Console.ReadKey();
                            goto loadgame;

                       
                        case 3: // Return to main menu
                            GameOpening(playerCharacter);
                            break;
                        case 2: // Delete a character
                            shortInfo();
                            seperator();

                            bool continueDeleting = true;

                            while (continueDeleting)
                            {
                                Console.Write("\nEnter the name of the character you want to delete: ");
                                string charNameToDelete = Console.ReadLine()?.Trim();

                                    DeleteCharacter(charNameToDelete);
                                

                                Console.Write("\nKeep Deleting?\n[Y] Yes \n[N] No\nEnter Your Choice: ");
                                string input = Console.ReadLine()?.Trim().ToUpper();

                                switch (input)
                                {
                                    case "Y":
                                        continueDeleting = true;
                                        break;
                                    case "N":
                                        continueDeleting = false;
                                        GameOpening(playerCharacter);
                                        break;
                                    default:
                                        seperator();
                                        Console.WriteLine("\nInvalid Input! \nReturning to the Load Game menu.");
                                        goto loadgame;
                                }
                            }
                            break;

                        default:
                            // This case should not occur due to input validation above
                            seperator();
                            Console.WriteLine("\nInvalid choice. Returning to Load Game menu.");
                            goto loadgame;
                    }
                    break;


                case "CAMPAIGN MODE":
                    Console.WriteLine("\nCAMPAIGN MODE:");
                    Console.WriteLine("\nYou are living in a small village called ‘Wandborough’. " +
                         "Ever since you were born, people kept calling you a freak, witch, strange, " +
                         "and scary for being different from others. You were abandoned by your parents " +
                         "because they were ashamed and embarrassed to have you. However, who would’ve thought " +
                         "that the mockery would result in an identity crisis and confusion for you? " +
                         "But then one day, you met a beautiful and kind girl named ‘Elowen’. " +
                         "A special kid just like you, Elowen became close friends with you immediately after " +
                         "she saw you being bullied by normal kids. Because of that, Elowen introduced you to her academy, " +
                         "a place that will eventually help you find your way of living, a way of identifying your capabilities—" +
                         "where you will never be labeled as a ‘Freak, Witch, Strange, or Scary’.");

                    Console.WriteLine("\nThe day has come when you entered the academy. " +
                        "Everybody has their own specialties and powers, but somehow you still felt left out " +
                        "because you did not know how to manage your own powers. Fortunately, there’s a boy named ‘Albus’. " +
                        "The meaning of his name is ‘Bright or White,’ symbolizing leadership and wisdom. " +
                        "However, his name is completely opposite of his powers because, despite his bright name, " +
                        "he is a strong guy, and anyone who messes with him surely won’t wake up the next morning. " +
                        "But you are an exception. Albus found you in a way that he felt you needed to be taught and supported. " +
                        "Therefore, Albus taught you how to make use of your powers, gave you a wand, showed you how to fly, " +
                        "and taught you how to turn every fear into strength.");

                    Console.WriteLine("\nThe final stage has come where you need to choose your physical features and strengths " +
                        "based on your magical powers. To test if you really remembered everything that Albus taught you, " +
                        "he put you into the well-known challenge called ‘Mystic Quest Gauntlet.’ Here, you must navigate a series of " +
                        "increasingly difficult trials. Each trial is a combination of intelligence, bravery, resourcefulness, and magical ability. " +
                        "Whoever wins the challenge will not only earn glory but also a powerful magical artifact such as an amulet, " +
                        "as well as the title of ‘Master of the Mystic Quest.’");
                    Console.Write("\nPress any key to return to the main menu.");
                    Console.ReadKey();
                    Console.Write("\n");
                    GameOpening(playerCharacter);
                    break;

                case "CREDITS":
                    Console.WriteLine("\nCREATOR OF THE GAME:");
                    Console.WriteLine("\nAngeles, Laila O. \n- CS student soon mag s-shift sa ibang course\n" +
                        "\nBonde, Jenny \n- CS Student na magaling sa math at batak mag ML\n" +
                        "\nSuarez, Khatlyn \n- The best na singer ng CS301\n");
                    Console.Write("\nPress any key to return to the main menu.");
                    Console.ReadKey();
                    Console.Write("\n");
                    GameOpening(playerCharacter);
                    break;

                case "EXIT":
                    Console.WriteLine("\nAre you sure you want to exit (yes/no)?");
                    string exit = Console.ReadLine()?.Trim().ToLower();
                    if (exit == "yes")
                    {
                        Console.WriteLine("\nYou've successfully exited the game!");
                        Environment.Exit(0);
                    }
                    else
                    {
                        GameOpening(playerCharacter);  
                    }
                    break;


                default:
                    Console.WriteLine("\nInvalid choice. Please try again.");
                    GameOpening(playerCharacter);
                    break;

            }

        }

        static void seperator()
        {
            for (int i = 0; i < 45; i++)
            {
                Console.Write("=");
            }
            Console.WriteLine();
        }

        static string GetOpeningChoice(string[] options)
        {
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"[{i + 1}] {options[i]}");
            }

            Console.Write("\nEnter The Number Of Your Choice: ");
            int chosen;

            while (!int.TryParse(Console.ReadLine(), out chosen) || chosen < 1 || chosen > options.Length)
            {
                Console.WriteLine("\nInvalid Input. Please enter a valid option.");
                Console.Write("Enter The Number Of Your Choice: ");
            }

            return options[chosen - 1];
        }
        static void DisplayAllCharacters()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Angeles\source\TaskPerformance\Wizard&Witch\Wizard&Witch\Character.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT CharacterName, EyeColor, HairColor, FaceShape, BodyShape, HairLength, LipColor, SkinTone, Height, Gender, HasMole, Outfit, Wand, Intelligence, Luck, Strength, Endurance, Speed, House FROM CHARACTER_INFO";

                    SqlCommand cmd = new SqlCommand(query, connection);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            Console.WriteLine("No characters found in the database.");
                        }
                        else
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"Name: {reader["CharacterName"]}");
                                Console.WriteLine($"Eye Color: {reader["EyeColor"]}");
                                Console.WriteLine($"Hair Color: {reader["HairColor"]}");
                                Console.WriteLine($"Face Shape: {reader["FaceShape"]}");
                                Console.WriteLine($"Body Shape: {reader["BodyShape"]}");
                                Console.WriteLine($"Hair Length: {reader["HairLength"]}");
                                Console.WriteLine($"Lip Color: {reader["LipColor"]}");
                                Console.WriteLine($"Skin Tone: {reader["SkinTone"]}");
                                Console.WriteLine($"Height: {reader["Height"]}");
                                Console.WriteLine($"Gender: {reader["Gender"]}");
                                Console.WriteLine($"Mole: {reader["HasMole"]}");
                                Console.WriteLine($"Outfit: {reader["Outfit"]}");
                                Console.WriteLine($"Wand: {reader["Wand"]}");
                                Console.WriteLine($"Intelligence: {reader["Intelligence"]}");
                                Console.WriteLine($"Luck: {reader["Luck"]}");
                                Console.WriteLine($"Strength: {reader["Strength"]}");
                                Console.WriteLine($"Endurance: {reader["Endurance"]}");
                                Console.WriteLine($"Speed: {reader["Speed"]}");
                                Console.WriteLine($"Assigned House: {reader["House"]}\n");
                                seperator();
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Database error: {ex.Message}");
                }
            }
        }

        static void shortInfo() {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Angeles\source\TaskPerformance\Wizard&Witch\Wizard&Witch\Character.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT CharacterName, House FROM CHARACTER_INFO"; // Only select name and house
                    SqlCommand cmd = new SqlCommand(query, connection);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            Console.WriteLine("No characters found in the database.");
                        }
                        else
                        {
                            Console.WriteLine("\nALL CHARACTERS:");
                            while (reader.Read())
                            {
                                Console.WriteLine($"Name: {reader["CharacterName"]}");
                                Console.WriteLine($"House: {reader["House"]}");
                                Console.WriteLine(); // Add a blank line between characters for readability
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Database error: {ex.Message}");
                }

            }
        }
        static void DeleteCharacter(string characterName)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Angeles\source\TaskPerformance\Wizard&Witch\Wizard&Witch\Character.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string checkQuery = "SELECT COUNT(*) FROM CHARACTER_INFO WHERE CharacterName = @Name";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, connection);
                    checkCmd.Parameters.AddWithValue("@Name", characterName);

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        
                        Console.WriteLine("Are you sure you want to delete this character? (yes/no)");

                        string deleteConfirmation = Console.ReadLine()?.ToLower();

                        if (deleteConfirmation == "yes")
                        {
                            string deleteQuery = "DELETE FROM CHARACTER_INFO WHERE CharacterName = @Name";
                            SqlCommand deleteCmd = new SqlCommand(deleteQuery, connection);
                            deleteCmd.Parameters.AddWithValue("@Name", characterName);

                            int rowsAffected = deleteCmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                seperator();
                                Console.WriteLine($"\nCharacter '{characterName}' deleted successfully.");
                            }
                            else
                            {
                                Console.WriteLine($"\nFailed to delete the character '{characterName}'.");
                            }
                        }
                        else if (deleteConfirmation == "no")
                        {
                            Console.WriteLine("\nDeletion cancelled.");
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid input. Deletion cancelled.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\nNo character found with the name '{characterName}'.");
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Database error: {ex.Message}");
                }
            }
        }

    }
}
