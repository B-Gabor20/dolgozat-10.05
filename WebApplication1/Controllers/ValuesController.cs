using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;
using WebApplication1.Jegyek;
using static WebApplication1.Dtos;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        Connect connect = new();
        public List<BasicDto> jegyeklista = new();
        private Guid Azonosito;

        [HttpGet]
        public ActionResult<IEnumerable<BasicDto>> Get()
        {
            try
            {
                connect.connection.Open();
                string sql = "SELECT * FROM `jegyek`";
                MySqlCommand cmd = new MySqlCommand(sql, connect.connection);
                MySqlDataReader miertnemolvasodbe = cmd.ExecuteReader();

                while (miertnemolvasodbe.Read())
                {
                    var kinesszevedes = new BasicDto(
                        miertnemolvasodbe.GetGuid("Azonosito"),
                        miertnemolvasodbe.GetInt32("Ertekeles"),
                        miertnemolvasodbe.GetString("Leiras"),
                        miertnemolvasodbe.GetString("LetrehozasIdeje")
                        );
                    jegyeklista.Add(kinesszevedes);
                }


                connect.connection.Close();
                return StatusCode(200, jegyeklista);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{Azonosito}")]
        public ActionResult<IEnumerable<BasicDto>> Get(Guid Azonosito)
        {
            var Azonositolocal = Get(Azonosito);
            try
            {
                connect.connection.Open();
                string mittudomen = "SELECT * FROM jegyek WHERE id=@ID";
                MySqlCommand cmd = new MySqlCommand(mittudomen, connect.connection);

                cmd.Parameters.AddWithValue("Azonosito", Azonosito);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    var result = new BasicDto(
                        reader.GetGuid("Azonosito"),
                        reader.GetInt32("Ertekeles"),
                        reader.GetString("Leiras"),
                        reader.GetString("LetrehozasIdeje")
                    );
                }
                connect.connection.Close();
                return StatusCode(200);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
