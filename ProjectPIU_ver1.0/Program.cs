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
            try
            {
                Activitate activitate1 = new Activitate("Sala", "Antrenarea pentru sanatate", "Sanatate curata");
                sapt.add_activityToList(activitate1);

                sapt.add_activity_fromList(1, new TimeOnly(13, 00), new TimeOnly(14, 00), WeekDays.Monday);
                activitate1 = new Activitate("Drocika", "Jestkaya drocika pe ceava", "Sanatate");
                sapt.add_activityToList(activitate1);

                sapt.add_activity_fromList(2, new TimeOnly(14, 00), new TimeOnly(16, 00), WeekDays.Monday);

                Console.WriteLine(sapt.show_orar());
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
            var found_activity = orar.activities.Values.Where(a => a.name == nume);
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
