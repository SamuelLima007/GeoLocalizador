using System.Net.Http;
using System.Threading.Tasks;
using GeoLocalizador.app.models;
using GeoLocalizador.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GeoLocalizador.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CepController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly HttpClient _httpClient;

        public CepController(DataContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        
        [HttpGet("{cep}")]
        public async Task<IActionResult> GetGeolocalizacao(string cep)
        {
           
            var geolocalizacao = await _context.Ceps
                .FirstOrDefaultAsync(c => c.CodigoCep == cep);

            if (geolocalizacao != null)
            {
               
                return Ok(geolocalizacao);
            }

            var url = $"https://viacep.com.br/ws/{cep}/json/";
            var response = await _httpClient.GetStringAsync(url);
            var dadosCep = JsonConvert.DeserializeObject<dynamic>(response);

            
            if (dadosCep.erro != null)
            {
                return NotFound("CEP não encontrado.");
            }

            
            decimal latitude = 0.0m; 
            decimal longitude = 0.0m;


            var novaGeolocalizacao = new Cep
            {
                CodigoCep = cep,
                Logradouro = dadosCep.logradouro,
                Bairro = dadosCep.bairro,
                Cidade = dadosCep.localidade,
                Estado = dadosCep.uf,
                Latitude = latitude,   
                Longitude = longitude   
            };

           
            _context.Ceps.Add(novaGeolocalizacao);
            await _context.SaveChangesAsync();

            
            return Ok(novaGeolocalizacao);
        }
    }
}
