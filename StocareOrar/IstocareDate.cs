using LibrarieModele;
using LibrarieModele.enums;

namespace NivelStocareDate
{
    public interface IstocareDateActivities // Interfata pentru clasa listei Activitati
    {
        void add_activityToList(Activitate activitate);
        void removeActivity(Activitate ActivitytoRemove);
        void modifyActivity(Activitate modifiedActivity);
        Activitate? GetActivity(Guid id);
        List<Activitate>? FindActivitiesByName(string NumeActivitate);
        List<Activitate> GetActivitiesValues();
        Dictionary<Guid, Activitate> GetActivities();
    }
    public interface IstocareDateOrar // interfata pentru clasa Orar
    {
        void add_ScheduledActivity_toSchedule(Scheduled_activity SchedActivity, WeekDays day);
        void add_activity_fromList(Guid ID, TimeOnly start, TimeOnly end, WeekDays day);
        Dictionary<WeekDays, List<Scheduled_activity>> GetOrar();
        string getOrarStr();
        void RemoveActivitiesFromDay(Guid ID_toremove, WeekDays day);
        void RemoveAllActivities(Activitate activitytoremove);
        void RemoveActivityFromDay(Scheduled_activity sched, WeekDays day);
    }
    public interface IstocareDateDeadline // interfata pentru clasa listei Deadline
    {
        void AddDeadline(Deadline deadline);
        Dictionary<Guid, Deadline> GetDeadlines();
        List<Deadline> GetDeadlinesValues();
        Deadline? GetDeadline(Guid id);
        void removeDeadline(Deadline deadlineToRemove);
        void removeDeadline(Guid id);
        public List<Deadline> GetExpiredDeadlines();
    }
}