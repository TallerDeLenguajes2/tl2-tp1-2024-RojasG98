using System.Formats.Asn1;
class Sistema
{

    private List<Cadeteria> sucursales;

    public List<Cadeteria> Sucursales { get => sucursales; }



    public Cadeteria elegirSucursal()
    {
        int cont = 1;
        string nombre;
        foreach (var sucursal in Sucursales)
        {
            Console.WriteLine(cont + ". " + sucursal.Nombre);
            cont++;
        }
        Console.WriteLine("Escriba el nombre la sucursal:");
        nombre = Console.ReadLine();
        Cadeteria seleccionada = Sucursales.Find(x => x.Nombre.Contains(nombre));
        seleccionada = AccesoADatos.LeerCadetes();
        return seleccionada;
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
        Console.WriteLine("6. SALIR.");

        do
        {
            respuesta = Int32.TryParse(Console.ReadLine(),out numero);
            if (respuesta)
            {
                Console.WriteLine("cargando sistema...");
            }
            else
            {
                Console.WriteLine("opcion no valida");
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
        }
    }

}
