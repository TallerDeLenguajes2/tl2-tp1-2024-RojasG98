class Sistema
{
        public Cadeteria cargarSucursal() {
        AccesoADatos archivoSucursal = new AccesoADatos();
        List<Cadete> cadetesAux;
        Cadeteria sucursal;
        sucursal = archivoSucursal.LeerSucursal();
        cadetesAux = archivoSucursal.LeerCadetes();
        return new Cadeteria(sucursal.Nombre,sucursal.Telefono,cadetesAux,new List<Pedido>());
    }
    public int menuPrincipal()
    {
        bool respuesta;
        int numero;
        Console.WriteLine("1. Dar de alta pedidos");
        Console.WriteLine("2. Asignarlos a cadetes");
        Console.WriteLine("3. Cambiarlos de estado");
        Console.WriteLine("4. reasignar el pedido a otro cadete.");
        Console.WriteLine("5. Pagar cadete.");
        Console.WriteLine("6. Mostrar Pedidos.");
        Console.WriteLine("7. Finalizar.");

        do
        {
            respuesta = Int32.TryParse(Console.ReadLine(),out numero);
            if (respuesta)
            {
                break;
            }
        } while (!respuesta && (numero == 1 || numero == 2 || numero == 3 || numero == 4 || numero== 5));
        return numero;
    }

        public void hacerTarea(int tarea, Cadeteria sucursal){
        int id;
        float jornal;
        switch (tarea)
        {
            case 1:
                sucursal.darAltaPedido();
                break;
            case 2:
                sucursal.asignarPedido();
                break;
            case 3:
                sucursal.cambiarEstadoPedidos();
                break;
            case 4:
                sucursal.reasignarPedido();
                break;
            case 5:
                sucursal.mostrarCadetes();
                Console.WriteLine("Ingresse el id del cadete");
                id = Int32.Parse(Console.ReadLine());
                jornal = sucursal.jornalACobrar(id);
                Console.WriteLine($"El jornal a cobrar es: ${jornal:F2}");
                break;
            case 6:
                sucursal.mostrarPedidos();
                break;
        }


}