using System.Text.Json;
abstract class AccesoADatos
{
    public abstract List<Cadete> LeerCadetes();
    public abstract List<Cadeteria> LeerSucursales();
}

class AccesoCSV:AccesoADatos
{
    const string archivoCadetes = "csv/Cadetes.csv";
    const string archivoSucursales = "csv/Cadeterias.csv";

    public override List<Cadete> LeerCadetes()
    {
        List<Cadete> listadoCadetes = new List<Cadete>();
        List<string> lineas = new List<string>();

        using (StreamReader sr = new StreamReader(archivoCadetes))
        {
            string linea;
            sr.ReadLine();
            while ((linea = sr.ReadLine()) != null)
            {
                string[] valores = linea.Split(',');

                Cadete cadete = new Cadete(int.Parse(valores[0]), valores[1], valores[2], valores[3], new List<Pedido>());

                listadoCadetes.Add(cadete);
            }
        }
        return listadoCadetes;
    }

    public override List<Cadeteria> LeerSucursales()
    {
        List<Cadeteria> sucursales = new List<Cadeteria>();
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
        return sucursales;
    }
}

class AccesoJSON:AccesoADatos
{
    const string archivoCadetes = "csv/Cadetes.csv";
    const string archivoSucursales = "csv/Cadeterias.csv";
    public override List<Cadete> LeerCadetes()
    {
        if (!Existe(archivoCadetes))
        {
            throw new FileNotFoundException($"ERROR: {archivoCadetes} no existe o está vacío.");
        }

        string jsonString = File.ReadAllText(archivoCadetes);
        return JsonSerializer.Deserialize<List<Cadete>>(jsonString);
    }
    public override List<Cadeteria> LeerSucursales()
    {
        if (!Existe(archivoSucursales))
        {
            throw new FileNotFoundException($"ERROR: {archivoSucursales} no existe o está vacío.");
        }

        string jsonString = File.ReadAllText(archivoSucursales);
        return JsonSerializer.Deserialize<List<Cadeteria>>(jsonString);
    }

    public bool Existe(string nombreArchivo){
        return File.Exists(nombreArchivo) && new FileInfo(nombreArchivo).Length > 0;
    }
}