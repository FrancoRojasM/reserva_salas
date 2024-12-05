using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using DataTables.AspNet.AspNetCore;
using MatrixService;
using Microsoft.AspNetCore.Http.Features;
using MatrixWeb.Hubs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;


namespace MatrixWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSignalR();
            services.AddControllers().AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);
            services.AddSession(options =>
            {
                options.Cookie.Name = "SIA.Sesion";
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AllowAnonymous", policy => policy.RequireAssertion(context => true));
            });

            services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = int.MaxValue;
                options.MultipartBodyLengthLimit = long.MaxValue;
                options.MultipartBoundaryLengthLimit = int.MaxValue;
                options.MultipartHeadersCountLimit = int.MaxValue;
                options.MultipartHeadersLengthLimit = int.MaxValue;
                options.BufferBodyLengthLimit = long.MaxValue;
                options.BufferBody = true;
                options.ValueCountLimit = int.MaxValue;
            });
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
                options.MaxRequestBodySize = long.MaxValue;
            });
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new RedirectIfNotFoundAttribute());
            });
            services.RegisterDataTables();
            services.AddCors();
            services.AddHttpContextAccessor();

           

            //Seguridad
            // services.AddTransient<ISvcUsuario, SvcUsuarioApi>();
            services.AddTransient<ISvcUsuario, SvcUsuarioSql>();
            services.AddTransient<ISvcPerfil, SvcPerfilSql>();
            services.AddTransient<ISvcUsuarioPerfil, SvcUsuarioPerfilSql>();
            //services.AddTransient<ISvcUsuarioPerfil, SvcUsuarioPerfilApi>();
            services.AddTransient<ISvcPerfilOpcion, SvcPerfilOpcionSql>();
            services.AddTransient<ISvcUsuarioToken, SvcUsuarioTokenSql>();

            //Sistema
            services.AddTransient<ISvcModulo, SvcModuloSql>();
            services.AddTransient<ISvcOpcion, SvcOpcionSql>();
            services.AddTransient<ISvcOpcionFormulario, SvcOpcionFormularioSql>();
            services.AddTransient<ISvcSoftware, SvcSoftwareSql>();


            //Catalogo
            services.AddTransient<ISvcCatalogo, SvcCatalogoSql>();

            //General
            services.AddTransient<ISvcArea, SvcAreaSql>();
            services.AddTransient<ISvcAreaDocumento, SvcAreaDocumentoSql>();
            services.AddTransient<ISvcCargo, SvcCargoSql>();
            services.AddTransient<ISvcEmpresa, SvcEmpresaSql>();
            services.AddTransient<ISvcEmpresaSedeAmbiente, SvcEmpresaSedeAmbienteSql>();
            services.AddTransient<ISvcEmpresaSede, SvcEmpresaSedeSql>();
            services.AddTransient<ISvcMes, SvcMesSql>();
            services.AddTransient<ISvcPais, SvcPaisSql>();
            services.AddTransient<ISvcPeriodo, SvcPeriodoSql>();
            services.AddTransient<ISvcPersona, SvcPersonaSql>();
            services.AddTransient<ISvcUbigeo, SvcUbigeoSql>();
            services.AddTransient<ISvcRegion, SvcRegionSql>();
            services.AddTransient<ISvcGobiernoRegional, SvcGobiernoRegionalSql>();
            services.AddTransient<ISvcUnidadEjecutora, SvcUnidadEjecutoraSql>();
            
            //Inventario
            services.AddTransient<ISvcArticulo, SvcArticuloSql>();

            //Sanciones
            services.AddTransient<ISvcSancion, SvcSancionSql>();

            //Personal
            services.AddTransient<ISvcEmpleado, SvcEmpleadoSql>();
            services.AddTransient<ISvcEmpleadoPerfil, SvcEmpleadoPerfilSql>();

            //Casilla
            services.AddTransient<ISvcAdministrado, SvcAdministradoSql>();
            services.AddTransient<ISvcNotificacion, SvcNotificacionSql>();
            services.AddTransient<ISvcNotificacionArchivo, SvcNotificacionArchivoSql>();

            services.AddTransient<ISvcSala, SvcSalaSql>();
            services.AddTransient<ISvcSolicitud, SvcSolicitudSql>();
            services.AddTransient<ISvcReserva, SvcReservaSql>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Usuario/Error");
                app.UseStatusCodePagesWithReExecute("/Usuario/Error/{0}");
                app.UseHsts();
            }

            // configure Session middleware
            app.UseSession();

            // configure your application pipeline to use Captcha middleware
            // Important! UseCaptcha(...) must be called after the UseSession() call

            app.UseDefaultFiles();
            app.UseHttpsRedirection();

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=31536000");
                }
            });

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            // Shows UseCors with CorsPolicyBuilder.


            app.UseCors(builder =>
            {
                builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "sancion",
                    pattern: "Sancion/{action=Consulta}/{id?}",
                    defaults: new { controller = "Sancion" }).AllowAnonymous();

                endpoints.MapControllerRoute(
                    name: "SolicitudInterna",
                    pattern: "Solicitud/SolicitudInterna",
                    defaults: new { controller = "Solicitud" }).AllowAnonymous();

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Usuario}/{action=Login}/{id?}");
            });
            app.Use(async (context, next) =>
            {
                var endpoint = context.GetEndpoint();
                if (endpoint == null || endpoint.Metadata.GetMetadata<ControllerActionDescriptor>() == null)
                {
                    // No se encontró un controlador o acción, redirige a la página de inicio de sesión
                    context.Response.Redirect("/Usuario/Error");
                    return;
                }

                await next();
                //context.Features.Get<IHttpMaxRequestBodySizeFeature>().MaxRequestBodySize = null; // unlimited I guess
                //Console.WriteLine("Middleware antes del manejo de excepciones");
                //await next.Invoke();
            });
            MatrixUtilitarios.Utilitario.Configure(httpContextAccessor);
        }
    }
}



public class RedirectIfNotFoundAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // Verifica si la ruta solicitada no existe
        if (context.Result is NotFoundResult)
        {
            // Puedes personalizar la redirección aquí
            context.Result = new RedirectToActionResult("Error", "Usuario", null);
        }

        base.OnActionExecuting(context);
    }
}