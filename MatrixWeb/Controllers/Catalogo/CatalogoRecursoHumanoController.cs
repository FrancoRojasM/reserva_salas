using MatrixService;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
namespace MatrixWeb.Controllers
{
    //[ValidarUsuario]
    public class CatalogoRecursoHumanoController : Controller
    {
        private readonly ISvcCatalogo icatalogo;
        public CatalogoRecursoHumanoController(ISvcCatalogo _icatalogo)
        {
            icatalogo = _icatalogo;
        } 
        [HttpPost]
        public async Task<IActionResult> ListarCatalogoEstadoEmpleado()
        {
            Utilitarios.ListaCatalogo listacatalogo = new Utilitarios.ListaCatalogo();           
                listacatalogo =await icatalogo.ListarCatalogoComboAsync( 1, "RecursoHumano");
            var data = listacatalogo.lista.Select(lq => new
            {
                lq.IdCatalogo,
                lq.Descripcion
            }).ToList();
            return Json(data);
        }

        [HttpPost]
        public async Task<IActionResult> ListarCatalogoTipoEmpleado()
        {
            Utilitarios.ListaCatalogo listacatalogo = new Utilitarios.ListaCatalogo();
            listacatalogo =await icatalogo.ListarCatalogoComboAsync( 7, "RecursoHumano");
            var data = listacatalogo.lista.Select(lq => new
            {
                lq.IdCatalogo,
                lq.Descripcion
            }).ToList();
            return Json(data);
        }
        [HttpPost]
        public async Task<IActionResult> ListarCatalogoEstadoActividadDiaria()
        {
            var listacatalogo = await icatalogo.ListarCatalogoComboAsync(43, "RecursoHumano");
            var data = listacatalogo.lista.Select(lq => new
            {
                lq.IdCatalogo,
                lq.Descripcion
            }).ToList();
            return Json(data);
        }
    }
}









		