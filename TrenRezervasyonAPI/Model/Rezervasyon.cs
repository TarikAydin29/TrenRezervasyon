using Newtonsoft.Json;

namespace TrenRezervasyonAPI.Model
{
    public class Rezervasyon
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int KisiSayisi { get; set; }
    }
}
