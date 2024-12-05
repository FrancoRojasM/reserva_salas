using MatrixService;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
namespace MatrixWeb.Controllers
{
    //[ValidarUsuario]
    public class CatalogoCourrierController : Controller
    {
        private readonly ISvcCatalogo icatalogo;
        public CatalogoCourrierController(ISvcCatalogo _icatalogo)
        {
            icatalogo = _icatalogo;
        }
        [HttpPost]
        public async Task<IActionResult> ListarCatalogoSituacionEnvioCourrier()
        {
            var listacatalogo = await icatalogo.ListarCatalogoComboAsync(1, "Courrier");
            var data = listacatalogo.lista.Select(lq => new
            {
                lq.IdCatalogo,
                lq.Descripcion
            }).ToList();
            return Json(data);
        }
        [HttpPost]
        public async Task<IActionResult> ListarCatalogoSituacionEnvioCourrierEntrega()
        {
            var listacatalogo = await icatalogo.ListarCatalogoComboAsync(1, "Courrier");
            var data = listacatalogo.lista.Select(lq => new
            {
                lq.IdCatalogo,
                lq.Descripcion
            }).ToList();
            return Json(data);
        }
        [HttpPost]
        public async Task<IActionResult> ListarCatalogoTipoCourrier()
        {
            var listacatalogo = await icatalogo.ListarCatalogoComboAsync(8, "Courrier");
            var data = listacatalogo.lista.Select(lq => new
            {
                lq.IdCatalogo,
                lq.Descripcion
            }).ToList();
            return Json(data);
            
        }
    }
}









