using System;
using System.Data.SqlClient;

namespace Wizard_Witch
{
    interface sortCharactersIntoThereHouse
    {
        void sortCharac(); 
    }

    struct Personality
    {
        public int Boldness { get; set; }
        public int Ambition { get; set; }
        public int Knowledge { get; set; }
        public int Loyalty { get; set; }
        public int Independence { get; set; }
        public int Resourcefulness { get; set; }
        public int Curiosity { get; set; }
        public int Dedication { get; set; }
        public int Adaptability { get; set; }
        public int Protectiveness { get; set; }
        public int Greatness { get; set; }
        public int Discovery { get; set; }
        public int Helping { get; set; }
        public int SelfTruth { get; set; }

        public Personality(int boldness, int ambition, int knowledge, int loyalty, int independence,
                           int resourcefulness, int curiosity, int dedication, int adaptability,
                           int protectiveness, int greatness, int discovery, int helping, int selfTruth)
        {
            this.Boldness = boldness;
            this.Ambition = ambition;
            this.Knowledge = knowledge;
            this.Loyalty = loyalty;
            this.Independence = independence;
            this.Resourcefulness = resourcefulness;
            this.Curiosity = curiosity;
            this.Dedication = dedication;
            this.Adaptability = adaptability;
            this.Protectiveness = protectiveness;
            this.Greatness = greatness;
            this.Discovery = discovery;
            this.Helping = helping;
            this.SelfTruth = selfTruth;
        }

        public int GetBoldness() => this.Boldness;
        public int GetAmbition() => this.Ambition;
        public int GetKnowledge() => this.Knowledge;
        public int GetLoyalty() => this.Loyalty;
        public int GetIndependence() => this.Independence;
        public int GetResourcefulness() => this.Resourcefulness;
        public int GetCuriosity() => this.Curiosity;
        public int GetDedication() => this.Dedication;
        public int GetAdaptability() => this.Adaptability;
        public int GetProtectiveness() => this.Protectiveness;
        public int GetGreatness() => this.Greatness;
        public int GetDiscovery() => this.Discovery;
        public int GetHelping() => this.Helping;
        public int GetSelfTruth() => this.SelfTruth;
    }

    public class House
    {
        public string AssignedHouse { get; private set; }

        public House(string assignedHouse)
        {
            if (string.IsNullOrEmpty(assignedHouse))
                throw new ArgumentException("Assigned house cannot be null or empty.", nameof(assignedHouse));

            this.AssignedHouse = assignedHouse;
        }

        public void displayCharacterHouse()
        {
            Console.WriteLine("\n|     You are born to be in " + this.AssignedHouse + "        |");
            Console.WriteLine("=============================================");
            Console.WriteLine("|       CHARACTER SUCCESSFULLY CREATED!     |");
            Console.WriteLine("=============================================");
        }

        public void SaveCharacterToDatabase(string characterName)
        {
            string DBconnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=C:\USERS\ANGELES\SOURCE\TASKPERFORMANCE\WIZARD&WITCH\WIZARD&WITCH\CHARACTER.MDF;Integrated Security=True;";

            using (SqlConnection connectToSql = new SqlConnection(DBconnection))
            {
                try
                {
                    string insertQueryString = @"INSERT INTO dbo.CHARACTER_INFO (CharacterName, House) VALUES (@CharacterName, @AssignedHouse)";

                    using (SqlCommand insertDataPuhliz = new SqlCommand(insertQueryString, connectToSql))
                    {
                 
                        insertDataPuhliz.Parameters.AddWithValue("@CharacterName", characterName);
                        insertDataPuhliz.Parameters.AddWithValue("@AssignedHouse", this.AssignedHouse);
                        connectToSql.Open();
                        insertDataPuhliz.ExecuteNonQuery();
                   //     Console.WriteLine("\nCharacter data saved to the database successfully!");
                    }
                }
                catch (Exception e)
                {
                  //  Console.WriteLine("Error while saving the character data to the database: " + e.Message);
                }
            }
        }

        public void SaveAssignedHouseToDatabase(string characterName)
        {
            string DBconnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=C:\USERS\ANGELES\SOURCE\TASKPERFORMANCE\WIZARD&WITCH\WIZARD&WITCH\CHARACTER.MDF;Integrated Security=True;";

            using (SqlConnection connectToSql = new SqlConnection(DBconnection))
            {
                try
                {
                    string updateQueryString = @"UPDATE dbo.CHARACTER_INFO 
                                         SET House = @AssignedHouse 
                                         WHERE CharacterName = @CharacterName";

                    using (SqlCommand updateDataPuhliz = new SqlCommand(updateQueryString, connectToSql))
                    {
                        updateDataPuhliz.Parameters.AddWithValue("@AssignedHouse", this.AssignedHouse);
                        updateDataPuhliz.Parameters.AddWithValue("@CharacterName", characterName);

                        connectToSql.Open();
                        updateDataPuhliz.ExecuteNonQuery();
                 //       Console.WriteLine("\nAssigned house saved to the database successfully!");
                    }
                }
                catch (Exception e)
                {
                //    Console.WriteLine("Error while saving the assigned house to the database: " + e.Message);
                }
            }
        }

    }

    public class DesignatedHouse
    {
        public House AssignUsersHouse(int boldness, int ambition, int knowledge, int loyalty, int independence,
                                      int resourcefulness, int curiosity, int dedication, int adaptability,
                                      int protectiveness, int greatness, int discovery, int helping, int selfTruth)
        {
            int gryffindorScore = 0, ravenclawScore = 0, hufflepuffScore = 0, slytherinScore = 0;

            gryffindorScore += boldness + ambition + adaptability + greatness;
            ravenclawScore += knowledge + independence + curiosity + dedication;
            hufflepuffScore += loyalty + helping + protectiveness + adaptability;
            slytherinScore += ambition + resourcefulness + greatness + selfTruth;

            int maxScore = Math.Max(Math.Max(gryffindorScore, ravenclawScore),
                                    Math.Max(hufflepuffScore, slytherinScore));

            if (maxScore == gryffindorScore)
                return new House("Gryffindor");
            else if (maxScore == ravenclawScore)
                return new House("Ravenclaw");
            else if (maxScore == hufflepuffScore)
                return new House("Hufflepuff");
            else
                return new House("Slytherin");
        }
    }

    public class CharacterPersonality : sortCharactersIntoThereHouse
    {
        private readonly Personality personality;
        private readonly DesignatedHouse houseFactory;
        private readonly string characterName;

        public CharacterPersonality(string characterName)
        {
            this.characterName = characterName;
            this.houseFactory = new DesignatedHouse();
            this.personality = GetPersonality();
        }

        private Personality GetPersonality()
        {
            string[] boldnessChoices = { "Boldly", "Strategically", "Creatively", "Dependably", "Calmly" };
            string[] ambitionChoices = { "Bravery", "Ambition", "Knowledge", "Loyalty", "Independence" };
            string[] knowledgeChoices = { "Lead", "Influence", "Advise", "Support", "Observe" };
            string[] loyaltyChoices = { "Courage", "Determination", "Intelligence", "Trustworthiness", "Open-mindedness" };
            string[] independenceChoices = { "Fearlessly", "Methodically", "Thoughtfully", "Persistently", "Flexibly" };
            string[] strengthChoices = { "Boldness", "Resourcefulness", "Curiosity", "Dedication", "Adaptability" };
            string[] motivationChoices = { "Protecting Others", "Achieving Greatness", "Discovering New Things", "Helping Those in Need", "Staying True to Yourself" };

            int boldness = AskUser("HOW DO YOU REACT TO A DIFFICULT SITUATION?", boldnessChoices);
            int ambition = AskUser("WHAT’S MOST IMPORTANT TO YOU IN LIFE?", ambitionChoices);
            int knowledge = AskUser("HOW DO YOU WORK WITH OTHERS?", knowledgeChoices);
            int loyalty = AskUser("WHAT DO YOU VALUE IN A FRIEND?", loyaltyChoices);
            int independence = AskUser("HOW DO YOU APPROACH YOUR GOALS?", independenceChoices);
            int resourcefulness = AskUser("WHAT IS YOUR BIGGEST STRENGTH?", strengthChoices);
            int motivation = AskUser("WHAT MOTIVATES YOU THE MOST?", motivationChoices);

            return new Personality(boldness, ambition, knowledge, loyalty, independence,
                                   resourcefulness, motivation, 0, 0, 0, 0, 0, 0, 0);
        }

        private int AskUser(string question, string[] choices)
        {
            int answer;
            do
            {
                Console.WriteLine($"\n{question}");
                for (int i = 0; i < choices.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {choices[i]}");
                }
                seperator();
                Console.Write("Enter Your Answer (1-5): ");
                if (!int.TryParse(Console.ReadLine(), out answer) || answer < 1 || answer > 5)
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");
                }
                seperator();
            } while (answer < 1 || answer > 5);

            return answer;
        }
        static void seperator()
        {
            for (int i = 0; i < 45; i++)
            {
                Console.Write("=");
            }
            Console.WriteLine();
        }

        public void sortCharac()
        {
          
            House assignedHouse = this.houseFactory.AssignUsersHouse(
                this.personality.GetBoldness(),
                this.personality.GetAmbition(),
                this.personality.GetKnowledge(),
                this.personality.GetLoyalty(),
                this.personality.GetIndependence(),
                this.personality.GetResourcefulness(),
                this.personality.GetCuriosity(),
                this.personality.GetDedication(),
                this.personality.GetAdaptability(),
                this.personality.GetProtectiveness(),
                this.personality.GetGreatness(),
                this.personality.GetDiscovery(),
                this.personality.GetHelping(),
                this.personality.GetSelfTruth()
            );

            assignedHouse.displayCharacterHouse();
            assignedHouse.SaveCharacterToDatabase(this.characterName);
            assignedHouse.SaveAssignedHouseToDatabase(this.characterName);
        }

    }
}
