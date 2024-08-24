class Sistema
{
    const string archivoSucursales = "csv/Cadeterias.csv";

    private List<Cadeteria> sucursales;

    public void LeerSucursales()
    {
        using (StreamReader sr = new StreamReader(archivoSucursales))
        {
            string linea;
            sr.ReadLine();
            while ((linea = sr.ReadLine()) != null)
            {
                string[] valores = linea.Split(',');

                Cadeteria cadete = new Cadeteria(valores[0], valores[1], new List<Cadete>(), new List<Pedido>());

                sucursales.Add(cadete);
            }
        }
    }

    public Cadeteria elegirSucursal()
    {
        int cont = 1;
        string nombre;
        foreach (var sucursal in sucursales)
        {
            Console.WriteLine(cont + ". " + sucursal.Nombre);
            cont++;
        }
        Console.WriteLine("Escriba el nombre la sucursal:");
        nombre = Console.ReadLine();
        return sucursales.Find(x => x.Nombre.Contains(nombre));
    }

    public bool menuPrincipal()
    {
        
        Console.WriteLine("1. Dar de alta pedidos");
        Console.WriteLine("2. Asignarlos a cadetes");
        Console.WriteLine("3. Cambiarlos de estado");
        Console.WriteLine("4. reasignar el pedido a otro cadete.");
    }

}