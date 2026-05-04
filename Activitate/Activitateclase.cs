using System.ComponentModel.Design.Serialization;
using LibrarieModele.enums;

namespace LibrarieModele
{
    public class Activitate
    {
        private const string SEPARATOR_FISIER = ";";
        private const string SEPARATOR_SECUNDAR_FISIER = " ";

        private const int ID_pos = 0;
        private const int NUME_pos = 1;
        private const int DESCRIPTION_pos = 2;
        private const int TYPE_pos = 3;

        //instantele clasei
        public string name { get; set; }
        public string description { get; set; }
        public ActivityType type { get; set; }
        public Guid ID { get; set; }

        // afisare
        public string TipulStr => type.ToRomanianString();
        // definirea metodelor
        public Activitate() // Constructorul implicit ai clasei
        {
            name = String.Empty;
            description = String.Empty;
            type = ActivityType.None;
            ID = Guid.NewGuid();
        }
        public Activitate(string _name, string _description, ActivityType _tip) // Constructorul ai clasei
        {
            name = _name;
            description = _description;
            type = _tip;
            ID = Guid.NewGuid();
        }
        public string INFO()
        {
            return $"Denumirea: {name}\n" +
                $"Descrierea: {description}\n" +
                $"Tip: {type.ToString()}\n";
        }

        public Activitate(string strFisier) //citire activitate din fisier
        {
            string[] FisierActivitate = strFisier.Split(SEPARATOR_FISIER);
            this.ID = Guid.Parse(FisierActivitate[ID_pos]);
            this.name = FisierActivitate[NUME_pos];
            this.description = FisierActivitate[DESCRIPTION_pos];
            Enum.TryParse(FisierActivitate[TYPE_pos], out ActivityType type);
            this.type = type;
        }
        
        public string ConversiePentruScriereFisier()
        {
            string ObjActivitateFisier = string.Format("{1}{0}{2}{0}{3}{0}{4}", 
                SEPARATOR_FISIER,
                this.ID.ToString(),
                this.name ?? ("NEDEFINIT"),
                this.description ?? ("NU ESTE NICI O DESCRIERE"),
                this.type.ToString()
                );
            return ObjActivitateFisier;
        }
    }
}
