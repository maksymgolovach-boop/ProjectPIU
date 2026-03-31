using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Activitateclase
{
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
        public string INFO(Activitate  activity)
        {
            return $"{start_time}-{end_time} : {activity.name}";
        }

    }
}
