using MatrixService;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
namespace MatrixWeb.Controllers
{
    //[ValidarUsuario]
    public class CatalogoSeguridadController : Controller
    {
        private readonly ISvcCatalogo icatalogo;
        public CatalogoSeguridadController(ISvcCatalogo _icatalogo)
        {
            icatalogo = _icatalogo;
        }
        [HttpPost]
        public async Task<IActionResult> ListarCatalogoTipoUsuario()
        {
            Utilitarios.ListaCatalogo listacatalogo = new Utilitarios.ListaCatalogo();           
                listacatalogo =await icatalogo.ListarCatalogoComboAsync( 1, "Seguridad");
           
            return Json(listacatalogo.lista);
        }

    }
}









		