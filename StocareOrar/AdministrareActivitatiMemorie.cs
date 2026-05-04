using LibrarieModele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivelStocareDate
{
    public class AdministrareActivitatiMemorie : IstocareDateActivities
    {
        private const string SEPARATOR_FISIER = ";";
        private const string SEPARATOR_SECUNDAR_FISIER = " ";
        private const string SEPARATOR_TERTIAR = "-";

        private Dictionary<Guid, Activitate> activitatiList; // lista cu activitati disponibile cu posibilitatea de adaugare a noii activitati

        public AdministrareActivitatiMemorie()
        {
            activitatiList = new Dictionary<Guid, Activitate>(); // dictionar cu activitati
        }
        public AdministrareActivitatiMemorie(Dictionary<Guid, Activitate> old_activities)
        {
            activitatiList = new Dictionary<Guid, Activitate>(old_activities); // copierea listei de activitati
        }

        public Dictionary<Guid, Activitate> GetActivities()
        {
            return activitatiList;
        }

        public List<Activitate> GetActivitiesValues()
        {
            return activitatiList.Values.ToList();
        }

        public Activitate? GetActivity(Guid id)
        {
            if (activitatiList.ContainsKey(id))
                return activitatiList[id];
            return null;
        }

        public void add_activityToList(Activitate activitate) // adaugare activitate in lista activities
        {
            if (activitatiList.Keys.Contains(activitate.ID))
            {
                throw new Exception("Activitatea deja exista in lista!!!");
            }
            else
            {
                activitatiList.Add(activitate.ID, activitate);
            }
        }

        public void removeActivity(Activitate ActivitytoRemove)
        {
            if (GetActivity(ActivitytoRemove.ID) == null)
                activitatiList.Remove(ActivitytoRemove.ID);
        }

        public List<Activitate>? FindActivitiesByName(string NumeActivitate)
        {
            if(activitatiList.Count == 0)
            {
                return null;
            }
            List<Activitate> activities_ = new List<Activitate>();
            activities_ = activitatiList.Values.Where(a => a.name == NumeActivitate).ToList();
            return activities_;
        }

        public string show_activitylist()
        {
            string buffer = "";
            foreach (var key in activitatiList.Keys)
            {
                buffer += $"ID: {key} \n" + activitatiList[key].INFO() + "\n";
            }
            return buffer;
        }

        public string ActivitatiToFile()
        {
            string List_pt_fisier= "";
            foreach (Activitate act in activitatiList.Values)
            {
                List_pt_fisier += act.ConversiePentruScriereFisier() + SEPARATOR_SECUNDAR_FISIER;
            }
            return List_pt_fisier;
        }

    }
}
