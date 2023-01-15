namespace WebApplication1.Models
{
    public class StrojIspis
    {
        public string Naziv_stroja { get; set; }

        public TimeSpan Ukupno_trajanje_kvarova { get; set; }
        public List<Kvar> kvarovi { get; set; }

    }
}
