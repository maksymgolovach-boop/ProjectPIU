using Activitateclase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zile.Core;

namespace StocareOrar
{
    public class Activitati
    {
        public Dictionary<int, Activitate> activities; // lista cu activitati disponibile cu posibilitatea de adaugare a noii activitati

        public Activitati()
        {
            activities = new Dictionary<int, Activitate>(); // dictionar cu activitati
        }
        public Activitati(Dictionary<int, Activitate> old_activities)
        {
            activities = new Dictionary<int, Activitate>(old_activities); // copierea listei de activitati
        }

        public void add_activityToList(Activitate activitate) // adaugare activitate in lista activities
        {
            if (activities.Keys.Contains(activitate.ID))
            {
                throw new Exception("Activitatea deja exista in lista!!!");
            }
            else
            {
                activities.Add(activitate.ID, activitate);
            }
        }
        public string show_activitylist()
        {
            string buffer = "";
            foreach (var key in activities.Keys)
            {
                buffer += $"ID: {key} \n" + activities[key].INFO() + "\n";
            }
            return buffer;
        }

    }
}
