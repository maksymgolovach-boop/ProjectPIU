using Activitateclase;
using WeekDays = Zile.Core.WeekDays;

namespace StocareOrar
{
    //clasa activitatilor in saptamana adica oraru nostrul
    public class Orar
    {
        public Dictionary<int, Activitate> activities; // lista cu activitati disponibile cu posibilitatea de adaugare a noii activitati

        public Dictionary<WeekDays, List<Scheduled_activity>> scheduled_week;

        public Orar()
        {
            activities = new Dictionary<int, Activitate>(); // dictionar cu activitati

            scheduled_week = new Dictionary<WeekDays, List<Scheduled_activity>>(); // orarul

            foreach (WeekDays day in Enum.GetValues(typeof(WeekDays)))
            {
                scheduled_week[day] = new List<Scheduled_activity>();
            }
        }
        public Orar(Dictionary<int, Activitate> old_activities) // constructor de copiere a listei de activitati
        {
            activities = new Dictionary<int, Activitate>(old_activities); // copierea listei de activitati
            scheduled_week = new Dictionary<WeekDays, List<Scheduled_activity>>();
        }

        public void add_activityToList(Activitate activitate) // ada
        {
            if (activities.Keys.Contains(activitate.ID))
            {
                throw new Exception("Activitatea deja exista in lista!!!");
            }
            else
            {
                activities.Add(activitate.ID, activitate);
            }
        }
        public void remove_activitybyID(int IDtoRemove) // remove activitatea din dupa ID
        {
            if (activities.ContainsKey(IDtoRemove))
            {
                activities.Remove(IDtoRemove);
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
                    throw new Exception($"Activitatea din orar {activities[sactivity.ID]} " +
                        $"se suprapune cu {activities[SchedActivity.ID]} orar");
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
            var activity = activities[ID];
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
                    buffer += act.INFO(activities[act.ID]) + "\n";
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

        
    }
}
