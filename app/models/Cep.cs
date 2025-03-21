using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoLocalizador.app.models
{
    public class Cep
    {
       public int Id { get; set; }
        public string CodigoCep { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}