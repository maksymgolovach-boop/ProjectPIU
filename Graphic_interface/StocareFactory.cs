using NivelStocareDate;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphic_interface
{
    public static class ManagerStocare
    {
        private const string FORMAT_SALVARE = "FormatSalvare";
        private const string NUME_ACTIVITATI_FISIER = "NumeActivitatiFisier";
        private const string NUME_ORAR_FISIER = "NumeOrarFisier";

        public static IstocareDateActivities GetAdministratorStocareActivitati() // administator stocare pentru activitati
        {
            string formatSalvare = ConfigurationManager.AppSettings[FORMAT_SALVARE] ?? "";

            string numeFisier = ConfigurationManager.AppSettings[NUME_ACTIVITATI_FISIER] ?? "";
            string locatieFisierSolutie = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName ?? "";
            string caleCompletaFisier = locatieFisierSolutie + "\\" + numeFisier;


            if (formatSalvare != null)
            {
                switch (formatSalvare)
                {
                    default:
                    case "txt":
                        return new AdministareActivitatiText(caleCompletaFisier + "." + formatSalvare);
                    case "memorie":
                        return new AdministrareActivitatiMemorie();
                }
            }

            return null;
        }

        public static IstocareDateOrar GetAdministratorStocareOrar(IstocareDateActivities activitati) // administartor pentru stocare orar
        {

            string formatSalvare = ConfigurationManager.AppSettings[FORMAT_SALVARE] ?? "";

            string numeFisier = ConfigurationManager.AppSettings[NUME_ORAR_FISIER] ?? "";
            string locatieFisierSolutie = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName ?? "";
            string caleCompletaFisier = locatieFisierSolutie + "\\" + numeFisier;

            if (formatSalvare != null)
            {
                switch (formatSalvare)
                {
                    default:
                    case "memorie":
                        return new AdministrareOrarMemorie(activitati);
                    case "txt":
                        return new AdministareOrarText(caleCompletaFisier + "." + formatSalvare, activitati);
                }
            }
            return null;
        }

    }
}
