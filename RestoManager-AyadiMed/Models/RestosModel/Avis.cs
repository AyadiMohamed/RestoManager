using System.ComponentModel.DataAnnotations;

namespace RestoManager_AyadiMed.Models.RestosModel
{
    public class Avis
    {
        public int CodeAvis { get; set; }
        public string nomPersonne { get; set; } = null!;
        public int Note { get; set; }
        public string Commentaire { get; set; } = null!;
        public int NumResto { get; set; }

        public virtual Restaurant leResto { get; set; }
    }
}
