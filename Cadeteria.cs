using System.Data.Common;

class Cadeteria
{
    private const float pagoPorEnvio = 500;
    const string archivoCadetes = "/csv";
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

    private void asignarPedido(Pedido nuevoPedido)
    {
        Cadete cadeteAux;
        mostrarCadetes();
        Console.WriteLine("Elija un cadete para asignar el pedido: ");
        int id = Int32.Parse(Console.ReadLine());
        if (listadoTotalPedidos.Exists(x => x.Nro == nuevoPedido.Nro))
        {
            cadeteAux = listadoCadetes.Find(x => x.Id == id);
            nuevoPedido.asignarCadete(cadeteAux);
            listadoTotalPedidos.Add(nuevoPedido);

        }
        else
        {
            Console.WriteLine("Error al agregar el pedido");
        }
    }
    private void reasignarPedido()
    {
        int id, numero;
        Cadete cadeteAux;
        Pedido pedidoAux;
        mostrarCadetes();
        Console.WriteLine("Coloque el numero de cadete");
        id = Int32.Parse(Console.ReadLine());
        Console.Clear();
        mostrarPedidosCadete(id);
        Console.WriteLine("Coloque el numero de pedido a reasignar");
        numero = Int32.Parse(Console.ReadLine());
        pedidoAux = ListadoTotalPedidos.Find(x => x.Nro == numero);
        borrarPedido(pedidoAux);
        Console.WriteLine("Borrado Con exito");
        Thread.Sleep(1000);
        Console.Clear();
        Console.WriteLine("Coloque el numero de cadete a reasignar el pedido");
        id = Int32.Parse(Console.ReadLine());
        cadeteAux = listadoCadetes.Find(x => x.Id == id);
        agregarPedido(pedidoAux);
        Console.WriteLine("Reasignacion completada!");
    }
    private void atenderLlamada()
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
            asignarPedido(nuevoPedido);
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
                Console.WriteLine("Pedido Pendientes: ", contarPedidos(cadete.Id, Estado.Pendiente));
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
            if (pedidos.Estado == estadoDelPedido && pedidos.Cadete.Id == id)
            {
                entregado++;
            }
        }
        return entregado;
    }
    public float jornalACobrar(int idCadete)
    {
        return pagoPorEnvio * contarPedidos(idCadete, Estado.Entregado);
    }

    public void mostrarPedidosCadete()
    {
        foreach (var pedidos in listadoTotalPedidos)
        {
            Console.WriteLine("Id: ", pedidos.Nro);
            pedidos.verDatosCliente();
        }
    }
    public void mostrarPedidosCadete(int id)
    {
        foreach (var pedidos in listadoTotalPedidos)
        {
            if (pedidos.Cadete.Id == id)
            {
                Console.WriteLine("Id: ", pedidos.Nro);
                pedidos.verDatosCliente();
            }

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
}