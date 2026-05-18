using LibrarieModele.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarieModele
{
    public class ActivityViewItem
    {
        public Scheduled_activity Sched { get; set; }
        public string Name { get; set; }
        public ActivityType type { get; set; }

        public int DayColumn { get; set; }
        public int StartRow => (Sched.start_time.Hour - 8);
        public int RowSpan => Sched.end_time.Hour - Sched.start_time.Hour;
    }
}
