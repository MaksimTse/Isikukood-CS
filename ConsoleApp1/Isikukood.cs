using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isikukood_cs
{
    public class IdCode
    {
        private readonly string _idCode;

        public IdCode(string idCode)
        {
            _idCode = idCode;
        }

        private static List<IdCode> validIdCodes = new List<IdCode>();

        public static void Run()
        {
            LoadCodesFromFile();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Valige toiming:");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("1. Koodide genereerimine");
                Console.WriteLine("2. Kuva koodide loend");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("3. Välju");
                Console.ResetColor();
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
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Sisestage genereeritavate koodide arv: ");
            if (int.TryParse(Console.ReadLine(), out int numberOfCodes))
            {
                for (int i = 0; i < numberOfCodes; i++)
                {
                    string randomIdCode = GenerateRandomIdCode(random);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"genereeritud IdCode: {randomIdCode}");
                    IdCode id = new IdCode(randomIdCode);

                    if (id.IsValid())
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"synniaasta: {id.GetFullYear()}");
                        Console.WriteLine($"synnipaev: {id.GetBirthDate():dd.MM.yyyy}");
                        string hospital = DetermineHospital(randomIdCode);
                        Console.WriteLine($"Gospiatal: {hospital}\n");
                        Console.ResetColor();

                        validIdCodes.Add(id); // Добавляем экземпляр IdCode в список
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Vale IdCode\n");
                        Console.ResetColor();
                    }
                }

                SaveCodesToFile();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Vale numbri sisestamine");
                Console.ResetColor();
            }
        }

        private static void SaveCodesToFile()
        {
            File.WriteAllLines("idcodes.txt", validIdCodes.Select(code => code._idCode));
        }

        private static void LoadCodesFromFile()
        {
            if (File.Exists("idcodes.txt"))
            {
                validIdCodes = File.ReadAllLines("idcodes.txt")
                    .Select(code => new IdCode(code))
                    .ToList();
            }
        }

        private static void DisplayIdCodes()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Õigesti loodud koodide loend:");
            foreach (var code in validIdCodes)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Kood: {code._idCode}");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Synniaasta: {code.GetFullYear()}");
                Console.WriteLine($"Synnipaev: {code.GetBirthDate():dd.MM.yyyy}");
                string hospital = DetermineHospital(code._idCode);
                Console.WriteLine($"Gospiatal: {hospital}\n");
                Console.ResetColor();
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

    private bool IsValidLength()
        {
            return _idCode.Length == 11;
        }

        private bool ContainsOnlyNumbers()
        {
            // return _idCode.All(Char.IsDigit);

            for (int i = 0; i < _idCode.Length; i++)
            {
                if (!Char.IsDigit(_idCode[i]))
                {
                    return false;
                }
            }
            return true;
        }

        private int GetGenderNumber()
        {
            return Convert.ToInt32(_idCode.Substring(0, 1));
        }

        private bool IsValidGenderNumber()
        {
            int genderNumber = GetGenderNumber();
            return genderNumber > 0 && genderNumber < 7;
        }

        private int Get2DigitYear()
        {
            return Convert.ToInt32(_idCode.Substring(1, 2));
        }

        public int GetFullYear()
        {
            int genderNumber = GetGenderNumber();
            // 1, 2 => 18xx
            // 3, 4 => 19xx
            // 5, 6 => 20xx
            return 1800 + (genderNumber - 1) / 2 * 100 + Get2DigitYear();
        }

        private int GetMonth()
        {
            return Convert.ToInt32(_idCode.Substring(3, 2));
        }

        private bool IsValidMonth()
        {
            int month = GetMonth();
            return month > 0 && month < 13;
        }

        private static bool IsLeapYear(int year)
        {
            return year % 4 == 0 && year % 100 != 0 || year % 400 == 0;
        }
        private int GetDay()
        {
            return Convert.ToInt32(_idCode.Substring(5, 2));
        }

        private bool IsValidDay()
        {
            int day = GetDay();
            int month = GetMonth();
            int maxDays = 31;
            if (new List<int> { 4, 6, 9, 11 }.Contains(month))
            {
                maxDays = 30;
            }
            if (month == 2)
            {
                if (IsLeapYear(GetFullYear()))
                {
                    maxDays = 29;
                }
                else
                {
                    maxDays = 28;
                }
            }
            return 0 < day && day <= maxDays;
        }

        private int CalculateControlNumberWithWeights(int[] weights)
        {
            int total = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                total += Convert.ToInt32(_idCode.Substring(i, 1)) * weights[i];
            }
            return total;
        }

        private bool IsValidControlNumber()
        {
            int controlNumber = Convert.ToInt32(_idCode[^1..]);
            int[] weights = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1 };
            int total = CalculateControlNumberWithWeights(weights);
            if (total % 11 < 10)
            {
                return total % 11 == controlNumber;
            }
            // second round
            int[] weights2 = { 3, 4, 5, 6, 7, 8, 9, 1, 2, 3 };
            total = CalculateControlNumberWithWeights(weights2);
            if (total % 11 < 10)
            {
                return total % 11 == controlNumber;
            }
            // third round, control number has to be 0
            return controlNumber == 0;
        }

        public bool IsValid()
        {
            return IsValidLength() && ContainsOnlyNumbers()
                && IsValidGenderNumber() && IsValidMonth()
                && IsValidDay()
                && IsValidControlNumber();
        }

        public DateOnly GetBirthDate()
        {
            int day = GetDay();
            int month = GetMonth();
            int year = GetFullYear();
            return new DateOnly(year, month, day);
        }
    }
}
