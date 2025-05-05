using System;

namespace Tahmaz_hava_limani_tapsiriq
{
    internal class Program
    {
        private const double QiymetNaxTurk = 300;
        private const double QiymetNaxBak = 60;
        private const string IstifadeciAdi = "Nazile";
        private const string Parol = "123456";
        private static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Xahiş edirik istifadəçi adını və parolu daxil edin.");
                string istifadeciAdi = Console.ReadLine();
                string parol = Console.ReadLine();
                if (IsUserAuthenticated(istifadeciAdi, parol))
                {
                    var (seriya, ad, soyad, tev, ceki, hardan, hara, status) = GetUserInputs();
                    if (status == "Vetendas")
                    {
                        if (hardan == "Naxcivan" && hara == "Turkiye")
                        {
                            CheckTurkishTicketDetails(tev, ceki);
                        }
                        else if (hardan == "Naxcivan" && hara == "Baki")
                        {
                            CheckBakuTicketDetails(tev, ceki);
                        }
                        else
                        {
                            HandleInvalidRoute();
                        }
                    }
                    else if ((status == "Shehid" || status == "Qazi") && ((hardan == "Naxcivan" && hara == "Baki") || (hardan == "Naxcivan" && hara == "Turkiye")))
                    {
                        HandleUnpaidTicket();
                    }
                    else if (status == "Telebe")
                    {
                        CheckStudentStatus(hardan, hara, tev);
                    }
                    else
                    {
                        HandleInvalidStatus();
                    }
                }
                else
                {
                    Console.WriteLine("Daxil etdiyiniz istifadəçi adı və/veya parol səhvdir.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta baş verdi: {ex.Message}");
            }
            Console.ReadKey();
        }
        private static bool IsUserAuthenticated(string istifadeciAdi, string parol)
        {
            return istifadeciAdi == IstifadeciAdi && parol == Parol;
        }
        private static (string, string, string, int, double, string, string, string) GetUserInputs()
        {
            string seriya = GetValidatedInput("Kimliyinizin seriya nomresi", x => !string.IsNullOrEmpty(x));   
            string ad = GetValidatedInput("Adinizi daxil edin", x => !string.IsNullOrEmpty(x));
            string soyad = GetValidatedInput("Soyadinizi daxil edin", x => !string.IsNullOrEmpty(x));
            int tev = GetValidIntegerInput("Tevelludunuzu yazin");
            double ceki = GetValidDoubleInput("Aparacaginiz cekini daxil edin");
            string hardan = GetValidatedInput("Hardan", x => !string.IsNullOrEmpty(x));
            string hara = GetValidatedInput("Haraya", x => !string.IsNullOrEmpty(x));
            string status = GetValidatedInput("Statusunuzu qeyd edin", x => !string.IsNullOrEmpty(x));
            return (seriya, ad, soyad, tev, ceki, hardan, hara, status);
        }

        private static string GetValidatedInput(string message, Func<string, bool> validation)
        {
            string input;
            do
            {
                Console.WriteLine(message);
                input = Console.ReadLine();
            } while (!validation(input));

            return input;
        }

        private static int GetValidIntegerInput(string message)
        {
            return int.Parse(GetValidatedInput(message, x => int.TryParse(x, out _)));
        }

        private static double GetValidDoubleInput(string message)
        {
            return double.Parse(GetValidatedInput(message, x => double.TryParse(x, out _)));
        }

        private static void CheckTurkishTicketDetails(int tev, double ceki)
        {
            if (tev > 0 && tev < 6)
            {
                if (ceki <= 10)
                    Console.WriteLine("Sizin yaşınıza görə maksimum çəki 10 kq. Ödəniləcək məbləğ: " + QiymetNaxTurk);
                else
                {
                    double hesab = QiymetNaxTurk + ((ceki - 11) * 0.5);
                    Console.WriteLine("Çəki limitini keçmisiniz. Bilet qiyməti və çəki üçün ödəyəcəyiniz məbləğ: " + hesab + " Azn");
                }
            }
            else if (tev > 7 && tev < 15)
            {
                if (ceki <= 20)
                    Console.WriteLine("Size biletin qiyməti " + QiymetNaxTurk + " Azn" + "Apara biləcəyiniz çəki 20 kq");
                else
                {
                    double hesab = QiymetNaxTurk + ((ceki - 21) * 0.5);
                    Console.WriteLine("Çəki limitini keçmisiniz. Bilet qiyməti və çəki üçün ödəyəcəyiniz məbləğ: " + hesab + " Azn");
                }
            }
            else if (tev > 16)
            {
                if (ceki <= 23)
                    Console.WriteLine("Sizin biletin qiyməti " + QiymetNaxTurk + " Azn" + " Apara biləcəyiniz çəki 23 kq");
                else
                {
                    double hesab = QiymetNaxTurk + ((ceki - 24) * 0.5);
                    Console.WriteLine("Çəki limitini keçmisiniz. Bilet qiyməti və çəki üçün ödəyəcəyiniz məbləğ: " + hesab + " Azn");
                }
            }
            else
            {
                Console.WriteLine("Yaşınız uyğun deyil");
            }
        }

        private static void CheckBakuTicketDetails(int tev, double ceki)
        {
            if (tev > 0 && tev < 6)
            {
                if (ceki <= 10)
                    Console.WriteLine("Sizin yaşınıza görə maksimum çəki 10 kq. Ödəniləcək məbləğ: " + QiymetNaxBak);
                else
                {
                    double hesab = QiymetNaxBak + ((ceki - 11) * 0.5);
                    Console.WriteLine("Çəki limitini keçmisiniz. Bilet qiyməti və çəki üçün ödəyəcəyiniz məbləğ: " + hesab + " Azn");
                }
            }
            else if (tev > 7 && tev < 15)
            {
                if (ceki <= 20)
                    Console.WriteLine("Size biletin qiyməti " + QiymetNaxBak + " Azn" + "Apara biləcəyiniz çəki 20 kq");
                else
                {
                    double hesab = QiymetNaxBak + ((ceki - 21) * 0.5);
                    Console.WriteLine("Çəki limitini keçmisiniz. Bilet qiyməti və çəki üçün ödəyəcəyiniz məbləğ: " + hesab + " Azn");
                }
            }
            else if (tev > 16)
            {
                if (ceki <= 23)
                    Console.WriteLine("Sizin biletin qiyməti " + QiymetNaxBak + " Azn" + " Apara biləcəyiniz çəki 23 kq");
                else
                {
                    double hesab = QiymetNaxBak + ((ceki - 24) * 0.5);
                    Console.WriteLine("Çəki limitini keçmisiniz. Bilet qiyməti və çəki üçün ödəyəcəyiniz məbləğ: " + hesab + " Azn");
                }
            }
            else
            {
                Console.WriteLine("Yaşınız uyğun deyil");
            }
        }

        private static void CheckStudentStatus(string hardan, string hara, int tev)
        {
            if (hardan == "Naxcivan" && hara == "Baki" && tev >= 17)
            {
                Console.WriteLine("Tələbələrə bilet 20% endirim " + (QiymetNaxBak * 0.5) + " Azn");
            }
            else if (hardan == "Naxcivan" && hara == "Türkiye")
            {
                Console.WriteLine("Tələbələrə bilet 20% endirim " + (QiymetNaxTurk * 0.5) + " Azn");
            }
            else
            {
                Console.WriteLine("Məlumatin düzgünlüyünü yoxlayın!");
            }
        }

        private static void HandleInvalidRoute()
        {
            Console.WriteLine("Yanlış səfər!");
        }

        private static void HandleUnpaidTicket()
        {
            Console.WriteLine("Bilet ödənilməyib!");
        }

        private static void HandleInvalidStatus()
        {
            Console.WriteLine("Status düzgün deyil!");
        }
    }
}
