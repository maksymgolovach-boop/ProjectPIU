namespace LibrarieModele
{
    public class Deadline
    {
        public const char SEPARATOR_FISIER = ';';
        public const char SEPARATOR_SECUNDAR_FISIER = ' ';

        private const int ID_pos = 0;
        private const int NUME_pos = 1;
        private const int DESCRIPTION_pos = 2;
        private const int DELETEAFTER_pos = 3;
        private const int DUEDATE_pos = 4;

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime DueDate { get; set; }

        public Guid ID { get; set; }
        public bool DeleteAfterExpiration { get; set; }
        public string DisplayDueDate => DueDate.ToString("dd/MM/yyyy");
        public string DisplayDaysLeft => (DueDate - DateTime.Now).TotalDays >= 0
            ? $"{(DueDate - DateTime.Now).Days} Zile ramase"
            : "Deadline trecut";

        public Deadline()
        {
            this.Name = string.Empty;
            this.Description = string.Empty;
            this.DueDate = DateTime.MinValue;
            ID = Guid.NewGuid();
        }
        public Deadline(string name, string description, DateTime dueDate, Guid ID)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or whitespace.", nameof(name));

            Name = name.Trim();
            Description = description ?? string.Empty;
            DueDate = dueDate;
            ID = this.ID;
        }

        public Deadline(string SirFisier)
        {
            string[] FisierDeadline = SirFisier.Split(SEPARATOR_FISIER);
            this.ID = Guid.Parse(FisierDeadline[ID_pos]);
            this.Name = FisierDeadline[NUME_pos];
            this.Description = FisierDeadline[DESCRIPTION_pos];
            this.DueDate = DateTime.Parse(FisierDeadline[DUEDATE_pos]);
            this.DeleteAfterExpiration = bool.Parse(FisierDeadline[DELETEAFTER_pos]);
        }

        public string ConversiePentruScriereFisier()
        {
            return String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}",
                SEPARATOR_FISIER,
                ID.ToString(),
                Name,
                Description,
                DeleteAfterExpiration.ToString(),
                DueDate.ToString("o")
            );
        }
    }
}