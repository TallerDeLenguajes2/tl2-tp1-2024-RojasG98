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

}

enum Estado {
    Entregado,
    Cancelado,
    Pendiente,
    Robado,

}