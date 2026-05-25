using LibrarieModele.enums;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace NivelWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>

public partial class App : Application
    {

    }

public static class ActivityTypeColors
    {
        public static readonly Dictionary<ActivityType, Color> ColorMap = new()
    {
        { ActivityType.SelfImprovement, Color.FromArgb(255, 0x1D, 0x9E, 0x75) },
        { ActivityType.Learning,        Color.FromArgb(255, 0x18, 0x5F, 0xA5) },
        { ActivityType.Work,            Color.FromArgb(255, 0x7F, 0x77, 0xDD) },
        { ActivityType.Sport,           Color.FromArgb(255, 74, 207, 107) },
        { ActivityType.Education,       Color.FromArgb(255, 84, 214, 199) },
        { ActivityType.Resting,         Color.FromArgb(255, 255, 244, 163) },
        { ActivityType.Entertainment,   Color.FromArgb(255, 203, 89, 235) },
        { ActivityType.Project,         Color.FromArgb(255, 224, 70, 70) },
        { ActivityType.None,            Color.FromArgb(255, 130, 130, 130) },
    };

        public static SolidColorBrush GetBrush(ActivityType type)
        {
            return new SolidColorBrush(ColorMap[type]);
        }
    }
    public class ActivityTypeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ActivityType type)
                return ActivityTypeColors.GetBrush(type);

            return Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
    public class ActivityTypeToRomanianConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is ActivityType type)
            {
                return type.ToRomanianString(); // Calls your extension method
            }
            return value?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
    public class DateToColorConverter : IValueConverter
    {
        public static readonly Dictionary<int, Color> ColorMap = new()
        {
            { 0,        Color.FromArgb(255, 252, 3, 3) },
            { 1,        Color.FromArgb(255, 255, 178, 13) },
            { 2,        Color.FromArgb(255, 0, 199, 27) },
            { 3,        Color.FromArgb(255, 120, 120, 120) },
        };
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is DateTime DueDate)
            {
                if (DueDate < DateTime.Now)
                    return new SolidColorBrush(ColorMap[3]);
                TimeSpan difference = (DueDate - DateTime.Now).Duration();
                if (difference.TotalDays <= 7)
                    return new SolidColorBrush(ColorMap[0]);
                else if (difference.TotalDays <= 14)
                    return new SolidColorBrush(ColorMap[1]);
                else
                    return new SolidColorBrush(ColorMap[2]);
            }
            return value?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
    public class DateToLightColorConverter : IValueConverter
    {
        public static readonly Dictionary<int, Color> ColorMap = new()
        {
            { 0,        Color.FromArgb(30, 252, 3, 3) },
            { 1,        Color.FromArgb(30, 255, 178, 13) },
            { 2,        Color.FromArgb(30, 0, 199, 27) },
            { 3,        Color.FromArgb(30, 120, 120, 120) },
        };
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is DateTime DueDate)
            {
                if (DueDate < DateTime.Now)
                    return new SolidColorBrush(ColorMap[3]);
                TimeSpan difference = (DueDate - DateTime.Now).Duration();
                if (difference.TotalDays <= 7)
                    return new SolidColorBrush(ColorMap[0]);
                else if (difference.TotalDays <= 14)
                    return new SolidColorBrush(ColorMap[1]);
                else
                    return new SolidColorBrush(ColorMap[2]);
            }
            return value?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
