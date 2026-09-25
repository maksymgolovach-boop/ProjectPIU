using LibrarieModele;
using WeekDays = LibrarieModele.enums.WeekDays;

namespace NivelStocareDate
{
    //clasa activitatilor in saptamana adica oraru nostrul
    public class AdministrareOrarMemorie : IstocareDateOrar
    {
        private const string SEPARATOR_FISIER = ";";
        private const string SEPARATOR_SECUNDAR_FISIER = " ";


        private readonly IstocareDateActivities activitatiList;

        public Dictionary<WeekDays, List<Scheduled_activity>> scheduled_week;
        /// sa nu uiti a modifica constructorul!!!
        public AdministrareOrarMemorie(IstocareDateActivities activities)
        {
            activitatiList = activities;
            scheduled_week = new Dictionary<WeekDays, List<Scheduled_activity>>(); // orarul de activitati
            foreach (WeekDays day in Enum.GetValues(typeof(WeekDays)))
            {
                scheduled_week[day] = new List<Scheduled_activity>();
            }
        }

        public AdministrareOrarMemorie(Dictionary<Guid, Activitate> old_activities) // constructor de copiere a listei de activitati
        {
            activitatiList = new AdministrareActivitatiMemorie(old_activities);
            scheduled_week = new Dictionary<WeekDays, List<Scheduled_activity>>();
        }
        public Dictionary<WeekDays, List<Scheduled_activity>> GetOrar()
        {
            return scheduled_week;
        }

        //adaugare activitate dupa rand in orar
        public void add_ScheduledActivity_toSchedule(Scheduled_activity SchedActivity, WeekDays day)
        {
            //cauta daca exista suprapuneri cu activitatea
            foreach (Scheduled_activity sactivity in scheduled_week[day])
            {
                if (sactivity.IfOverlap(SchedActivity))
                    throw new Exception($"Activitatea din orar {activitatiList.GetActivity(sactivity.ID)} " +
                        $"se suprapune cu {activitatiList.GetActivity(SchedActivity.ID)} orar");
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
        public void RemoveActivitiesFromDay(Guid ID_toremove, WeekDays day)
        {
            scheduled_week[day].RemoveAll(s => s.ID == ID_toremove);
        }

        public void RemoveAllActivities(Activitate activitytoremove)
        {
            Guid id = activitytoremove.ID;
            foreach (var key in scheduled_week.Keys)
            {
                scheduled_week[key].RemoveAll(activity => activity.ID == id);
            }
        }

        public void RemoveActivityFromDay(Scheduled_activity sched, WeekDays day)
        {
            scheduled_week[day].Remove(sched);
        }

        //adaugare din lista de activitati la intervalul dat
        public void add_activity_fromList(Guid ID, TimeOnly start, TimeOnly end, WeekDays day)
        {
            Activitate? activity = activitatiList.GetActivity(ID);
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
    }
}
