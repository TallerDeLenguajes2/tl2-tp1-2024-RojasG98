
using System.Security.Cryptography.X509Certificates;

class Cadete
{
    private const float pagoPorEnvio = 500;
    private int id;
    private string nombre;
    private string direccion;
    private string telefono;
    private List<Pedido> listadoPedidos;

    public Cadete(int id, string nombre, string direccion, string telefono,List<Pedido> pedidos)
    {
        this.id = id;
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
        this.listadoPedidos = pedidos;
    }

    public int Id { get => Id;}
    public string Nombre { get => nombre;}
    internal List<Pedido> ListadoPedidos { get => listadoPedidos; }

    public int contarPedidosRealizados(){
        int entregado = 0;
        foreach (var pedidos in listadoPedidos)
        {
            if(pedidos.Estado == Estado.Entregado){
                entregado++;
            }
        }
        return entregado;
    }
    public float jornalACobrar(){
        return pagoPorEnvio * contarPedidosRealizados();
    }
    
    public void mostrarPedidosCadete(){
        foreach (var pedidos in listadoPedidos)
        {
            Console.WriteLine("Id: ",pedidos.Nro);
            pedidos.verDatosCliente();
        }
    }
    public void borrarPedido(Pedido pedidoABorrar){
        listadoPedidos.Remove(pedidoABorrar);
    }

    public void agregarPedido(Pedido pedidoAAgregar){
        listadoPedidos.Add(pedidoAAgregar);
    }
}