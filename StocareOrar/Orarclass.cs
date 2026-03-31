using Activitateclase;
using WeekDays = Zile.Core.WeekDays;

namespace StocareOrar
{
    //clasa activitatilor in saptamana adica oraru nostrul
    public class Orar
    {
        private const string SEPARATOR_FISIER = ";";
        private const string SEPARATOR_SECUNDAR_FISIER = " ";
        private const string SEPARATOR_TERTIAR = "-";

        private const int DAY_POS = 0;
        private const int ID_SCHEDULED_ACTIVITY_POS = 1;
        private const int START_TIME_POS = 2;
        private const int END_TIME_POS = 3;

        public Activitati activitatiList;
        public Dictionary<WeekDays, List<Scheduled_activity>> scheduled_week;

        public Orar()
        {
            activitatiList = new Activitati();
            scheduled_week = new Dictionary<WeekDays, List<Scheduled_activity>>(); // orarul de activitati

            foreach (WeekDays day in Enum.GetValues(typeof(WeekDays)))
            {
                scheduled_week[day] = new List<Scheduled_activity>();
            }
        }
        /// Completeaza!!!
        public Orar(string sirfisier) // counstructor la citire din fisier
        {
            string[] strPrincipal = sirfisier.Split(SEPARATOR_SECUNDAR_FISIER);
            string[] strActivitati = strPrincipal[0].Split(SEPARATOR_FISIER);
        }

        public Orar(Dictionary<int, Activitate> old_activities) // constructor de copiere a listei de activitati
        {
            activitatiList = new Activitati(old_activities);
            scheduled_week = new Dictionary<WeekDays, List<Scheduled_activity>>();
        }

        public void remove_activitybyID(int IDtoRemove) // elimina toate activitatile cu acelasi ID din orar dupa ID
        {
            if (activitatiList.activities.ContainsKey(IDtoRemove))
            {
                activitatiList.activities.Remove(IDtoRemove);
                foreach (var day in scheduled_week.Keys)
                {
                    scheduled_week[day].RemoveAll(s => s.ID == IDtoRemove);
                }
            }
            else
                throw new Exception("Activitatea nu exista in lista de activitati, deci nu poate fi eliminata");
        }

        //adaugare activitate dupa rand in orar
        public void add_ScheduledActivity_toSchedule(Scheduled_activity SchedActivity, WeekDays day)
        {
            //cauta daca exista suprapuneri cu activitatea
            foreach (Scheduled_activity sactivity in scheduled_week[day])
            {
                if (sactivity.IfOverlap(SchedActivity))
                    throw new Exception($"Activitatea din orar {activitatiList.activities[sactivity.ID]} " +
                        $"se suprapune cu {activitatiList.activities[SchedActivity.ID]} orar");
            }
            //cauta la care pozitie trebuie inserata
            int pos = 0;
            foreach (Scheduled_activity sactivity in scheduled_week[day])
            {
                if (sactivity.start_time > SchedActivity.start_time)
                    pos++;
                else
                    break;
            }
            scheduled_week[day].Insert(pos, SchedActivity);
        }

        // elimina activitatea de la intertvalul din orar
        public void remove_ScheduledActivity_fromSchedule(int ID_toremove, WeekDays day)
        {
            scheduled_week[day].RemoveAll(s => s.ID == ID_toremove);
        }

        //adaugare din lista de activitati la intervalul dat
        public void add_activity_fromList(int ID, TimeOnly start, TimeOnly end, WeekDays day)
        {
            var activity = activitatiList.activities[ID];
            if (activity == null)
            {
                throw new ArgumentException("Activitatea nu a fost gasita!");
            }

            //cauta daca exista suprapuneri cu activitatea
            foreach (Scheduled_activity sactivity in scheduled_week[day])
            {
                if (sactivity.IfOverlap(new Scheduled_activity(ID, start, end)))
                    throw new Exception($"Intervalul {start} - {end} la ziua {day} este ocupat!");
            }
            //cauta la care pozitie trebuie inserata
            int pos = 0;
            foreach (Scheduled_activity sactivity in scheduled_week[day])
            {
                if (sactivity.start_time < start)
                    pos++;
                else
                    break;
            }
            scheduled_week[day].Insert(pos, new Scheduled_activity(ID, start, end));
        }

        //returneaza sirul de caractere orarului
        public string show_orar()
        {
            string buffer = "";
            foreach (var key in scheduled_week.Keys)
            {
                buffer += "<---------- " + key.ToString() + " ---------->\n";
                foreach (Scheduled_activity act in scheduled_week[key])
                {
                    buffer += act.INFO(activitatiList.activities[act.ID]) + "\n";
                }
            }
            return buffer;
        }

        // Cuarata intervalul de timp dat in orar
        public void clearinterval(TimeOnly inceput, TimeOnly sfarsit, WeekDays day)
        {
            var testSActivity = new Scheduled_activity(0, inceput, sfarsit);
            scheduled_week[day].RemoveAll(a => a.IfOverlap(testSActivity));
        }

        public string ConversiaLaString()
        {
            string SirPentruFisier="", List_pt_fisier="", orar_pt_fifiser="";
            foreach(Activitate act in activitatiList.activities.Values)
            {
                List_pt_fisier += act.ConversiePentruScriereFisier() + SEPARATOR_SECUNDAR_FISIER;
            }
            foreach(var day in scheduled_week.Keys)
            {
                orar_pt_fifiser += day.ToString() + SEPARATOR_TERTIAR;
                foreach(Scheduled_activity act in scheduled_week[day])
                {
                    orar_pt_fifiser += act.ID + SEPARATOR_FISIER + act.start_time + SEPARATOR_FISIER + act.end_time;
                }
            }
            SirPentruFisier = List_pt_fisier + SEPARATOR_SECUNDAR_FISIER + orar_pt_fifiser;
            return SirPentruFisier;
        }
    }
}
