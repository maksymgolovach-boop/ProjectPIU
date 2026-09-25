using LibrarieModele;

namespace NivelStocareDate
{
    public class AdministrareDeadlinesMemorie : IstocareDateDeadline
    {
        private Dictionary<Guid, Deadline> deadlinesList; // lista cu deadline-uri disponibile cu posibilitatea de adaugare a noilor deadline-uri

        public AdministrareDeadlinesMemorie()
        {
            deadlinesList = new Dictionary<Guid, Deadline>(); // dictionar cu deadline-uri
        }
        public AdministrareDeadlinesMemorie(Dictionary<Guid, Deadline> old_deadlines)
        {
            deadlinesList = new Dictionary<Guid, Deadline>(old_deadlines); // copierea listei de deadline-uri
        }
        public Dictionary<Guid, Deadline> GetDeadlines()
        {
            return deadlinesList;
        }
        public List<Deadline> GetDeadlinesValues()
        {
            return deadlinesList.Values.ToList();
        }
        public Deadline? GetDeadline(Guid id)
        {
            if (deadlinesList.ContainsKey(id))
                return deadlinesList[id];
            return null;
        }
        public List<Deadline> GetExpiredDeadlines()
        {
            return deadlinesList.Values.Where(d => d.DueDate < DateTime.Now).ToList();
        }
        public void AddDeadline(Deadline deadline)
        {
            if (deadlinesList.Keys.Contains(deadline.ID))
            {
                throw new Exception("Deadline-ul deja exista in lista!!!");
            }
            else
            {
                deadlinesList.Add(deadline.ID, deadline);
            }
        }
        public void removeDeadline(Deadline DeadlineToRemove)
        {
            if (GetDeadline(DeadlineToRemove.ID) == null)
                deadlinesList.Remove(DeadlineToRemove.ID);
        }
        public void removeDeadline(Guid id)
        {
            if (GetDeadline(id) == null)
                deadlinesList.Remove(id);
        }
    }
}
