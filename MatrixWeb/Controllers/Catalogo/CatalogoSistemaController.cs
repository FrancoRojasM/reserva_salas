using MatrixService;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
namespace MatrixWeb.Controllers
{
    //[ValidarUsuario]
    public class CatalogoSistemaController : Controller
    {
        private readonly ISvcCatalogo icatalogo;
        public CatalogoSistemaController(ISvcCatalogo _icatalogo)
        {
            icatalogo = _icatalogo;
        }
        [HttpPost]
        public async Task<IActionResult> ListarCatalogoTipoModulo()
        {
            Utilitarios.ListaCatalogo listacatalogo = new Utilitarios.ListaCatalogo();
            listacatalogo =await icatalogo.ListarCatalogoComboAsync( 1, "Sistema");
            return Json(listacatalogo.lista);
        }
        [HttpPost]
        public async Task<IActionResult> ListarCatalogoTipoOpcion()
        {
            Utilitarios.ListaCatalogo listacatalogo = new Utilitarios.ListaCatalogo();
            listacatalogo =await icatalogo.ListarCatalogoComboAsync( 5, "Sistema");
            return Json(listacatalogo.lista);
        }
    }
}









		