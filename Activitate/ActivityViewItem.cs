using LibrarieModele.enums;

namespace LibrarieModele
{
    public class ActivityViewItem
    {
        public Scheduled_activity Sched { get; set; }
        public string Name { get; set; }
        public ActivityType type { get; set; }
        private const int startRowHour = 8;
        public int DayColumn { get; set; }
        public int StartRow => (Sched.start_time.Hour - startRowHour);
        public int RowSpan => Sched.end_time.Hour - Sched.start_time.Hour;
    }
}
