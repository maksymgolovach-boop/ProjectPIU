namespace Activitateclase
{
    public class Activitate
    {
        //instantele clasei
        public string name { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public int ID { get; set; }

        public static int _nextID = 0;

        //definirea metodelor
        public Activitate() // Constructorul implicit ai clasei
        {
            name = null;
            description = null;
            type = null;
            ID = 0;
        }
        public Activitate(string _name, string _description, string _tip) // Constructorul ai clasei
        {
            name = _name;
            description = _description;
            type = _tip;
            ID = _nextID + 1;
            _nextID++;
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
        public int ID { get; set; }
        public TimeOnly start_time { get; set; }
        public TimeOnly end_time { get; set; }

        public Scheduled_activity(int activityID, TimeOnly _start_time, TimeOnly _endtime)
        {
            if (_start_time > _endtime)
                throw new ArgumentException("Ora de început nu poate fi după ora de sfarsit.");
            ID = activityID;
            start_time = _start_time;
            end_time = _endtime;
        }
        public bool IfOverlap(Scheduled_activity other)
        {
            if (start_time < other.end_time && end_time > other.start_time)
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
}
