# ProjectPIU - Personal Schedule Manager

A modern desktop application for managing your personal schedule, activities, and deadlines efficiently. Built with .NET 8 and WPF, this application provides a user-friendly interface for organizing your daily tasks and events.

## 📋 Table of Contents

- [Key Features](#key-features)
- [Application Structure](#application-structure)
- [Requirements](#requirements)
- [Installation](#installation)
- [Running the Application](#running-the-application)
- [Project Structure](#project-structure)
- [Features to be added](#new-features-to-be-added)

## ✨ Key Features

### 📅 Schedule Management
- **Personal Schedule Viewer**: Visualize your weekly and daily schedule with an intuitive calendar interface
- **Activity Organization**: Create, modify, and manage activities with ease
- **Weekly View**: See all your activities organized by day of the week

### 📝 Activity Management
- **Create Activities**: Add new activities with names, descriptions, and specific types
- **Activity Types**: Organize activities by type (Work, Study, Exercise, Personal, etc.)
- **Modify Activities**: Edit existing activities directly from the interface
- **Activity Search**: Quickly find activities using the search functionality
- **Delete Activities**: Remove activities from your list and schedule

### ⏰ Deadline Tracking
- **Add Deadlines**: Set and track important deadlines for your tasks
- **Deadline Management**: Modify and delete deadlines as needed
- **Visual Indicators**: Color-coded display to differentiate activities and deadlines

### 🗓️ Schedule Planning
- **Direct Scheduling**: Add activities directly to your weekly schedule
- **Flexible Scheduling**: Schedule activities for specific days and times
- **Activity Selection**: Easy dialog for selecting activities to schedule
- **Schedule Management**: Remove activities from specific days or entirely

### 💾 Data Persistence
- **Multiple Storage Options**: Support for both file-based and in-memory data storage
- **Automatic Saving**: Seamlessly save your schedule and activities to text files
- **File-Based Configuration**: Easy access to configuration through text files

### 🎨 User-Friendly Interface
- **Modern WPF Design**: Clean and intuitive user interface built with WPF
- **Responsive Layout**: Adaptive design that works with different screen sizes
- **Color-Coded Display**: Visual differentiation between different types of activities and dates
- **Intuitive Navigation**: Easy-to-use dialogs and windows for all operations

## 🏗️ Application Structure

The application follows a layered architecture with three main projects:

- **NivelWPF** (Graphic Interface Layer): The presentation layer with WPF windows and controls
- **LibrarieModele** (Models/Business Logic Layer): Core domain models and business logic
- **NivelStocareDate** (Data Storage Layer): Data persistence layer supporting multiple storage mechanisms

## 🚀 Installation

### Prerequisites

1. Install [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
2. Clone the repository:
   ```bash
   git clone https://github.com/maksymgolovach-boop/ProjectPIU.git
   cd ProjectPIU_ver1.0
   ```

### Build Instructions

1. Open the solution file in Visual Studio:
   ```
   ProjectPIU_ver1.0.sln
   ```

2. Or build from command line:
   ```bash
   dotnet build
   ```

## ▶️ Running the Application

### Option 1: Visual Studio (Recommended)

1. Open `ProjectPIU_ver1.0.sln` in Visual Studio
2. Set `NivelWPF` as the startup project (right-click → Set as Startup Project)
3. Press `F5` or click **Start Debugging**

### Option 2: Command Line

```bash
cd Graphic_interface
dotnet run
```

### Option 3: Direct Execution

Build the application first:
```bash
dotnet build -c Release
```

Then run the executable:
```bash
cd Graphic_interface/bin/Release/net8.0-windows
./NivelWPF.exe
```

## 📁 Project Structure

```
ProjectPIU_ver1.0/
├── Graphic_interface/          # WPF Presentation Layer (NivelWPF)
│   ├── MainWindow.xaml         # Main application window
│   ├── AddActivityWindow.xaml   # Add activity dialog
│   ├── AddDeadlineWindow.xaml   # Add deadline dialog
│   ├── AddtoSchedule.xaml       # Schedule activity dialog
│   ├── AddDirectlyToSchedule.xaml
│   ├── ModifyAct.xaml           # Modify activity dialog
│   ├── SelectActivityDialog.xaml
│   └── ...
├── Activitate/                 # Business Logic & Models (LibrarieModele)
│   ├── Activitate.cs           # Activity model class
│   ├── Deadline.cs             # Deadline model class
│   ├── ScheduledActivity.cs     # Scheduled activity model
│   ├── ActivityViewItem.cs      # Activity view model
│   └── enums/
│       ├── ActivityType.cs      # Activity type enumeration
│       └── WeekDays.cs          # Week days enumeration
├── StocareOrar/                # Data Storage Layer (NivelStocareDate)
│   ├── IstocareDate.cs         # Storage interface
│   ├── AdministareActivitatiText.cs      # Text file storage for activities
│   ├── AdministareDeadlinesText.cs       # Text file storage for deadlines
│   ├── AdministareOrarText.cs            # Text file storage for schedule
│   ├── AdministrareActivitatiMemorie.cs  # In-memory storage for activities
│   └── ...
└── README.md
```

## 📧 Support

For questions or feedback about the project, please visit the [GitHub repository](https://github.com/maksymgolovach-boop/ProjectPIU).

## New Features to be Added:
1. **Info window for deadlines**
2. **Modify button for activities in schedule**
3. **Add unit testing**
4. **Settings for another languages**

---

**Enjoy organizing your schedule with Personal Schedule Manager!** 📅✨
