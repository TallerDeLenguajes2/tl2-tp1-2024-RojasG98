
using System.Security.Cryptography.X509Certificates;

class Cadete
{
    private int id;
    private string nombre;
    private string direccion;
    private string telefono;
    public Cadete(int id, string nombre, string direccion, string telefono,List<Pedido> pedidos)
    {
        this.id = id;
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
    }

    public int Id { get => id;}
    public string Nombre { get => nombre;}

}