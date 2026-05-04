using LibrarieModele.enums;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Graphic_interface
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
}
