using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarieModele
{
    public class Scheduled_activity //clasa ce va contine activitatea cu ora start si ora stop
    {
        private const string SEPARATOR_FISIER = ";";

        private const int SCHEDULED_ACTIVITY_ID_POS = 0;
        private const int START_TIME_POS = 1;
        private const int END_TIME_POS = 2;

        public Guid ID { get; set; }
        public TimeOnly start_time { get; set; }
        public TimeOnly end_time { get; set; }

        public Scheduled_activity(Guid activityID, TimeOnly _start_time, TimeOnly _endtime)
        {
            if (_start_time > _endtime)
                throw new ArgumentException("Ora de început nu poate fi după ora de sfarsit.");
            ID = activityID;
            start_time = _start_time;
            end_time = _endtime;
        }

        public Scheduled_activity(string linieFisier)
        {
            string[] Fisier = linieFisier.Split(SEPARATOR_FISIER);
            this.ID = Guid.Parse(Fisier[SCHEDULED_ACTIVITY_ID_POS]);
            this.start_time = TimeOnly.Parse(Fisier[START_TIME_POS]);
            this.end_time = TimeOnly.Parse(Fisier[END_TIME_POS]);
        }
        public string ConversiePentruScriereFisier()
        {
            string ObjSchedActivityFisier = string.Format("{1}{0}{2}{0}{3}",
                SEPARATOR_FISIER,
                this.ID.ToString(),
                this.start_time.ToString(),
                this.end_time.ToString()
                );
            return ObjSchedActivityFisier;
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
