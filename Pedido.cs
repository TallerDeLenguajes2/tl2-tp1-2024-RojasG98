using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

class Pedido
{
    private int nro;
    private string obs;
    private Cliente cliente;
    private Estado estado;

    public Pedido(int nro, string obs,string nombre, string direccion, string referencia, string telefono)
    {
        cliente = new Cliente(nombre,direccion,telefono,referencia);
        this.nro = nro;
        this.obs = obs;
        estado = Estado.Pendiente;
    }

    public int Nro { get => nro;}
    internal Estado Estado { get => estado;}

    public void verDatosCliente(){
        Console.WriteLine("Datos Cliente:");
        Console.WriteLine($"Nombre: {cliente.Nombre}");
        Console.WriteLine($"Telefono: {cliente.Telefono}");
    }
    public void verDireccionCliente(){
        Console.WriteLine("Direccion Cliente:");
        Console.WriteLine($"Direccion: {cliente.Nombre}");
        Console.WriteLine($"Refrencia: {cliente.DatosReferenciaDireccion}");
    }
    public void cambiarEstado(){
        Console.WriteLine("Cual es el estado del pedido: \n1.Entregado\n2.Cancelado");
        char respuesta;
        do
        {
            respuesta = Console.ReadKey().KeyChar;
        } while (respuesta == 1 || respuesta == 2);
        switch (respuesta)
        {
            case '1':
                estado = Estado.Entregado;
                break;
            case '2':
                estado = Estado.Cancelado;
                break;
        }
    }
}

enum Estado {
    Entregado,
    Cancelado,
    Pendiente,

}