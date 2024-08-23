class Cadeteria
{
    private string nombre;
    private string telefono;
    private List<Cadete> listadoCadetes;

    private void atenderLlamada(){
        bool realizaPedido = false;
        if (realizaPedido)
        {
            tomarPedido();
        }

    }
    private void tomarPedido(){
        string nombre,direccion,telefono,datosReferenciaDireccion;
        Pedido nuevoPedido = new Pedido();
        Console.WriteLine("Ingrese Nombre Cliente:");
        nombre = Console.ReadLine();
        Console.WriteLine("Ingrese Direccion Cliente:");
        direccion = Console.ReadLine();
        Console.WriteLine("Ingrese Referencia de Direccion:");
        datosReferenciaDireccion = Console.ReadLine();
        Console.WriteLine("Ingrese Telefono:");
        telefono = Console.ReadLine();
        
    }
    private int crearNumeroPedido(){
        return numero;
    }
}