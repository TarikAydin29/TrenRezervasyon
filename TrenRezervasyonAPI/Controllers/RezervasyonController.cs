using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Identity.Client;
using System.Text.Json;
using TrenRezervasyonAPI.Model;

namespace TrenRezervasyonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RezervasyonController : ControllerBase
    {

        public RezervasyonController()
        {
          
        }       

        [HttpPost]
        public ActionResult<RezervasyonCevabi> Rezervasyon(Tren trenGelen,int kisiSayisi, bool kisilerFarkliVagonlaraYerlestirilebilir)
        {           
            try
            {
                List<OturmaPlani> oturmaPlaniListe = new List<OturmaPlani>();
                
                foreach (var item in trenGelen.Vagonlar)
                {
                    OturmaPlani oturmaPlani = new OturmaPlani();

                    if (Convert.ToDouble(item.DoluKoltukAdet) / Convert.ToDouble(item.Kapasite) * 100 >= 70)
                        continue;

                    while (Convert.ToDouble(item.DoluKoltukAdet) / Convert.ToDouble(item.Kapasite) * 100 < 70 && kisiSayisi > 0)
                    {
                        item.DoluKoltukAdet++;
                        kisiSayisi--;
                        oturmaPlani.VagonAdi = item.Name;
                        oturmaPlani.KisiSayisi++;
                    }
                    if (kisilerFarkliVagonlaraYerlestirilebilir == false && kisiSayisi != 0)
                        break;
                    oturmaPlaniListe.Add(oturmaPlani);
                    if (kisiSayisi == 0)
                        break;

                }
                RezervasyonCevabi rezervasyonCevabi = new RezervasyonCevabi();
                if (kisiSayisi != 0)
                {
                    rezervasyonCevabi.RezervasyonOnayi = false;
                    rezervasyonCevabi.YerlesimAyrinti = new List<OturmaPlani>();
                    return Ok(rezervasyonCevabi);
                }
                else
                {
                    rezervasyonCevabi.YerlesimAyrinti = oturmaPlaniListe;
                    rezervasyonCevabi.RezervasyonOnayi = true;
                   
                    return Ok(rezervasyonCevabi);
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

    }
}
