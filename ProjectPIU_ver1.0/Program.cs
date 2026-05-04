using System.ComponentModel;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

using LibrarieModele;
using LibrarieModele.enums;
using NivelStocareDate;

namespace Program
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IstocareDateActivities activities = ManagerStocare.GetAdministratorStocareActivitati();
            IstocareDateOrar adminOrar = ManagerStocare.GetAdministratorStocareOrar(activities);
            
            TimeOnly start, stop;
            string optiune, nume;
            Activitate? activitate1 = null;
            int day; 
            Guid id;
            try
            {
                do
                {
                    Console.WriteLine("C. Citire informatii activitate de la tastatura");
                    Console.WriteLine("I. Afisarea informatiilor despre ultima activitate introdusa");
                    Console.WriteLine("A. Afisare activitati din lista");
                    Console.WriteLine("S. Salvare activitate in lista");
                    Console.WriteLine("L. Introducere activitate in orar dupa Nume");
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
                            Console.WriteLine(show_activitylist(activities.GetActivitiesValues()));
                            break;
                        case "S":
                            if (activitate1 != null)
                            {
                                activities.add_activityToList(activitate1);
                                Console.WriteLine("activitate salvata.");
                            }
                            else
                            {
                                Console.WriteLine("Activitatea nu a fost initializata");
                            }
                            break;

                        case "LO":
                            Console.WriteLine("Scrieti denumirea activitatii:");
                            nume = Console.ReadLine();
                            id = activities.FindActivitiesByName(nume).First().ID;
                            Console.Write("Introduceti numarul zilei[1,2,3,4,5,6,7]: ");
                            day = Int32.Parse(Console.ReadLine());
                            Console.Write("Introduceti ora de inceput a activitatii: ");
                            start = TimeOnly.Parse(Console.ReadLine());
                            Console.Write("Introduceti ora de sfarsit a activitatii: ");
                            stop = TimeOnly.Parse(Console.ReadLine());

                            adminOrar.add_activity_fromList(id, start, stop, (WeekDays)day);
                            break;

                        case "RA":
                            activities.removeActivity(activitate1);
                            break;

                        case "O":
                            Console.WriteLine(adminOrar.getOrarStr());
                            break;

                        case "R":
                            nume = Console.ReadLine();
                            activitate1 = activities.FindActivitiesByName(nume).First();
                            adminOrar.RemoveAllActivities(activitate1);
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
            Enum.TryParse(tip, out ActivityType type);

            return new Activitate(nume, descriere, type);
        }

        // citire activitate cu interval de la tastatura
        public static Scheduled_activity citire_Scheduledactivity(Dictionary <Guid, Activitate> orar) 
        {
            string nume, descriere, tip;
            TimeOnly start, stop;

            Console.WriteLine("Introduceti numele activitatii: ");
            nume = Console.ReadLine();
            Console.WriteLine("Introduceti descrierea activitatii: ");
            descriere = Console.ReadLine();
            Console.WriteLine("Introduceti tipul activitatii: ");
            tip = Console.ReadLine();
            Enum.TryParse(tip, out ActivityType type);
            Activitate new_activitate = new Activitate(nume, descriere, type);
            orar.Add(new_activitate.ID, new_activitate);

            Console.WriteLine("Introduceti timpul de inceput a activitatii: ");
            start = TimeOnly.Parse(Console.ReadLine());
            Console.WriteLine("Introduceti timpul de sfarsit a activitatii: ");
            stop = TimeOnly.Parse(Console.ReadLine());

            return new Scheduled_activity(new_activitate.ID, start, stop);
        }

        public static string show_activitylist(Dictionary<Guid, Activitate> activities)
        {
            string buffer = "";
            foreach (var key in activities.Keys)
            {
                buffer += $"ID: {key} \n" + activities[key].INFO() + "\n";
            }
            return buffer;
        }        
        public static string show_activitylist(List<Activitate> activities)
        {
            string buffer = "";
            foreach (var activitty in activities)
            {
                buffer += activitty.INFO();
            }
            return buffer;
        }
    }
}
