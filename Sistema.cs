using System.Formats.Asn1;
using System.Runtime.InteropServices;
class Sistema
{

    public Cadeteria cargarSucursal()
    {
        AccesoADatos archivoSucursal;
        List<Cadete> cadetesAux;
        Cadeteria sucursal;
        Console.WriteLine("Elija el tipo de archivo que se leera: 1.CSV 2.JSON");
        int tipoArchivo;
        do
        {
            tipoArchivo = Int32.Parse(Console.ReadLine());
        } while (tipoArchivo != 1 && tipoArchivo != 2);
        if (tipoArchivo == 1)
        {
            archivoSucursal = new AccesoCSV();
        }
        else
        {
            archivoSucursal = new AccesoJSON();
        }
        sucursal = archivoSucursal.LeerSucursales();
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
        Console.WriteLine("5. Pagar cadete.");
        Console.WriteLine("6. Mostrar Pedidos.");
        Console.WriteLine("7. SALIR.");

        do
        {
            respuesta = Int32.TryParse(Console.ReadLine(), out numero);
            if (respuesta)
            {
                Console.WriteLine("cargando sistema...");
            }
            else
            {
                Console.WriteLine("opcion no valida");
            }
        } while (!respuesta && numero < 7 && numero > 0);
        return numero;
    }

    public bool hacerTarea(int tarea, Cadeteria sucursal)
    {
        int id;
        float jornal;
        switch (tarea)
        {
            case 1:
                sucursal.darAltaPedido();
                return true;
            case 2:
                sucursal.asignarPedido();
                return true;
            case 3:
                sucursal.cambiarEstadoPedidos();
                return true;
            case 4:
                sucursal.reasignarPedido();
                return true;
            case 5:
                sucursal.mostrarCadetes();
                Console.WriteLine("Ingresse el id del cadete");
                id = Int32.Parse(Console.ReadLine());
                jornal = sucursal.jornalACobrar(id);
                Console.WriteLine($"El jornal a cobrar es: ${jornal:F2}");
                return true;

            case 6:
                sucursal.mostrarPedidos();
                return true;

            case 7:
                sucursal.mostrarInforme();
                Console.WriteLine("Presione Cualquier tecla para salir:");
                Console.ReadLine();
                return false;
            default:
                return true;
        }
    }

}
