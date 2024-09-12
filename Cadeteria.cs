class Cadeteria
{
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
    public string Telefono { get => telefono; }
    internal List<Pedido> ListadoTotalPedidos { get => listadoTotalPedidos; }

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
            if (ListadoTotalPedidos.Exists(x => x.Nro == pedidoAux.Nro))
            {
                cadeteAux = listadoCadetes.Find(x => x.Id == id);
                cadeteAux.agregarPedido(pedidoAux);
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
        if (mostrarPedidos())
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
    private void reasignarPedido()
    {
        int id, numero;
        Cadete cadeteAux;
        Pedido pedidoAux;
        mostrarCadetes();
        Console.WriteLine("Coloque el numero de cadete");
        id = Int32.Parse(Console.ReadLine());
        cadeteAux = listadoCadetes.Find(x => x.Id == id);
        cadeteAux.mostrarPedidosCadete();
        Console.WriteLine("Coloque el numero de pedido a reasignar");
        numero = Int32.Parse(Console.ReadLine());
        pedidoAux = cadeteAux.ListadoPedidos.Find(x => x.Nro == numero);
        cadeteAux.borrarPedido(pedidoAux);
        Console.WriteLine("Borrado Con exito");
        Console.WriteLine("Coloque el numero de cadete a reasignar el pedido");
        id = Int32.Parse(Console.ReadLine());
        cadeteAux = listadoCadetes.Find(x => x.Id == id);
        cadeteAux.agregarPedido(pedidoAux);
        Console.WriteLine("Reasignacion completada!");
    }


    private void altaPedido()
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
            int ultimoNumero = aux[cant - 1].Nro;
            return ultimoNumero + 1;
        }
        else
        {
            return 1000;
        }
    }

    private void mostrarCadetes()
    {
        if (listadoCadetes.Count == 0)
        {
            Console.WriteLine("No hay cadetes disponibles");
        }
        else
        {
            foreach (var cadete in listadoCadetes)
            {
                Console.WriteLine("id: ", cadete.Id);
                Console.WriteLine("Nombre: ", cadete.Nombre);
                Console.Write("\n");
            }
        }
    }

    private void mostrarPedidos()
    {
        foreach (var pedido in listadoTotalPedidos)
        {
            Console.WriteLine("id: ", pedido.Nro);
            pedido.verDatosCliente();
            Console.Write("\n");

        }
    }

}