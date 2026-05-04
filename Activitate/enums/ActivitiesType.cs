using System.Drawing;
using System.Globalization;

namespace LibrarieModele.enums
{
    public enum ActivityType
    {
        None = 0,
        Sport = 1,
        Education = 2,
        SelfImprovement = 3,
        Resting = 4,
        Entertainment = 5,
        Learning = 6,
        Work = 7,
        Project = 8,
    }
    public static class ActivityTypeExtensions
    {
        public static string ToRomanianString(this ActivityType type)
        {
            return type switch
            {
                ActivityType.None => "Niciuna",
                ActivityType.Sport => "Sport",
                ActivityType.Education => "Educație",
                ActivityType.SelfImprovement => "Dezvoltare personală",
                ActivityType.Resting => "Odihnă",
                ActivityType.Entertainment => "Divertisment",
                ActivityType.Learning => "Învățare",
                ActivityType.Work => "Muncă",
                ActivityType.Project => "Proiect",
                _ => "Necunoscut"
            };
        }
    }
}