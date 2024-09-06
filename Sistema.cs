using System.Formats.Asn1;

class Sistema
{
    const string archivoSucursales = "csv/Cadeterias.csv";

    private List<Cadeteria> sucursales;

    public List<Cadeteria> Sucursales { get => sucursales; }

    public void LeerSucursales()
    {
        List<Cadeteria> sucursal = new List<Cadeteria>();
        using (StreamReader sr = new StreamReader(archivoSucursales))
        {
            string linea;
            sr.ReadLine();
            while ((linea = sr.ReadLine()) != null)
            {
                string[] valores = linea.Split(',');

                Cadeteria cadete = new Cadeteria(valores[0], valores[1], new List<Cadete>(), new List<Pedido>());

                sucursal.Add(cadete);
            }
        }
        sucursales = sucursal;
    }

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
        seleccionada.LeerCadetes();
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
        Console.WriteLine("5. SALIR.");

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
        }
    }

}
