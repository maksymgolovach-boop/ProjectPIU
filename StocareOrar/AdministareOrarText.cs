using LibrarieModele;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zile.Core;

namespace NivelStocareDate
{
    public class AdministareOrarText : IstocareDateOrar
    {
        private const string SEPARATOR_FISIER = ";";
        private const string SEPARATOR_SECUNDAR_FISIER = " ";

        private const int DAY_POS = 0;
        private const int SCHED_ACTIVITY_POS = 1;

        private string numeFisier;
        private readonly IstocareDateActivities activitati;

        public AdministareOrarText(string numeFisier, IstocareDateActivities activitati) // constructor de fisier
        {
            this.numeFisier = numeFisier;
            this.activitati = activitati;
            Stream stream = File.Open(numeFisier, FileMode.OpenOrCreate);
            if (stream == null)
                throw new FileLoadException($"Nu sa putut a deschide fisierul cu numele {numeFisier}");
            stream.Close();
        }

        public Dictionary<WeekDays, List<Scheduled_activity>> GetOrar()
        {
            Dictionary<WeekDays, List<Scheduled_activity>> orar = new Dictionary<WeekDays, List<Scheduled_activity>>();
            foreach (WeekDays day in Enum.GetValues(typeof(WeekDays)))
            {
                orar[day] = new List<Scheduled_activity>();
            }

            using (StreamReader file = new StreamReader(numeFisier))
            {
                string liniefisier;
                string[] linie;
                while((liniefisier = file.ReadLine()) != null )
                {
                    linie = liniefisier.Split(SEPARATOR_SECUNDAR_FISIER);
                    Enum.TryParse(linie[DAY_POS], out WeekDays zi);

                    orar[zi].Add(new Scheduled_activity(linie[SCHED_ACTIVITY_POS]));
                }
            }
            return orar;
        }
        public Dictionary<Guid, Activitate> GetActivitiesList()
        {
            return activitati.GetActivities();
        }

        public Activitate? ActivityByID(Guid id)
        {
            return activitati.GetActivity(id);
        }
        //adaugare activitate in orar
        // Posibil sa apare un conflict daca scheduled_activity exista dar informatia totala - nu, aceasta problema se va reusi daca Scheduled_activity va primi ca argument activitatea completa
        public void add_ScheduledActivity_toSchedule(Scheduled_activity SchedActivity, WeekDays day)
        {
            using(StreamReader file = new StreamReader(numeFisier))
            {
                string liniefisier;
                string[] continut;
                while((liniefisier = file.ReadLine()) != "\n")
                {
                    continut = liniefisier.Split(SEPARATOR_SECUNDAR_FISIER);
                    Enum.TryParse(continut[0], out WeekDays zi);
                    if (zi != day)
                        continue;
                    if (new Scheduled_activity(continut[SCHED_ACTIVITY_POS]).IfOverlap(SchedActivity))
                        throw new Exception($"Activitatile se suprapun!");
                }
            }
            using (StreamWriter file = new StreamWriter(numeFisier, true))
            {
                file.WriteLine(SchedActivity.ConversiePentruScriereFisier());
            }
        }

        //adaugare din lista de activitati la un interval dat
        public void add_activity_fromList(Guid ID, TimeOnly start, TimeOnly end, WeekDays day)
        {
            Scheduled_activity sactivity;
            if (activitati.GetActivity(ID) == null)
                throw new ArgumentException($"Activitatea cu ID {ID} nu exista in lista de activitati!");
            sactivity = new Scheduled_activity(ID, start, end);
            using (StreamReader file = new StreamReader(numeFisier)) // cautam daca exista suprapuneri intre activitati in ziua respectiva
            {
                string liniefisier;
                string[] continut;
                while ((liniefisier = file.ReadLine()) != "\n" && (liniefisier = file.ReadLine()) != null)
                {
                    continut = liniefisier.Split(SEPARATOR_SECUNDAR_FISIER);
                    Enum.TryParse(continut[0], out WeekDays zidorita);
                    if (zidorita != day)
                        continue;
                    if (new Scheduled_activity(continut[SCHED_ACTIVITY_POS]).IfOverlap(sactivity))
                        throw new Exception($"Activitatile se suprapun!");
                }
            }
            using (StreamWriter file = new StreamWriter(numeFisier, true))
            {
                file.WriteLine(day + SEPARATOR_SECUNDAR_FISIER + sactivity.ConversiePentruScriereFisier());
            }
        }
        public string getOrarStr()
        {
            string buffer = "";
            Dictionary<WeekDays, List<Scheduled_activity>> orar = this.GetOrar();
            foreach (var key in orar.Keys)
            {
                buffer += "<---------- " + key.ToString() + " ---------->\n";
                foreach (Scheduled_activity act in orar[key])
                {
                    buffer += act.INFO(activitati.GetActivity(act.ID))  + "\n";
                }
            }
            return buffer;
        }
        public void RemoveActivitiesFromDay(Guid ID_toremove, WeekDays day)
        {
            var buffer = File.ReadAllLines(numeFisier);
            string day_id = day.ToString() + SEPARATOR_SECUNDAR_FISIER + ID_toremove.ToString();

            var newlines = buffer.Where(linie => !linie.StartsWith(day_id)).ToList();
            Console.WriteLine(newlines.Count);
            File.WriteAllLines(numeFisier, newlines);
        }
        public void RemoveAllActivities(Activitate activity) // functia care va sterge toate activitatile din orar care match cu activitatea de la argument
        {
            string ID_toremove = activity.ID.ToString();
            var buffer = File.ReadAllLines(numeFisier);
            var newlines = buffer.Where(linie =>
            {
                if(string.IsNullOrEmpty(linie)) return false;
                string[] campuri = linie.Split(SEPARATOR_SECUNDAR_FISIER);
                return !campuri[1].StartsWith(ID_toremove);
            }).ToList();
            File.WriteAllLines(numeFisier, newlines);
        }

    }
}
