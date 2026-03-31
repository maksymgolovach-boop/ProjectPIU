using System.ComponentModel;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

using Activitateclase;
using StocareOrar;
using Zile.Core;

namespace program
{
    public class program
    {
        public static void Main(string[] args)
        {
            Orar sapt = new Orar();

            TimeOnly start, stop;
            string optiune, nume;
            Activitate? activitate1 = null;
            int day, id;
            try
            {
                do
                {
                    Console.WriteLine("C. Citire informatii activitate de la tastatura");
                    Console.WriteLine("I. Afisarea informatiilor despre ultima activitate introdusa");
                    Console.WriteLine("A. Afisare activitati din lista");
                    Console.WriteLine("S. Salvare activitate in lista");
                    Console.WriteLine("L. Introducere activitate in orar dupa ID");
                    Console.WriteLine("F. Introducere activitate in orar dupa Nume");
                    Console.WriteLine("O. Afisare orar");
                    Console.WriteLine("X. Inchidere program");

                    Console.WriteLine("Alegeti o optiune");
                    optiune = Console.ReadLine()?.ToUpper() ?? string.Empty;

                    switch (optiune)
                    {
                        case "C":
                            activitate1 = citireactivitate();
                            break;

                        case "I":
                            if (activitate1 != null)
                                Console.WriteLine(activitate1.INFO());
                            break;

                        case "A":
                            Console.WriteLine(sapt.activitatiList.show_activitylist());
                            break;

                        case "S":
                            if (activitate1 != null)
                            {
                                sapt.activitatiList.add_activityToList(activitate1);
                                Console.WriteLine("activitate salvata.");
                            }
                            else
                            {
                                Console.WriteLine("Activitatea nu a fost initializata");
                            }
                            break;

                        case "L":
                            Console.Write("ID-ul activitati:");
                            id = Int32.Parse(Console.ReadLine());
                            Console.Write("Introduceti numarul zilei[1,2,3,4,5,6,7]: ");
                            day = Int32.Parse(Console.ReadLine());
                            Console.Write("Introduceti ora de inceput a activitatii: ");
                            start = TimeOnly.Parse(Console.ReadLine());
                            Console.Write("Introduceti ora de sfarsit a activitatii: ");
                            stop = TimeOnly.Parse(Console.ReadLine());

                            sapt.add_activity_fromList(id, start, stop, (WeekDays)day);
                            break;

                        case "F":
                            Console.Write("Numele activitati:");
                            nume = Console.ReadLine();
                            Console.Write("Introduceti numarul zilei[1,2,3,4,5,6,7]: ");
                            day = Int32.Parse(Console.ReadLine());
                            Console.Write("Introduceti ora de inceput a activitatii: ");
                            start = TimeOnly.Parse(Console.ReadLine());
                            Console.Write("Introduceti ora de sfarsit a activitatii: ");
                            stop = TimeOnly.Parse(Console.ReadLine());

                            adaugaActivitateDupanume(nume, sapt, start, stop, (WeekDays)day);
                            break;

                        case "O":
                            Console.WriteLine(sapt.show_orar());
                            break;

                        case "X":
                            Console.WriteLine("Aplicatia va fi inchisa");
                            return;

                        default:
                            Console.WriteLine("Optiune inexistenta");
                            break;
                    }

                } while (optiune.ToUpper() != "X");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        // citire activitate de la tastatura
        public static Activitate citireactivitate()
        {
            string nume, descriere, tip;
            Console.WriteLine("Introduceti numele activitatii: ");
            nume = Console.ReadLine();
            Console.WriteLine("Introduceti descrierea activitatii: ");
            descriere = Console.ReadLine();
            Console.WriteLine("Introduceti tipul activitatii: ");
            tip = Console.ReadLine();
            return new Activitate(nume, descriere, tip);
        }

        // citire activitate cu interval de la tastatura
        public static Scheduled_activity citire_Scheduledactivity(Dictionary <int, Activitate> orar) 
        {
            string nume, descriere, tip;
            TimeOnly start, stop;

            Console.WriteLine("Introduceti numele activitatii: ");
            nume = Console.ReadLine();
            Console.WriteLine("Introduceti descrierea activitatii: ");
            descriere = Console.ReadLine();
            Console.WriteLine("Introduceti tipul activitatii: ");
            tip = Console.ReadLine();

            Activitate new_activitate = new Activitate(nume, descriere, tip);
            orar.Add(new_activitate.ID, new_activitate);

            Console.WriteLine("Introduceti timpul de inceput a activitatii: ");
            start = TimeOnly.Parse(Console.ReadLine());
            Console.WriteLine("Introduceti timpul de sfarsit a activitatii: ");
            stop = TimeOnly.Parse(Console.ReadLine());

            return new Scheduled_activity(new_activitate.ID, start, stop);
        }

        // adauga activitate din lista dupa nume
        public static void adaugaActivitateDupanume(string nume, Orar orar, TimeOnly start, TimeOnly stop, WeekDays day)
        {
            var found_activity = orar.activitatiList.activities.Values.Where(a => a.name == nume);
            if (found_activity == null)
                throw new ArgumentException($"Nu a fost gasita activitatea {nume} in lista de activitati");
            else if (found_activity.Count() != 1)
            {
                Console.WriteLine("Au fost gasite mai multe activitati cu acelasi nume, alegeti pe cea dorita");
                foreach (var activity in found_activity)
                    Console.WriteLine($"Activitatea {activity.ID} cu numele {activity.name} de tipul {activity.type}\n" +
                        $"Descrierea: {activity.description}");
                Console.Write("Introduceti numarul activitatii dorite:");
                int _id = int.Parse(Console.ReadLine());
                orar.add_activity_fromList(_id, start, stop, day);
            }
            else
            {
                orar.add_activity_fromList(found_activity.First().ID, start, stop, day);
            }
        }
    }
}
