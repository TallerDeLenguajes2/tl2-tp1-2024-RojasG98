class Cadeteria
{
    private string nombre;
    private string telefono;
    private List<Cadete> listadoCadetes;

    private List<Pedido> listadoTotalPedidos;
    private void asignarPedido(Pedido nuevoPedido, List<Cadete> listadoCadetes)
    {
        int aux = 0;
        int id = 0;
        foreach (var cadete in listadoCadetes)
        {
            if (aux < cadete.ListadoPedidos.Count)
            {
                id = cadete.Id;
                aux = cadete.ListadoPedidos.Count;
            }
        }
        foreach (var cadete in listadoCadetes)
        {
            if (id == cadete.Id)
            {
                cadete.ListadoPedidos.Add(nuevoPedido);
            }
            else
            {
                Console.WriteLine("Error al asignar pedido");
            }
        }
    }
    private void reasignarPedido(List<Pedido> listadoPedidos,List<Cadete> listadoCadetes){
        
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
            asignarPedido(nuevoPedido, listadoCadetes);
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
        return new Pedido(crearNumeroPedido(listadoTotalPedidos), obs, nombre, direccion, datosReferenciaDireccion, telefono);
    }
    private int crearNumeroPedido(List<Pedido> listadoTotal)
    {
        int cant = listadoTotal.Count;
        if (cant != 0)
        {
            var aux = listadoTotal.ToArray();
            int ultimoNumero = aux[cant].Nro;
            return ultimoNumero++;
        }
        else
        {
            return 1000;
        }
    }

    private void mostrarCadetes(List<Cadete> listaCadetes)
    {
        if (listaCadetes.Count == 0)
        {
            Console.WriteLine("No hay cadetes disponibles");
        }
        else
        {
            foreach (var cadete in listaCadetes)
            {
                Console.WriteLine("id: ", cadete.Id);
                Console.WriteLine("Nombre: ", cadete.Nombre);
                Console.Write("\n");
            }
        }

    }
}