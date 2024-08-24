class Cadeteria
{
    private string nombre;
    private string telefono;
    private List<Cadete> listadoCadetes;

    private List<Pedido> listadoTotalPedidos;
    private void asignarPedido(Pedido nuevoPedido)
    {
        int aux = 0;
        int id = 0;
        Cadete cadeteAux ;
        foreach (var cadete in listadoCadetes)
        {
            if (aux < cadete.ListadoPedidos.Count)
            {
                id = cadete.Id;
                aux = cadete.ListadoPedidos.Count;
            }
        }
        if(listadoCadetes.Exists(x => x.Id == id)){
            cadeteAux =  listadoCadetes.Find(x => x.Id == id);
            cadeteAux.agregarPedido(nuevoPedido);
        }
        else
        {
            Console.WriteLine("Error al agregar el pedido");
        }
    }
    private void reasignarPedido(){
        int id,numero;
        Cadete cadeteAux;
        Pedido pedidoAux;
        mostrarCadetes();
        Console.WriteLine("Coloque el numero de cadete");
        id = Int32.Parse(Console.ReadLine());
        cadeteAux = listadoCadetes.Find(x => x.Id == id);
        Console.Clear();
        cadeteAux.mostrarPedidosCadete();
        Console.WriteLine("Coloque el numero de pedido a reasignar");
        numero = Int32.Parse(Console.ReadLine());
        pedidoAux = cadeteAux.ListadoPedidos.Find(x => x.Nro == numero);
        cadeteAux.borrarPedido(pedidoAux);
        Console.WriteLine("Borrado Con exito");
        Thread.Sleep(1000);
        Console.Clear();
        Console.WriteLine("Coloque el numero de cadete a reasignar el pedido");
        id = Int32.Parse(Console.ReadLine());
        cadeteAux = listadoCadetes.Find(x => x.Id == id);
        cadeteAux.agregarPedido(pedidoAux);
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
            listadoTotalPedidos.Add(nuevoPedido);
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
        int cant = listadoTotalPedidos.Count;
        if (cant != 0)
        {
            var aux = listadoTotalPedidos.ToArray();
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
                Console.Write("\n");
            }
        }

    }
}