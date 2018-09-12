using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi_Seguridad.Controllers
{
    [Security.clsAutorizacion]//control de acceso
    public class VehiculoController : ApiController
    {
        public List<Models.clsVehiculo> Get()
        {
            List<Models.clsVehiculo> lstPersonas = new List<Models.clsVehiculo>();
            return lstPersonas = new Models.clsVehiculo().funSelectTodos();
        }
        public Models.clsVehiculo Get(int id)
        {
            Models.clsVehiculo objPersona = new Models.clsVehiculo();
            objPersona = new Models.clsVehiculo().Recuperar(id);
            if (objPersona.PER_ID == 0)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound, id);
                return objPersona;
            }
            else
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, id);
                return objPersona;
            }
        }
        public HttpResponseMessage Post(Models.clsVehiculo objPersona)
        {
            if (objPersona != null)
            {
                Models.clsVehiculo obj = new Models.clsVehiculo();
                obj.Alta(objPersona);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, objPersona);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
        public HttpResponseMessage Put(Models.clsVehiculo objPersona)
        {
            if (objPersona == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            else
            {
                Models.clsVehiculo ob = new Models.clsVehiculo();
                ob.Modificar(objPersona);
                return Request.CreateResponse(HttpStatusCode.OK, objPersona);
            }
        }
        public HttpResponseMessage Delete(int id)
        {
            if (id == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, id);
            }
            else
            {
                Models.clsVehiculo obj = new Models.clsVehiculo();
                obj.Borrar(id);
                return Request.CreateResponse(HttpStatusCode.OK, id);
            }
        }  
    }
}
