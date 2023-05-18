namespace RestoManager_AyadiMed.Models.RestosModel
{
    public class Restaurant
    {
        public int CodeResto { get; set; }
        public string NomResto { get; set; } = null!;

        public string Specialite { get; set; }
        public string Ville { get; set; } = null!;
        public string Tel { get; set; } = null!;
        public int NumProp { get; set; }

        public virtual Proprietaires? LePropio { get; set; }
        public virtual ICollection<Avis>? LesAvis { get; set; } 
    }
}
