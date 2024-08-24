
class Cadete
{
    private const float pagoPorEnvio = 500;
    private int id;
    private string nombre;
    private string direccion;
    private string telefono;
    private List<Pedido> listadoPedidos;

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
}