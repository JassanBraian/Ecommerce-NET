using System;
using System.Windows.Forms;
using Presentacion.Seguridad;
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Core.Articulo;
using XCommerce.Servicio.Core.Articulo.DTOs;
using XCommerce.Servicio.Core.Cliente;
using XCommerce.Servicio.Core.Cliente.DTOs;
using XCommerce.Servicio.Core.CondicionIva;
using XCommerce.Servicio.Core.CondicionIva.DTOs;
using XCommerce.Servicio.Core.Empleado;
using XCommerce.Servicio.Core.Empleado.DTOs;
using XCommerce.Servicio.Core.ListaPrecio;
using XCommerce.Servicio.Core.ListaPrecio.DTOs;
using XCommerce.Servicio.Core.Localidad;
using XCommerce.Servicio.Core.Localidad.DTOs;
using XCommerce.Servicio.Core.Marca;
using XCommerce.Servicio.Core.Marca.DTOs;
using XCommerce.Servicio.Core.Precio;
using XCommerce.Servicio.Core.Provincia;
using XCommerce.Servicio.Core.Provincia.DTOs;
using XCommerce.Servicio.Core.Rubro;
using XCommerce.Servicio.Core.Rubro.DTOs;
using XCommerce.Servicio.Core.TipoEmpleado;
using XCommerce.Servicio.Core.TipoEmpleado.DTOs;
using XCommerce.Servicio.Seguridad.Seguridad;
using XCommerce.Servicio.Seguridad.Usuario;
using XCommerce.Servicio.Seguridad.Usuario.DTOs;

namespace XCommerce
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IProvinciaServicio _provinciaServicio = new ProvinciaServicio();
            ILocalidadServicio _localidadServicio = new LocalidadServicio();
            ITipoEmpleadoSericio _tipoEmpleadoServicio = new TipoEmpleadoServicio();
            IEmpleadoServicio _empleadoServicio = new EmpleadoServicio();
            IUsuarioServicio _usuarioServicio = new UsuarioServicio();
            IClienteServicio _clienteServicio = new ClienteServicio();
            IListaPrecioServicio _listaPrecioServicio = new ListaPrecioServicio();
            ICondicionIvaServicio _condicionIvaServicio = new CondicionIvaServicio();
            IArticuloServicio _articuloServicio = new ArticuloServicio();
            IMarcaServicio _marcaServicio = new MarcaServicio();
            IRubroServicio _rubroServicio = new RubroServicio();
            IPrecioServicio _precioServicio = new PrecioServicio();


            ///////////// Inicio de Verificacion de Registros base en DB /////////////

            // Provincia
            var provincia =_provinciaServicio.ObtenerPorDescripcion("Tucuman");
            if(provincia == null)
            {
                var nuevaProvincia = new ProvinciaDto
                {
                    Descripcion = "Tucuman",
                    EstaEliminado = false
                };

                _provinciaServicio.Insertar(nuevaProvincia);
            }

            // Localidad
            var localidad = _localidadServicio.ObtenerPorDescripcion("San Miguel de Tucuman");
            if (localidad == null)
            {
                var nuevaLocalidad = new LocalidadDto
                {
                    Descripcion = "San Miguel de Tucuman",
                    ProvinciaId = _provinciaServicio.ObtenerPorDescripcion("Tucuman").Id,
                    Provincia = _provinciaServicio.ObtenerPorDescripcion("Tucuman").Descripcion,
                    EstaEliminado = false,
                    
                };

                _localidadServicio.Insertar(nuevaLocalidad);
            }

            // Tipo de Empleado
            var tipoEmpleado1 = _tipoEmpleadoServicio.ObtenerPorDescripcion("Cajero");
            if (tipoEmpleado1 == null)
            {
                var nuevoTipoEmple = new TipoEmpleadoDto
                {
                    Descripcion = "Cajero",
                    EstaEliminado = false
                };

                _tipoEmpleadoServicio.Insertar(nuevoTipoEmple);
            }
            var tipoEmpleado2 = _tipoEmpleadoServicio.ObtenerPorDescripcion("Cadete");
            if (tipoEmpleado2 == null)
            {
                var nuevoTipoEmple = new TipoEmpleadoDto
                {
                    Descripcion = "Cadete",
                    EstaEliminado = false
                };

                _tipoEmpleadoServicio.Insertar(nuevoTipoEmple);
            }
            var tipoEmpleado3 = _tipoEmpleadoServicio.ObtenerPorDescripcion("Mozo");
            if (tipoEmpleado3 == null)
            {
                var nuevoTipoEmple = new TipoEmpleadoDto
                {
                    Descripcion = "Mozo",
                    EstaEliminado = false
                };

                _tipoEmpleadoServicio.Insertar(nuevoTipoEmple);
            }

            // Empleado
            var empleado1 = _empleadoServicio.ObtenerPorCuil("20404392604");
            if(empleado1 == null)
            {
                var nuevoEmpleado = new EmpleadoDto
                {
                    Apellido = "Jassan",
                    Nombre = "Braian",
                    Legajo = _empleadoServicio.ObtenerSiguienteLegajo(),
                    Barrio = "Barrio Sur",
                    Calle = "Catamarca",
                    Casa = "1",
                    Celular = "4546465",
                    Cuil = "20404392604",
                    Dni = "46545450",
                    Dpto = "A",
                    Email = "braian@gmail.com",
                    FechaNacimiento = DateTime.Parse("1997/06/07"),
                    Lote = "A",
                    Mza = "A",
                    Numero = int.TryParse("450", out var numero) ? numero : 0,
                    Piso = "2",
                    Telefono = "4546465",
                    LocalidadId = _localidadServicio.ObtenerPorDescripcion("San Miguel de Tucuman").Id,
                    ProvinciaId = _provinciaServicio.ObtenerPorDescripcion("Tucuman").Id,
                    TipoEmpleadoId = _tipoEmpleadoServicio.ObtenerPorDescripcion("Cajero").Id,
                    Foto = null,    
                    EstaEliminado = false,
                    FechaIngreso = DateTime.Now,
                };

                _empleadoServicio.Insertar(nuevoEmpleado);

            }
            var empleado2 = _empleadoServicio.ObtenerPorCuil("20444444444");
            if (empleado2 == null)
            {
                var nuevoEmpleado = new EmpleadoDto
                {
                    Apellido = "Juarez",
                    Nombre = "Pepe",
                    Legajo = _empleadoServicio.ObtenerSiguienteLegajo(),
                    Barrio = "Barrio Sur",
                    Calle = "Catamarca",
                    Casa = "1",
                    Celular = "4546465",
                    Cuil = "20444444444",
                    Dni = "46545450",
                    Dpto = "A",
                    Email = "braian@gmail.com",
                    FechaNacimiento = DateTime.Parse("1997/06/07"),
                    Lote = "A",
                    Mza = "A",
                    Numero = int.TryParse("450", out var numero) ? numero : 0,
                    Piso = "2",
                    Telefono = "4546465",
                    LocalidadId = _localidadServicio.ObtenerPorDescripcion("San Miguel de Tucuman").Id,
                    ProvinciaId = _provinciaServicio.ObtenerPorDescripcion("Tucuman").Id,
                    TipoEmpleadoId = _tipoEmpleadoServicio.ObtenerPorDescripcion("Cadete").Id,
                    Foto = null,
                    EstaEliminado = false,
                    FechaIngreso = DateTime.Now,
                };

                _empleadoServicio.Insertar(nuevoEmpleado);

            }
            var empleado3 = _empleadoServicio.ObtenerPorCuil("20555555554");
            if (empleado3 == null)
            {
                var nuevoEmpleado = new EmpleadoDto
                {
                    Apellido = "Jorge",
                    Nombre = "Juan",
                    Legajo = _empleadoServicio.ObtenerSiguienteLegajo(),
                    Barrio = "Barrio Sur",
                    Calle = "Catamarca",
                    Casa = "1",
                    Celular = "4546465",
                    Cuil = "20555555554",
                    Dni = "46545450",
                    Dpto = "A",
                    Email = "braian@gmail.com",
                    FechaNacimiento = DateTime.Parse("1997/06/07"),
                    Lote = "A",
                    Mza = "A",
                    Numero = int.TryParse("450", out var numero) ? numero : 0,
                    Piso = "2",
                    Telefono = "4546465",
                    LocalidadId = _localidadServicio.ObtenerPorDescripcion("San Miguel de Tucuman").Id,
                    ProvinciaId = _provinciaServicio.ObtenerPorDescripcion("Tucuman").Id,
                    TipoEmpleadoId = _tipoEmpleadoServicio.ObtenerPorDescripcion("Mozo").Id,
                    Foto = null,
                    EstaEliminado = false,
                    FechaIngreso = DateTime.Now,
                };

                _empleadoServicio.Insertar(nuevoEmpleado);

            }

            // Usuario
            var empleadoCreado = _empleadoServicio.ObtenerPorCuil("20404392604");
            var usuario = _usuarioServicio.ObtenerUsuarioPorEmpleadoId(empleadoCreado.Id);
            if (usuario == null)
            {
                _usuarioServicio.Crear(empleadoCreado.Id, empleadoCreado.Apellido, empleadoCreado.Nombre);
            }

            // Cliente
            var cliente = _clienteServicio.ObtenerClientePorCuil("99999999999");
            if (cliente == null)
            {
                var nuevoCliente = new ClienteDto
                {
                    Apellido = "Final",
                    Nombre = "Consumidor",
                    Barrio = "1",
                    Calle = "1",
                    Casa = "1",
                    Celular = "1",
                    Cuil = "99999999999",
                    Dni = "99999999",
                    Dpto = "1",
                    Email = "1",
                    FechaNacimiento = DateTime.Parse("1997/06/07"),
                    Lote = "1",
                    Mza = "1",
                    Numero = int.TryParse("1", out var numero) ? numero : 0,
                    Piso = "1",
                    Telefono = "1",
                    LocalidadId = _localidadServicio.ObtenerPorDescripcion("San Miguel de Tucuman").Id,
                    ProvinciaId = _provinciaServicio.ObtenerPorDescripcion("Tucuman").Id,
                    Foto = null,    
                    MontoMaximoCtaCte = 10000,
                    SaldoCtaCte = 0,
                    EstaEliminado = false

                };

                _clienteServicio.Insertar(nuevoCliente);
            }
            var cliente2 = _clienteServicio.ObtenerClientePorCuil("20333333334");
            if (cliente2 == null)
            {
                var nuevoCliente = new ClienteDto
                {
                    Apellido = "Fernandez",
                    Nombre = "Facundo",
                    Barrio = "1",
                    Calle = "1",
                    Casa = "1",
                    Celular = "1",
                    Cuil = "20333333334",
                    Dni = "33333333",
                    Dpto = "1",
                    Email = "1",
                    FechaNacimiento = DateTime.Parse("1997/06/07"),
                    Lote = "1",
                    Mza = "1",
                    Numero = int.TryParse("1", out var numero) ? numero : 0,
                    Piso = "1",
                    Telefono = "1",
                    LocalidadId = _localidadServicio.ObtenerPorDescripcion("San Miguel de Tucuman").Id,
                    ProvinciaId = _provinciaServicio.ObtenerPorDescripcion("Tucuman").Id,
                    Foto = null,
                    EstaEliminado = false

                };

                _clienteServicio.Insertar(nuevoCliente);
            }

            // Cargo listasDePrecios
            var listaPrecioCosto = _listaPrecioServicio.ObtenerPorDescripcion("Costo");
            if (listaPrecioCosto == null)
            {
                var nuevaListaPrecio = new ListaPrecioDto
                {
                    Descripcion = "Costo",
                    Rentabilidad = 0,
                    EstaEliminado = false
                };

                _listaPrecioServicio.Insertar(nuevaListaPrecio);
            }
            var listaPrecioKiosco = _listaPrecioServicio.ObtenerPorDescripcion("Kiosco");
            if (listaPrecioKiosco == null)
            {
                var nuevaListaPrecio = new ListaPrecioDto
                {
                    Descripcion = "Kiosco",
                    Rentabilidad = 10,
                    EstaEliminado = false
                };

                _listaPrecioServicio.Insertar(nuevaListaPrecio);
            }
            var listaPrecioDelivery = _listaPrecioServicio.ObtenerPorDescripcion("Delivery");
            if (listaPrecioDelivery == null)
            {
                var nuevaListaPrecio = new ListaPrecioDto
                {
                    Descripcion = "Delivery",
                    Rentabilidad = 20,
                    EstaEliminado = false
                };

                _listaPrecioServicio.Insertar(nuevaListaPrecio);
            }
            var listaPrecioBar = _listaPrecioServicio.ObtenerPorDescripcion("Bar");
            if (listaPrecioBar == null)
            {
                var nuevaListaPrecio = new ListaPrecioDto
                {
                    Descripcion = "Bar",
                    Rentabilidad = 30,
                    EstaEliminado = false
                };

                _listaPrecioServicio.Insertar(nuevaListaPrecio);
            }

            // Condicion Iva
            var condicionIva1 = _condicionIvaServicio.ObtenerPorDescripcion("Responsable Inscripto");
            if (condicionIva1 == null)
            {
                var nuevaCondicionIva = new CondicionIvaDto
                {
                    Descripcion = "Responsable Inscripto",
                    EstaEliminado = false
                };

                _condicionIvaServicio.Insertar(nuevaCondicionIva);
            }
            var condicionIva2 = _condicionIvaServicio.ObtenerPorDescripcion("Monotributista");
            if (condicionIva2 == null)
            {
                var nuevaCondicionIva = new CondicionIvaDto
                {
                    Descripcion = "Monotributista",
                    EstaEliminado = false
                };

                _condicionIvaServicio.Insertar(nuevaCondicionIva);
            }
            var condicionIva3 = _condicionIvaServicio.ObtenerPorDescripcion("Consumidor Final");
            if (condicionIva3 == null)
            {
                var nuevaCondicionIva = new CondicionIvaDto
                {
                    Descripcion = "Consumidor Final",
                    EstaEliminado = false
                };

                _condicionIvaServicio.Insertar(nuevaCondicionIva);
            }

            // Marca
            var marca1 = _marcaServicio.ObtenerPorDescripcion("CocaCola");
            if (marca1 == null)
            {
                var nuevaMarca = new MarcaDto
                {
                    Descripcion = "CocaCola",
                    EstaEliminado = false,
                };

                _marcaServicio.Insertar(nuevaMarca);
            }
            var marca2 = _marcaServicio.ObtenerPorDescripcion("Pepsi");
            if (marca2 == null)
            {
                var nuevaMarca = new MarcaDto
                {
                    Descripcion = "Pepsi",
                    EstaEliminado = false,
                    
                };

                _marcaServicio.Insertar(nuevaMarca);
            }

            // Rubro
            var marca = _marcaServicio.ObtenerPorDescripcion("CocaCola");
            var rubro = _rubroServicio.ObtenerPorDescripcion("Gaseosa");
            if (rubro == null)
            {
                var nuevoRubro = new RubroDto
                {
                    Descripcion = "Gaseosa",
                    EstaEliminado = false
                };

                _rubroServicio.Insertar(nuevoRubro);
            }

            // Articulo
            /*var articulo1 = _articuloServicio.ObtenerPorCodigo("1");
            if (articulo1 == null)
            {
                var nuevoArticulo = new ArticuloDto
                {
                    Codigo = "1",
                    CodigoBarra = "1",
                    Abreviatura = "Pepsi 2L",
                    Descripcion = "Pepsi 2L",
                    Detalle = "Pepsi 2L",
                    Foto = null,
                    ActivarLimiteVenta = false,
                    LimiteVenta = 0,
                    EstaDiscontinuado = false,
                    StockMaximo = 100,
                    StockMinimo = 0,
                    DescuentaStock = true,
                    MarcaId = _marcaServicio.ObtenerPorDescripcion("Pepsi").Id,
                    RubroId = _rubroServicio.ObtenerPorDescripcion("Gaseosa").Id,
                    PrecioCosto = 100,
                    Stock = 50,
                    PermiteStockNegativo = false,
                    EstaEliminado = false
                };

                _articuloServicio.Insertar(nuevoArticulo);
            }*/

            
            ///////////// Final de Verificacion de Registros base en DB /////////////


            // Lanzo el formulario de Login del Sistema
            var fLogin = new Login(new AccesoSistema(), new UsuarioServicio());
            fLogin.ShowDialog(); // Abrir el formulario
            
            // verifico si puede o no acceder
            if (fLogin.PuedeAccederSistema)
            {
                Application.Run(new Principal());
            }
            else
            {
                Application.Exit(); // Cierra la Aplicacion Completa
            }
        }
    }
}
