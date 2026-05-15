using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestion_taller
{
    public class Trabajo
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public string Vehiculo { get; set; }
        public string Descripcion { get; set; }
        public int Precio { get; set; }
        

        public Trabajo(DateTime _fecha, string _cliente, string _vehiculo, string _descripcion, int _precio)
        {
           
            Fecha = _fecha;
            Cliente = _cliente;
            Vehiculo = _vehiculo;
            Descripcion = _descripcion;
            Precio = _precio;
            
        }

        public Trabajo(int _id, DateTime _fecha, string _cliente, string _vehiculo, string _descripcion, int _precio)
        {
            Id = _id;
            Fecha = _fecha;
            Cliente = _cliente;
            Vehiculo = _vehiculo;
            Descripcion = _descripcion;
            Precio = _precio;
            
        }
    }
}
