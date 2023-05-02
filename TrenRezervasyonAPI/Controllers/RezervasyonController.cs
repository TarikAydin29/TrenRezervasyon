﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly RezervasyonDBContext dB;
        public RezervasyonController(RezervasyonDBContext DB)
        {
            dB = DB;
        }
        [HttpGet]
        public ActionResult<Tren> GetTren()
        {
            if (dB.Tren == null)
            {
                return BadRequest();
            }
            var result = from t in dB.Tren
                         join v in dB.Vagonlar on t.Id equals v.TrenId into vagon
                         select new Tren
                         {
                             Id = t.Id,
                             Name = t.Name,
                             Vagonlar = vagon.ToList()
                         };
            return Ok(result);
        }

        [HttpPost]
        public ActionResult<RezervasyonCevabi> Rezervasyon(string trenAdi, int kisiSayisi,bool kisilerFarkliVagonlaraYerlestirilebilir)
        {
            
            try
            {
                List<OturmaPlani> oturmaPlaniListe = new List<OturmaPlani>();
                Tren tren = dB.Tren.Include(x => x.Vagonlar).Where(x => x.Name == trenAdi).FirstOrDefault();
                foreach (var item in tren.Vagonlar)
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
                    dB.Update(tren);
                    dB.AddRange(oturmaPlaniListe);
                    dB.SaveChanges();
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
