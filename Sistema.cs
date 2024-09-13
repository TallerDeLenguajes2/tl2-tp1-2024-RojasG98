using System.Security.Cryptography.X509Certificates;

class Sistema
{
    public Cadeteria cargarSucursal()
    {
        AccesoADatos archivoSucursal = new AccesoADatos();
        List<Cadete> cadetesAux;
        Cadeteria sucursal;
        sucursal = archivoSucursal.LeerSucursal();
        cadetesAux = archivoSucursal.LeerCadetes();
        return new Cadeteria(sucursal.Nombre, sucursal.Telefono, cadetesAux, new List<Pedido>());
    }
    public int menuPrincipal()
    {
        bool respuesta;
        int numero;
        Console.WriteLine("1. Dar de alta pedidos");
        Console.WriteLine("2. Asignarlos a cadetes");
        Console.WriteLine("3. Cambiarlos de estado");
        Console.WriteLine("4. reasignar el pedido a otro cadete.");
        Console.WriteLine("5. Finalizar.");

        do
        {
            respuesta = Int32.TryParse(Console.ReadLine(), out numero);
            if (respuesta)
            {
                break;
            }
        } while (!respuesta && (numero == 1 || numero == 2 || numero == 3 || numero == 4 || numero == 5));
        return numero;
    }

    public bool hacerTarea(int tarea, Cadeteria sucursal)
    {
        switch (tarea)
        {
            case 1:
                sucursal.darAltaPedido();
                return true;
            case 2:
                sucursal.asignarPedido();
                return true;
            case 3:
                int idCadete, nroPedido, seleccionEstado;
                Estado nuevoEstado = Estado.Pendiente;
                sucursal.mostrarCadetes();
                Console.WriteLine("Elija el cadete");
                idCadete = Int32.Parse(Console.ReadLine());
                Cadete cadeteAux = sucursal.ListadoCadetes.Find(x => x.Id == idCadete);
                cadeteAux.mostrarPedidosCadete();
                Console.WriteLine("Elija el pedido");
                nroPedido = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Cual es el estado? 1. Entregado 2.Cancelado");
                do
                {
                    seleccionEstado = Int32.Parse(Console.ReadLine());
                    if (seleccionEstado == 1)
                    {
                        nuevoEstado = Estado.Entregado;
                    }
                    else
                    {
                        if (seleccionEstado == 2)
                        {
                            nuevoEstado = Estado.Cancelado;

                        }
                    }
                } while (seleccionEstado != 1 || seleccionEstado != 2);
                cadeteAux.cambiarEstadoPedido(nuevoEstado,nroPedido);
                return true;
            case 4:
                sucursal.reasignarPedido();
                return true;
            case 5:
                sucursal.mostrarInforme();
                Console.WriteLine("Presione Cualquier tecla para salir:");
                Console.ReadLine();
                return false;
            default:
                return true;
        }
    }
}