using LibrarieModele;
using Zile.Core;

namespace NivelStocareDate
{
    public interface IstocareDateActivities // Interfata pentru clasa Activitati
    {
        void add_activityToList(Activitate activitate);
        void removeActivity(Activitate ActivitytoRemove);
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
    }
}
