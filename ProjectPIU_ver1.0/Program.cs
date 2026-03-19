using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Linq;

namespace program
{
    public enum DayOfWeek
    {
        Monday=1, Tuesday=2, Wednesday=3, Thursday=4, Friday=5, Saturday=6, Sunday=7
    }
    public class Activitate
    {
        //instantele clasei
        public string name { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public int ID { get; set; }

        //definirea metodelor
        public Activitate() // Constructorul implicit ai clasei
        {
            name = null;
            description = null;
            type = null;
            ID = 0;
        }
        public Activitate(string _name, string _description, string _tip, List<Activitate> activitati) // Constructorul ai clasei
        {
            name = _name;
            description = _description;
            type = _tip;
            ID = activitati.Count + 1;
        }
        public string INFO()
        {
            return $"Denumirea: {name}\n" +
                $"Descrierea: {description}\n" +
                $"Tip: {type}\n";
        }
        public string getname()
        {
            return name;
        }
    }

    public class Scheduled_activity //clasa ce va contine activitatea cu ora start si ora stop
    {
        public int ID;
        public TimeOnly start_time { get; set; }
        public TimeOnly end_time { get; set; }

        public Scheduled_activity(int activityID, TimeOnly _start_time, TimeOnly _endtime)
        {
            ID = activityID;
            start_time = _start_time;
            end_time = _endtime;
        }
        public bool IfOverlap(Scheduled_activity other)
        {
            if (start_time <= other.end_time && end_time >= other.start_time)
            {
                return true;
            }
            return false;
        }
        public string INFO(Activitate activity)
        {
            return $"{start_time}-{end_time} : {activity.getname()}";
        }
        
    }

    //clasa activitatilor in saptamana adica oraru nostrul
    public class Orar
    {
        public List<Activitate> activities; // lista cu activitati disponibile cu posibilitatea de adaugare a noii activitati
        
        public Dictionary<DayOfWeek, List<Scheduled_activity>> scheduled_week;

        public Orar()
        {
            scheduled_week = new Dictionary<DayOfWeek, List<Scheduled_activity>>();
            activities = new List<Activitate>();

            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                scheduled_week[day] = new List<Scheduled_activity>();
            }
        }
        public Orar(List<Activitate> old_activities) // constructor de copiere a listei de activitati
        {
            activities = old_activities.Select(a => new Activitate { name = a.name, 
                description = a.description, 
                type = a.type, 
                ID = a.ID}).ToList(); // copierea listei de activitati
            scheduled_week = new Dictionary<DayOfWeek, List<Scheduled_activity>>();
        }

        public void add_activityToList(Activitate activitate)
        {
            if (activities.Contains(activitate))
            {
                return;
            }
            else {
                activities.Add(activitate);
            }
        }
        public void remove_activitybyID(int ID) // remove activitatea dupa ID
        {
            foreach (Activitate activitate in activities)
            {
                if (activitate.ID == ID)
                {
                    activities.Remove(activitate);
                }
            }
        }

        //adaugare activitate dupa rand in orar
        public void add_ScheduledActivity_toSchedule(Scheduled_activity SchedActivity, DayOfWeek day) 
        {
            //cauta daca exista suprapuneri cu activitatea
            foreach (Scheduled_activity sactivity in scheduled_week[day])
            {
                if (sactivity.IfOverlap(SchedActivity))
                    return;
            }
            //cauta la care pozitie trebuie inserata
            int pos=0;
            foreach (Scheduled_activity sactivity in scheduled_week[day])
            {
                if (sactivity.start_time > SchedActivity.start_time)
                    pos++;
                else
                    break;
            }
            scheduled_week[day].Insert(pos, SchedActivity);
        }

        // remove activitatea de la intertvalul sau din orar
        public void remove_ScheduledActivity_fromSchedule(int ID, DayOfWeek day) 
        {
            foreach (Scheduled_activity sactivity in scheduled_week[day])
            {
                if (sactivity.ID == ID)
                    scheduled_week[day].Remove(sactivity);
            }
        }

        //adaugare din lista de activitati la intervalul dat
        public void add_activity_fromList(int ID, TimeOnly start, TimeOnly end, DayOfWeek day) 
        {
            var activity = activities.FirstOrDefault(a => a.ID == ID);
            if (activity == null)
            {
                throw new ArgumentException("Activitatea nu a fost gasita!");
            }

            //cauta daca exista suprapuneri cu activitatea
            foreach (Scheduled_activity sactivity in scheduled_week[day])
            {
                if (sactivity.IfOverlap(new Scheduled_activity(ID, start, end)))
                    return;
            }
            //cauta la care pozitie trebuie inserata
            int pos = 0;
            foreach (Scheduled_activity sactivity in scheduled_week[day])
            {
                if (sactivity.start_time > start)
                    pos++;
                else
                    break;
            }
            scheduled_week[day].Insert(pos, new Scheduled_activity(ID, start, end));
        }
    }

    public class program
    {
        public static void Main(string[] args)
        {
            Orar sapt = new Orar();
            /*
            Activitate activitate1 = new Activitate("Acti21", "Loh123", "asdfds", sapt.activities);
            sapt.add_activityToList(activitate1);

            sapt.add_activity_fromList(1, new TimeOnly(13, 00), new TimeOnly(14, 00), DayOfWeek.Monday);
            */
            

        }
    }
}
