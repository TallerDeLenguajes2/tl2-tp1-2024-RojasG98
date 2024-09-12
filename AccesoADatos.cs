
class AccesoADatos
{
    const string archivoCadetes = "csv/cadetes.csv";

    const string archivoSucursales = "csv/Cadeterias.csv";

    public Cadeteria LeerSucursal()
    {
        Cadeteria sucursal;
        using (StreamReader sr = new StreamReader(archivoSucursales))
        {
            string linea;
            sr.ReadLine();
            linea = sr.ReadLine();
            string[] valores = linea.Split(',');
            sucursal = new Cadeteria(valores[0], valores[1], new List<Cadete>(), new List<Pedido>());
        }
        return sucursal;
    }

    public List<Cadete> LeerCadetes()
    {
        List<Cadete> listaCadete = new List<Cadete>();

        using (StreamReader sr = new StreamReader(archivoCadetes))
        {
            string linea;
            sr.ReadLine();
            while ((linea = sr.ReadLine()) != null)
            {
                string[] valores = linea.Split(',');

                Cadete cadete = new Cadete(int.Parse(valores[0]), valores[1], valores[2], valores[3], new List<Pedido>());

                listaCadete.Add(cadete);
            }
        }
        return listaCadete;
    }
}