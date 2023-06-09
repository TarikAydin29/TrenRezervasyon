﻿using System.Text.Json;

namespace TrenRezervasyonAPI.Model
{
    public class Tren
    {
        public Tren()
        {
            Vagonlar = new HashSet<Vagon>();
        }      
        public string Name { get; set; }
        public virtual ICollection<Vagon>? Vagonlar { get; set; }
    }
}
