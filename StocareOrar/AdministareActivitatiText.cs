using LibrarieModele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivelStocareDate
{
    public class AdministareActivitatiText : IstocareDateActivities
    {
        private string numeFisierActivitati { get; set; }

        public AdministareActivitatiText(string NumeFisier)
        {
            this.numeFisierActivitati = NumeFisier;
            Stream streamFisierText = File.Open(numeFisierActivitati, FileMode.OpenOrCreate);
            streamFisierText.Close();
        }

        public Dictionary<Guid, Activitate> GetActivities()
        {
            Dictionary<Guid, Activitate> activities = new Dictionary<Guid, Activitate>();

            using(StreamReader stream = new StreamReader(numeFisierActivitati))
            {
                string linieFisier;

                while((linieFisier = stream.ReadLine()) != null) 
                {
                    Activitate act = new Activitate(linieFisier);
                    activities.Add(act.ID, act);
                }
            }
            return activities;
        }

        public List<Activitate> GetActivitiesValues()
        {
            List<Activitate> activities = new List<Activitate>();

            using (StreamReader stream = new StreamReader(numeFisierActivitati))
            {
                string linieFisier;

                while ((linieFisier = stream.ReadLine()) != null)
                {
                    activities.Add(new Activitate(linieFisier));
                }
            }
            return activities;
        }

        public Activitate? GetActivity(Guid id)
        {
            using (StreamReader stream = new StreamReader(numeFisierActivitati))
            {
                string linieFisier;

                while ((linieFisier = stream.ReadLine()) != null)
                {
                    if(new Activitate(linieFisier).ID == id)
                    {
                        return new Activitate(linieFisier);
                    }
                }
            }
            return null;
        }

        public void add_activityToList(Activitate activitate) // adaugare activitate in lista activities
        {
            using(StreamWriter stream = new StreamWriter(numeFisierActivitati, true))
            {
                stream.WriteLine(activitate.ConversiePentruScriereFisier());
            }
        }

        public void removeActivity(Activitate ActivitytoRemove)
        {
            if (numeFisierActivitati == null)
                throw new ArgumentNullException("Fisierul nu a fost gasit sau nu exista!!!");
            string IDtoremove = ActivitytoRemove.ID.ToString();
            var filelines = File.ReadAllLines(numeFisierActivitati);
            var newfilelines = filelines.Where(activity => !activity.StartsWith(IDtoremove));
            File.WriteAllLines(numeFisierActivitati, newfilelines);
        }

        public List<Activitate>? FindActivitiesByName(string NumeActivitate)
        {
            string liniefisier;
            List<Activitate> activitatiGasite = new List<Activitate>(); 

            using(StreamReader stream = new StreamReader(numeFisierActivitati))
            {
                while((liniefisier = stream.ReadLine()) != null)
                {
                    var act = new Activitate(liniefisier);
                    if (act.name == NumeActivitate)
                        activitatiGasite.Add(act);
                }
            }
            if(activitatiGasite.Count() > 0)
                return activitatiGasite;
            return null;
        }

    }
}
