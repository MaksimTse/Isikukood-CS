using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isikukood_cs
{
    public class Program
    {
        private static List<string> validIdCodes = new List<string>();

        public static void Main()
        {
            LoadCodesFromFile();

            while (true)
            {
                Console.WriteLine("Valige toiming:");
                Console.WriteLine("1. Koodide genereerimine");
                Console.WriteLine("2. Kuva koodide loend");
                Console.WriteLine("3. Välju");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        GenerateAndSaveCodes();
                        break;
                    case "2":
                        DisplayIdCodes();
                        break;
                    case "3":
                        SaveCodesToFile();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Vale valik. Valige 1, 2 või 3");
                        break;
                }
            }
        }

        private static void GenerateAndSaveCodes()
        {
            Random random = new Random();
            Console.Write("Sisestage genereeritavate koodide arv: ");
            if (int.TryParse(Console.ReadLine(), out int numberOfCodes))
            {
                for (int i = 0; i < numberOfCodes; i++)
                {
                    string randomIdCode = GenerateRandomIdCode(random);
                    Console.WriteLine($"genereeritud IdCode: {randomIdCode}");
                    IdCode id = new IdCode(randomIdCode);

                    if (id.IsValid())
                    {
                        Console.WriteLine($"synniaasta: {id.GetFullYear()}");
                        Console.WriteLine($"synnipaev: {id.GetBirthDate():dd.MM.yyyy}");
                        string hospital = DetermineHospital(randomIdCode);
                        Console.WriteLine($"Gospiatal: {hospital}\n");

                        validIdCodes.Add(randomIdCode);
                    }
                    else
                    {
                        Console.WriteLine("Vale IdCode\n");
                    }
                }

                SaveCodesToFile();
            }
            else
            {
                Console.WriteLine("Vale numbri sisestamine");
            }
        }

        private static void SaveCodesToFile()
        {
            File.WriteAllLines("idcodes.txt", validIdCodes);
        }

        private static void LoadCodesFromFile()
        {
            if (File.Exists("idcodes.txt"))
            {
                validIdCodes = File.ReadAllLines("idcodes.txt").ToList();
            }
        }

        private static void DisplayIdCodes()
        {
            Console.WriteLine("Õigesti loodud koodide loend:");
            foreach (string code in validIdCodes)
            {
                Console.WriteLine(code);
            }
        }

        public static string GenerateRandomIdCode(Random random)
        {
            string genderNumber = random.Next(1, 7).ToString();
            string year = random.Next(0, 100).ToString("D2");
            string month = random.Next(1, 13).ToString("D2");
            string day = random.Next(1, 32).ToString("D2");
            string randomNumbers = string.Join("", Enumerable.Range(0, 3).Select(_ => random.Next(10).ToString()));
            string controlNumber = random.Next(10).ToString();

            return $"{genderNumber}{year}{month}{day}{randomNumbers}{controlNumber}";
        }
        public static string DetermineHospital(string ikood)
        {
            string tahed8910 = ikood.Substring(7, 3);
            int t = int.Parse(tahed8910);

            if (1 < t && t < 10)
            {
                return "Kuressaare Haigla";
            }
            else if (11 < t && t < 19)
            {
                return "Tartu Ülikooli Naistekliinik, Tartumaa, Tartu";
            }
            else if (21 < t && t < 220)
            {
                return "Ida-Tallinna Keskhaigla, Pelgulinna sünnitusmaja, Hiiumaa, Keila, Rapla haigla, Loksa haigla";
            }
            else if (221 < t && t < 270)
            {
                return "Ida-Viru Keskhaigla";
            }
            else if (271 < t && t < 370)
            {
                return "Maarjamõisa Kliinikum";
            }
            else if (371 < t && t < 420)
            {
                return "Narva Haigla";
            }
            else if (421 < t && t < 470)
            {
                return "Pärnu Haigla";
            }
            else if (471 < t && t < 490)
            {
                return "Pelgulinna Sünnitusmaja";
            }
            else if (491 < t && t < 520)
            {
                return "Järvamaa Haigla";
            }
            else if (521 < t && t < 570)
            {
                return "Rakvere, Tapa haigla";
            }
            else if (571 < t && t < 600)
            {
                return "Valga Haigla";
            }
            else if (601 < t && t < 650)
            {
                return "Viljandi Haigla";
            }
            else if (651 < t && t < 700)
            {
                return "Lõuna-Eesti Haigla (Võru), Põlva Haigla";
            }
            else
            {
                return "Välismaаl";
            }
        }
    }
}
