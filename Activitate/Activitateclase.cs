using System.ComponentModel.Design.Serialization;

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
        public string type { get; set; }
        public Guid ID { get; set; }

        //definirea metodelor
        public Activitate() // Constructorul implicit ai clasei
        {
            name = null;
            description = null;
            type = null;
            ID = Guid.NewGuid();
        }
        public Activitate(string _name, string _description, string _tip) // Constructorul ai clasei
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
                $"Tip: {type}\n";
        }

        public Activitate(string strFisier) //citire activitate din fisier
        {
            string[] FisierActivitate = strFisier.Split(SEPARATOR_FISIER);
            this.ID = Guid.Parse(FisierActivitate[ID_pos]);
            this.name = FisierActivitate[NUME_pos];
            this.description = FisierActivitate[DESCRIPTION_pos];
            this.type = FisierActivitate[TYPE_pos];
        }
        
        public string ConversiePentruScriereFisier()
        {
            string ObjActivitateFisier = string.Format("{1}{0}{2}{0}{3}{0}{4}", 
                SEPARATOR_FISIER,
                this.ID.ToString(),
                this.name ?? ("NEDEFINIT"),
                this.description ?? ("NU ESTE NICI O DESCRIERE"),
                this.type ?? ("NU A FOST DEFINIT")
                );
            return ObjActivitateFisier;
        }
    }
}
