namespace TrenRezervasyonAPI.Model
{
    public class RezervasyonCevabi
    {
        public int Id { get; set; }
        public bool RezervasyonOnayi { get; set; }
        public List<OturmaPlani> YerlesimAyrinti { get; set; }
    }
}
