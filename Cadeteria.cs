using System.Data.Common;

class Cadeteria
{
    private const float pagoPorEnvio = 500;
    const string archivoCadetes = "csv/Cadetes.csv";
    private string nombre;
    private string telefono;
    private List<Cadete> listadoCadetes;

    private List<Pedido> listadoTotalPedidos;

    public Cadeteria(string nombre, string telefono, List<Cadete> cadetes, List<Pedido> pedidos)
    {
        this.nombre = nombre;
        this.telefono = telefono;
        listadoCadetes = cadetes;
        listadoTotalPedidos = pedidos;
    }

    public string Nombre { get => nombre; }
    public List<Pedido> ListadoTotalPedidos { get => listadoTotalPedidos; }

    public void asignarPedido()
    {
        Cadete cadeteAux;
        Pedido pedidoAux;
        pedidoAux = elegirPedido();
        if (pedidoAux != null)
        {
            mostrarCadetes();
            Console.WriteLine("Elija un cadete para asignar el pedido: ");
            int id = Int32.Parse(Console.ReadLine());
            if (listadoTotalPedidos.Exists(x => x.Nro == pedidoAux.Nro))
            {
                cadeteAux = listadoCadetes.Find(x => x.Id == id);
                pedidoAux.asignarCadete(cadeteAux);
            }
            else
            {
                Console.WriteLine("Error al agregar el pedido");
            }
        }

    }

    private Pedido elegirPedido()
    {
        Pedido pedidoAux;
        if (mostrarPedidosSinAsignar())
        {
            Console.WriteLine("Elija un pedido para asignar: ");
            int nroPedido = Int32.Parse(Console.ReadLine());
            pedidoAux = ListadoTotalPedidos.Find(x => x.Nro == nroPedido);
        }
        else
        {
            pedidoAux = null;
        }

        return pedidoAux;
    }

    public void reasignarPedido()
    {
        int id, numero;
        Cadete cadeteAux;
        Pedido pedidoAux;
        mostrarCadetes();
        Console.WriteLine("Coloque el numero de cadete");
        id = Int32.Parse(Console.ReadLine());
        Console.Clear();
        mostrarPedidos();
        Console.WriteLine("Coloque el numero de pedido a reasignar");
        numero = Int32.Parse(Console.ReadLine());
        pedidoAux = ListadoTotalPedidos.Find(x => x.Nro == numero);
        Thread.Sleep(1000);
        Console.Clear();
        Console.WriteLine("Coloque el numero de cadete a reasignar el pedido");
        id = Int32.Parse(Console.ReadLine());
        cadeteAux = listadoCadetes.Find(x => x.Id == id);
        pedidoAux.asignarCadete(cadeteAux);
        Console.WriteLine("Reasignacion completada!");
    }
    public void darAltaPedido()
    {
        Console.WriteLine("Desea cargar pedido?\n1.SI\n2.NO");
        char respuesta;
        do
        {
            respuesta = Console.ReadKey().KeyChar;
        } while (respuesta == 1 || respuesta == 2);
        if (respuesta == '1')
        {
            Pedido nuevoPedido = tomarPedido();
            ListadoTotalPedidos.Add(nuevoPedido);
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
            int ultimoNumero = aux[cant].Nro;
            return ultimoNumero++;
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
    public void LeerCadetes()
    {
        List<string> lineas = new List<string>();

        using (StreamReader sr = new StreamReader(archivoCadetes))
        {
            string linea;
            sr.ReadLine();
            while ((linea = sr.ReadLine()) != null)
            {
                string[] valores = linea.Split(',');

                Cadete cadete = new Cadete(int.Parse(valores[0]), valores[1], valores[2], valores[3], new List<Pedido>());

                listadoCadetes.Add(cadete);
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

    private Pedido seleccionPedido()
    {
        int numero;
        bool respuesta, encontrado;
        mostrarPedidosSinAsignar();
        Console.WriteLine("Ingrese el id del pedido a actualizar.");
        do
        {
            respuesta = Int32.TryParse(Console.ReadLine(), out numero);
            if (respuesta)
            {
                Console.WriteLine("cargando pedido...");
                encontrado = listadoTotalPedidos.Exists(pedido => pedido.Nro == numero);
                if (!encontrado)
                {
                    Console.WriteLine("No se encontro pedido");
                }
            }
            else
            {
                Console.WriteLine("error en la seleccion");
                encontrado = false;
            }
        } while (!encontrado);
        return listadoTotalPedidos.Find(pedido => pedido.Nro == numero);
    }

    public void cambiarEstadoPedidos()
    {
        Pedido pedidoACambiar = seleccionPedido();
        pedidoACambiar.cambiarEstado();
    }
}