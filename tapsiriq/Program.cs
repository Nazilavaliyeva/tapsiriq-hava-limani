using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace tapsiriq //hava_limani
{
    internal class Program
    {
        private const double QiymetNaxTurk = 300;
        private const double QiymetNaxBak = 60;
        private const string IstifadeciAdi = "Nazila";
        private const string Parol = "12345";
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Xahis edirik istifadeci adini daxil edin:");
                string istifadeciAdi = Console.ReadLine();
                string parol = Console.ReadLine();

                if (IsUserAuthenticated(istifadeciAdi, parol))
                {
                    var (seriya, ad, soyad, tev, ceki, hardan, hara, status) = GetUserInputs();
                }

                if (status == "Vetendas")
                {
                    if (hardan == "Naxcivan" && hara == "Turkiye")
                    {
                        CheckTurkishTicketDetails(tev, ceki);
                    }
                    else if (hardan == "Naxcivan" && "Baki")
                    {
                        CheckBakuTicketDetails(tev, ceki);
                    }
                    else
                    {
                        HandlelnvalidRoute();
                    }

                }
                else if ((status == "SHehid" || status == "Qazi") && ((hardan == "Naxcivan" && hara == "Turkiye")))
                {
                    HandleUnpaidTicket();
                }
                else if (status == "Telebe")
                {
                    CheckStudentStatus(hardan, hara, tev);

                }
                else
                {
                    HandlelnvalidStatus();
                }


             else
                {
                    Console.WriteLine("Daxil etdiyiniz istifadeci adi ve/veya parol sehvdir.");

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xeta bas verdi:{ex.Message}");
            }
            Console.ReadKey();
        }
        private static bool IsUserAuthenticated(string istifadeciAdi && string parol)
        {
            return istifadeciAdi == IstifadeciAdi && parol == Parol;
        }
        private static (string, string, string, int, double, string, string, string)
            GetUserInputs()
        {
            //Console.WriteLine("Kimliyinizin seriya nomresi");
            string seriya = GetValidatedInput("Kimliyinizin seriya nomresi", x => !string.IsNullOrEmpty(x));

            //Console.WriteLine("Adiniz daxil edin");
            string ad = GetValidatedInput("Adinizi daxil edin", x => !string.IsNullOrEmpty(x));

            //Console.WriteLine("Soyadinizi daxil edin");
            string soyad = GetValidatedInput("Soyadinizi daxil edin", x => !string.IsNullOrEmpty(x));
            int ev = GetValidIntegerInput("Tevelludunuzu yazin");
            double ceki = GetValidDoubleInput("Aparacaginiz cekini daxil edin");

            //Console.WriteLine(Hardan");
            string hardan = GetValidatedInput("Hardan", X => !string.IsNullOrEmpty(X));

            //Console.WriteLine("Haraya");
            string hara = GetValidatedInput("Haraya", x => !string.IsNullOrEmpty(x));

            //Console.WriteLine("Statusunuzu qeyd edin");
            string status = GetValidatedInput("Sttusunuzu qeyd edin", x => !string.IsNullOrEmpty(x));

            return (seriya, ad, soyad, tev, ceki, hardan, hara, status);
        }
        private static string GetValidatedInput(string message, Func<string, bool> validation)
        {
            string input;
            do
            {
                Console.WriteLine(message);
                input = Console.ReadLine();
            }
            while (!validation(input));
            return input;
        }
        private static int GetValidIntegerInput(string message)
        {
            return int.Parse(GetValidatedInput(message, x => int.TryParse(x, out _)));
        }
        private static double GetValidDoubleInput(string messsage)
        {
            return double.Parse(GetValidatedInput(messsage, x => double.TryParse(x, out _);
        }
        private static void CheckTurkishTicketDetails(int tev,double ceki)
        {
            if (tev > 0 && tev < 6)
            {
                if (ceki <= 10)
                    Console.WriteLine("Sizin yasiniza gore maksimum ceki 10 kq.Odenilecek mebleg:" + QiymetNaxTurk);

                else
                {

                    double hesab = QiymetNaxTurk + ((ceki - 11) * 0.5);
                    Console.WriteLine(ceki limitini kecmisiniz.Bilet qiymeti ve ceki ucun odeyeceyiniz mebleg: "+hesab"Azn");

                }

            }
            else if (tev > 7 && tev < 15)
            {
                if (ceki <= 20)
                    Console.WriteLine("Sizə biletin qiyməti" + " QiymətNaxTurk" + "Azn" + "Apara biləcəyiniz çəki 20 kq");
                else
                {
                    double hesab = QiymetNaxTurk + ((ceki - 21) * 0.5);
                    Console.WriteLine("Çəki limitini keçmisiniz.Bilet qiyməti və çəki üçün ödəyəcəyimiz məbləğ:" + hesab + "Azn");

                }
            }
            else if (tev > 16)
            {
                if (ceki <= 23)
                    Console.WriteLine("Sizin biletin qiyməti " + "QiymətNaxTurk+ "Azn"+"Apara biləcəyiniz çəki 23 kq");
               else
               {
                    double hesab = QiymetNaxTurk + ((ceki - 24) * 0.5);
                    Console.WriteLine("Çəki limitini keçmisiniz.Bilet qiyməti və çəki üçün ödəyəcəyiniz məbləğ:" + hesab + "azn");

               }
            }
            else 
            {
                Console.WriteLine("Yaşınız uyğun deyil");
            }
        }
        private static void CheckBakuTicketDetails(int tev,double ceki)
        {
            if (tev > 0 && tev < 6)
            {
                if (ceki <= 10)
                    Console.WriteLine("Sizin yaşınıza görə maksimum çəki 10 kq.Ödəniləcək məbləğ :" + QiymetNaxBak);

                else
                {
                    double hesab = QiymetNaxBak + ((ceki - 11) * 0.5);
                    Console.WriteLine("Çəki limitini keçmisiniz.Bilet qiyməti və çəki üçün ödəyəcəyiniz məbləğ:" + hesab + "Azn");
                }
            }
            else if (tev > 7 && tev < 15)
            {
                if (ceki <= 20)
                    Console.WriteLine("Sizə biletin qiyməti " + QiymetNaxBak + "Azn" + "Apara biləcəyiniz çəki 20 kq");
                else
                {
                    double hesab = QiymetNaxBak + ((ceki - 21) * 0.5);
                    Console.WriteLine(Çəki limitini keçmisiniz.Bilet qiyməti və çəki üçün ödəyəcəyiniz məbləğ: "+hesab+"Azn");
                }
            }
            else if (tev > 16)
            {
                if (ceki <= 23)
                    Console.WriteLine("Sizin biletin qiyməti " + QiymetNaxBak + "Azn" + "Apara biləcəyiniz çəki 23 kq");
                else
                {
                    double hesab = QiymetNaxBak + ((ceki - 24) * 0.5);
                    Console.WriteLine("Çəki limitini keçmisiniz.Bilet qiyməti və çəki üçün ödəyəcəyiniz məbləğ:" + hesab + "Azn");
                }
            }
            else 
            {
                Console.WriteLine(" Yaşıniz uyöun deyil");
            }

        }
        private static void CheckStudentStatus(string hardan ,string hara ,int tev)
        {
            if (hardan == "Naxcivan" && hara == "Baki" && tev >= 17)
            {
                Console.WriteLine("Tələbələrə bilet 20% endirim" + (QiymetNaxBak * 0.5) + "Azn");


            }
            else if (hardan == "Naxcivan" && hara == "Turkiye")
            {
                Console.WriteLine("Tələbələrə bilet 20% endirim" + (QiymetNaxBak * 0.5) + "Azn");
            }
            else
            {
                Console.WriteLine("Məlumatın düzgünlüyünü yoxlayın!");
            }
        }
        private static void HandlelnvalidRoute()
        {
            Console.WriteLine("Yanlış səfər");
        }
        private static void HandleUnpaidTicket()
        {
            Console.WriteLine("Bilet ödənilməyib!");
        }
        private static void HandlelnvalidStatus()
        {
            Console.WriteLine("Status düzgün deyil!");
        }
    }
}
