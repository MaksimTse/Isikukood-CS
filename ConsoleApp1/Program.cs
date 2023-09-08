using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main()
        {
            Random random = new Random();
            for (int i = 0; i < 20; i++)
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
                }
                else
                {
                    Console.WriteLine("Vale IdCode.\n");
                }
            }
        }

        public static string GenerateRandomIdCode(Random random)
        {
            // Генерация случайного IdCode
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
                return "Välismaал";
            }
        }
    }
}