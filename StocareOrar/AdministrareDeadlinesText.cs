using LibrarieModele;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivelStocareDate
{
    public class AdministrareDeadlinesText : IstocareDateDeadline
    {
        private const string SEPARATOR_FISIER = ";";
        private const string SEPARATOR_SECUNDAR_FISIER = " ";
        private string numeFisier;

        public AdministrareDeadlinesText(string numeFisier)
        {
            this.numeFisier = numeFisier;
        }
        public Dictionary<Guid, Deadline> GetDeadlines()
        {
            Dictionary<Guid, Deadline> deadlines = new Dictionary<Guid, Deadline>();
            if (File.Exists(numeFisier))
            {
                using (StreamReader reader = new StreamReader(numeFisier))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var deadline = new Deadline(line);
                        deadlines.Add(deadline.ID, deadline);
                    }
                }
            }
            return deadlines;
        }
        public List<Deadline> GetDeadlinesValues()
        {
            List<Deadline> deadlines = new List<Deadline>();
            if (File.Exists(numeFisier))
            {
                using (StreamReader reader = new StreamReader(numeFisier))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var deadline = new Deadline(line);
                        deadlines.Add(deadline);
                    }
                }
            }
            else
            {
                throw new ArgumentNullException("Fisierul nu a fost gasit sau nu exista!!!");
            }

            return deadlines;
        }
        public Deadline? GetDeadline(Guid id)
        {
            if (numeFisier == null || !File.Exists(numeFisier))
                throw new ArgumentNullException("Fisierul nu a fost gasit sau nu exista!!!");
            using (StreamReader reader = new StreamReader(numeFisier))
            {
                string IDToFind = id.ToString();
                var lines = File.ReadAllLines(numeFisier);
                var ddl = lines.FirstOrDefault(linie => linie.StartsWith(IDToFind));
                if (ddl != null)
                    return new Deadline(ddl);
            }
            return null;
        }
        public List<Deadline> GetExpiredDeadlines()
        {
            List<Deadline> Expireddeadlines = new List<Deadline>();
            if (File.Exists(numeFisier))
            {
                using (StreamReader reader = new StreamReader(numeFisier))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var deadline = new Deadline(line);
                        if (deadline.DueDate < DateTime.Now)
                            Expireddeadlines.Add(deadline);
                    }
                }
            }
            else
            {
                throw new ArgumentNullException("Fisierul nu a fost gasit sau nu exista!!!");
            }

            return Expireddeadlines;
        }

        public void AddDeadline(Deadline deadline)
        {
            using (StreamReader reader = new StreamReader(numeFisier))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var existingDeadline = new Deadline(line);
                    if (existingDeadline.ID == deadline.ID)
                    {
                        throw new ArgumentException("Un deadline cu acest ID exista deja in fisier!!!");
                    }
                }
            }
            using (StreamWriter writer = new StreamWriter(numeFisier, true))
            {
                writer.WriteLine(deadline.ConversiePentruScriereFisier());
            }
        }
        public void removeDeadline(Deadline deadlineToRemove)
        {
            if (numeFisier == null)
                throw new ArgumentNullException("Fisierul nu a fost gasit sau nu exista!!!");
            string IDToRemove = deadlineToRemove.ID.ToString();
            string[] lines = File.ReadAllLines(numeFisier);
            var linesToKeep = lines.Where(line => !line.StartsWith(IDToRemove)).ToArray();
            File.WriteAllLines(numeFisier, linesToKeep);
        }
        public void removeDeadline(Guid id)
        {
            if (numeFisier == null)
                throw new ArgumentNullException("Fisierul nu a fost gasit sau nu exista!!!");
            string IDToRemove = id.ToString();
            string[] lines = File.ReadAllLines(numeFisier);
            var linesToKeep = lines.Where(line => !line.StartsWith(IDToRemove)).ToArray();
            File.WriteAllLines(numeFisier, linesToKeep);
        }
    }
}
