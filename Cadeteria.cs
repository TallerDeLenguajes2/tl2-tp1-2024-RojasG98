class Cadeteria
{
    private string nombre;
    private string telefono;
    private List<Cadete> listadoCadetes;
    private List<Pedido> listadoTotalPedidos;

    private void atenderLlamada(){
        Console.WriteLine("Desea cargar pedido?\n1.SI\n2.NO");
        char respuesta = Console.ReadKey().KeyChar;
        if (respuesta == '1')
        {
            listadoTotalPedidos.Add(tomarPedido());
        }

    }
    private Pedido tomarPedido(){
        string nombre,direccion,telefono,datosReferenciaDireccion,obs;
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
        return new Pedido(crearNumeroPedido(listadoTotalPedidos),obs,nombre,direccion,datosReferenciaDireccion,telefono);
    }
    private int crearNumeroPedido(List<Pedido> listadoTotal){
        int cant = listadoTotal.Count;
        if(cant != 0){
            var aux = listadoTotal.ToArray();
            int ultimoNumero = aux[cant].Nro;
            return ultimoNumero++;
        }
        else
        {
            return 1000;
        }
    }
}