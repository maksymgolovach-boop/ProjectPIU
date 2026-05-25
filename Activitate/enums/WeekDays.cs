namespace LibrarieModele.enums
{
    public enum WeekDays
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
        Sunday = 7
    }

    public static class WeekDaysTypeExtensions
    {
        public static string ToRomanianString(this WeekDays type)
        {
            return type switch
            {
                WeekDays.Monday => "Luni",
                WeekDays.Tuesday => "Marți",
                WeekDays.Wednesday => "Miercuri",
                WeekDays.Thursday => "Joi",
                WeekDays.Friday => "Vineri",
                WeekDays.Saturday => "Sâmbătă",
                WeekDays.Sunday => "Duminică",
            };
        }
    }
}
