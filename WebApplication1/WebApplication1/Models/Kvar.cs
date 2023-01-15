﻿namespace WebApplication1.Models
{
    public class Kvar
    {
        public int Id_kvara { get; set; }
        public int Id_stroja { get; set; }
        public string Naziv_kvara { get; set; }
        public string Prioritet { get; set; }
        public DateTime Vrijeme_pocetka { get; set; }
        public DateTime? Vrijeme_zavrsetka { get; set; }
        public string Detaljni_opis { get; set; }
        public bool Aktivan_kvar { get; set; }

    }
}
