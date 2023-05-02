using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrenRezervasyonAPI.Model
{
    public class Vagon
    {       
        public string Name { get; set; }
        public int Kapasite { get; set; }
        public int DoluKoltukAdet { get; set; }    

    }
}
