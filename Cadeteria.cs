using System.Data.Common;
using System.Text.Json.Serialization;

class Cadeteria
{
    private const float pagoPorEnvio = 500;
    
    private string nombre;
    private string telefono;
    private List<Cadete> listadoCadetes;
    private List<Pedido> listadoTotalPedidos;

    public Cadeteria(){}
    public Cadeteria(string nombre, string telefono, List<Cadete> listadoCadetes, List<Pedido> listadoTotalPedidos)
    {
        this.nombre = nombre;
        this.telefono = telefono;
        this.listadoCadetes = listadoCadetes;
        this.listadoTotalPedidos = listadoTotalPedidos;
    }

    public string Nombre { get => nombre; set => nombre = value; }
    public string Telefono{get => telefono; set => telefono = value;}
    public List<Pedido> ListadoTotalPedidos { get => listadoTotalPedidos; set => listadoTotalPedidos = value;}
    internal List<Cadete> ListadoCadetes { get => listadoCadetes; set => listadoCadetes = value; }

    public bool asignarPedido(int id, Pedido pedido)
    {
        Cadete cadeteAux;
        if (pedido != null)
        {
            if (listadoTotalPedidos.Exists(x => x.Nro == pedido.Nro))
            {
                cadeteAux = listadoCadetes.Find(x => x.Id == id);
                pedido.asignarCadete(cadeteAux);
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;

    }

    // private Pedido elegirPedido()
    // {
    //     Pedido pedidoAux;
    //     if (mostrarPedidosSinAsignar())
    //     {
    //         Console.WriteLine("Elija un pedido para asignar: ");
    //         int nroPedido = Int32.Parse(Console.ReadLine());
    //         pedidoAux = ListadoTotalPedidos.Find(x => x.Nro == nroPedido);
    //     }
    //     else
    //     {
    //         pedidoAux = null;
    //     }

    //     return pedidoAux;
    // }

    public bool reasignarPedido(int idCadete, int idPedido)
    {
        Cadete cadeteAux;
        Pedido pedidoAux;
        pedidoAux = ListadoTotalPedidos.Find(x => x.Nro == idPedido);
        Console.WriteLine("Coloque el numero de cadete a reasignar el pedido");
        cadeteAux = listadoCadetes.Find(x => x.Id == idCadete);
        pedidoAux.asignarCadete(cadeteAux);
        return true;
    }
    public bool darAltaPedido(int respuesta)
    {
        if (respuesta == '1')
        {
            Pedido nuevoPedido = tomarPedido();
            ListadoTotalPedidos.Add(nuevoPedido);
            return true;
        }
        else
        {
            return false;
        }

    }
    private Pedido tomarPedido()
    {
        string nombre, direccion, telefono, datosReferenciaDireccion, obs;
        Console.WriteLine("Ingrese Nombre Cliente:");
        nombre = Console.ReadLine();
        Console.WriteLine("Ingrese Direccion Cliente:");
        direccion = Console.ReadLine();
        Console.WriteLine("Ingrese Referencia de Direccion:");
        datosReferenciaDireccion = Console.ReadLine();
        Console.WriteLine("Ingrese Telefono:");
        telefono = Console.ReadLine();
        Console.WriteLine("Ingrese observacion:");
        obs = Console.ReadLine();
        return new Pedido(crearNumeroPedido(), obs, nombre, direccion, datosReferenciaDireccion, telefono);
    }
    private int crearNumeroPedido()
    {
        int cant = ListadoTotalPedidos.Count;
        if (cant != 0)
        {
            var aux = ListadoTotalPedidos.ToArray();
            int ultimoNumero = aux[cant-1].Nro;
            return ultimoNumero+1;
        }
        else
        {
            return 1000;
        }
    }

    public void mostrarCadetes()
    {
        if (listadoCadetes.Count == 0)
        {
            Console.WriteLine("No hay cadetes disponibles");
        }
        else
        {
            foreach (var cadete in listadoCadetes)
            {
                Console.WriteLine($"id: {cadete.Id}");
                Console.WriteLine($"Nombre: {cadete.Nombre}");
                Console.WriteLine($"Pedido Pendientes: {contarPedidos(cadete.Id, Estado.Pendiente)}");
                Console.Write("\n");
            }
        }
    }
    public int contarPedidos(int id, Estado estadoDelPedido)
    {
        int entregado = 0;
        foreach (var pedidos in listadoTotalPedidos)
        {
            if (pedidos.Cadete != null)
            {
                if (pedidos.Estado == estadoDelPedido && pedidos.Cadete.Id == id)
                {
                    entregado++;
                }
            }
        }
        return entregado;
    }
    public float jornalACobrar(int idCadete)
    {
        return pagoPorEnvio * contarPedidos(idCadete, Estado.Entregado);
    }

    public void mostrarPedidos()
    {
       foreach (var pedidos in listadoTotalPedidos)
        {
            Console.WriteLine($"Id: {pedidos.Nro}");
            pedidos.verDatosCliente();
        }

    }

    public bool mostrarPedidosSinAsignar()
    {
        foreach (var pedidos in listadoTotalPedidos)
        {
            if (pedidos.Cadete == null)
            {

            }
            Console.WriteLine($"Id: {pedidos.Nro}");
            pedidos.verDatosCliente();
        }
                IEnumerable<Pedido> pedidosAux = listadoTotalPedidos.TakeWhile(pedidos => pedidos.Cadete == null);
        pedidosAux.ToList();
        if (pedidosAux.Count() > 0)
        {
            foreach (var pedidos in listadoTotalPedidos)
            {
                Console.WriteLine($"Id: {pedidos.Nro}");
                pedidos.verDatosCliente();
            }
            return true;
        }
        else
        {
            Console.WriteLine("No existen pedidos nuevos");
            return false;
        }
    }

    public void borrarPedido(Pedido pedidoABorrar)
    {
        listadoTotalPedidos.Remove(pedidoABorrar);
    }

    public void agregarPedido(Pedido pedidoAAgregar)
    {
        listadoTotalPedidos.Add(pedidoAAgregar);
    }

    // private Pedido seleccionPedido()
    // {
    //     int numero;
    //     bool respuesta, encontrado;
    //     mostrarPedidosSinAsignar();
    //     Console.WriteLine("Ingrese el id del pedido a actualizar.");
    //     do
    //     {
    //         respuesta = Int32.TryParse(Console.ReadLine(), out numero);
    //         if (respuesta)
    //         {
    //             Console.WriteLine("cargando pedido...");
    //             encontrado = listadoTotalPedidos.Exists(pedido => pedido.Nro == numero);
    //             if (!encontrado)
    //             {
    //                 Console.WriteLine("No se encontro pedido");
    //             }
    //         }
    //         else
    //         {
    //             Console.WriteLine("error en la seleccion");
    //             encontrado = false;
    //         }
    //     } while (!encontrado);
    //     return listadoTotalPedidos.Find(pedido => pedido.Nro == numero);
    // }

    public void cambiarEstadoPedidos(Pedido pedido, char estado)
    {
        pedido.cambiarEstado(estado);
    }
    //     public void mostrarInforme(){
    //     Console.WriteLine("-----------Informe del Dia-----------");
    //     Console.WriteLine("-----------Envios Cadete-----------");

    //     int cantidadEnviodelDia = 0;
    //     foreach (var cadetes in listadoCadetes)
    //     {
    //         Console.WriteLine($"Id Cadete: {cadetes.Id}");
    //         Console.WriteLine($"Nombre: {cadetes.Nombre}");
    //         int entregadosCadete = listadoTotalPedidos.TakeWhile(X => X.Estado == Estado.Entregado && X.Cadete.Id == cadetes.Id).Count();
    //         cantidadEnviodelDia += entregadosCadete;
    //         Console.WriteLine($"Envios entregados: {entregadosCadete}");
    //     }
    //     int cantidadDeCadetes = listadoCadetes.Count();
    //     float promedioDeEnvios = cantidadEnviodelDia/cantidadDeCadetes;
    //     Console.WriteLine($"Envios Totales:{cantidadEnviodelDia}");
    //     Console.WriteLine($"Promedio del Dia:{promedioDeEnvios}");

    // }
}