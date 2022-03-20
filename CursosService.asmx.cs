using Cursos_Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Cursos_Service
{
    /// <summary>
    /// Descripción breve de CursosService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class CursosService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }


        [WebMethod(Description = "Consultar datos desde la BD")]
        public List<Cursito> ObtenerDatos()
        {
            using (cursosEntities conexion = new cursosEntities())
            {
                var query = (from a in conexion.Cursitoes select a);
                return query.ToList();
            }
        }


 
        [WebMethod(Description = "Insertar datos en la BD")]
        public string InsertarDatos(string NombreCurso, string Duracion, string Docente, string Costo,
           string Horas)
        {

            try
            {
                using (cursosEntities conexion = new cursosEntities())
                {
                    Cursito nuevo = new Cursito();
                    nuevo.nombre_curso= NombreCurso;
                    nuevo.duracion = Duracion;
                    nuevo.docente = Docente;
                    nuevo.costo = Costo;         
                    nuevo.horas = Horas;
                    conexion.Cursitoes.Add(nuevo);
                    conexion.SaveChanges(); //Agrega el registro a la BD
                    return "--Registro Exitoso--";
                }
            }
            catch (Exception)
            {
                return "--Error--No se guardaron datos";
            }

        }


        [WebMethod(Description = "Modificar datos en la BD")]
        public string ModificarDatos(int Id, string NombreCurso, string Duracion, string Docente, string Costo,
          string Horas)
        {
            try
            {
                using (cursosEntities conexion = new cursosEntities())
                {
                    var query = (from a in conexion.Cursitoes where a.id == Id select a).FirstOrDefault();
                    if (query != null)
                    {
                        query.nombre_curso = NombreCurso;
                        query.duracion  = Duracion;
                        query.docente = Docente;
                        query.costo = Costo;
                        query.horas = Horas;
                        conexion.SaveChanges(); //Agrega el registro a la BD
                        return "--Datos Modificados--";
                    }
                    else
                    {
                        return "--A ocurrido un error--";
                    }

                }
            }
            catch (Exception)
            {
                return "--Error--No se guardaron cambios";
            }

        }

        [WebMethod(Description = "Eliminar datos en la BD")]
        public string EliminarDatos(int Id)
        {

            try
            {
                using (cursosEntities conexion = new cursosEntities())
                {
                    var query = (from a in conexion.Cursitoes where a.id == Id select a).FirstOrDefault();
                    if (query != null)
                    {
                        conexion.Cursitoes.Remove(query);
                        conexion.SaveChanges(); //Agrega el registro a la BD
                        return "--Datos Eliminados--";
                    }
                    else
                    {
                        return "--A ocurrido un error--";
                    }

                }
            }
            catch (Exception)
            {
                return "--Error--No se guardaron cambios";
            }

        }

        [WebMethod(Description = "Buscar datos en la BD")]

        public List<Cursito> BuscarDatos(int Id)
        {
            using (cursosEntities conexion = new cursosEntities())
            {
                var query = (from a in conexion.Cursitoes where a.id == Id select a);
                return query.ToList();
            }
        }



    }
}
