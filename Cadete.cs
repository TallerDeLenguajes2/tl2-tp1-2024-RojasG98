class Cadete
{
    private int Id;
    private string nombre;
    private string direccion;
    private string telefono;
    private List<Pedido> listadoPedidos;
    public void agregarPedido(Pedido nuevoPedido){
        listadoPedidos.Add(nuevoPedido);
    }
}