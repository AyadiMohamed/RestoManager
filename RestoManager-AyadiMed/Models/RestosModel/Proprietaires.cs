namespace RestoManager_AyadiMed.Models.RestosModel
{
    public class Proprietaires
    {
        public int Numero { get; set; }
        public string Nom { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Gsm { get; set; } = null!;

        public virtual ICollection<Restaurant> LesRestos { get; set; }
    }
}
