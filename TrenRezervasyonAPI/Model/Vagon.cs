using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrenRezervasyonAPI.Model
{
    public class Vagon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Kapasite { get; set; }
        public int DoluKoltukAdet { get; set; }
        public int TrenId { get; set; }

      
        public virtual Tren? Tren { get; set; }
    }
}
