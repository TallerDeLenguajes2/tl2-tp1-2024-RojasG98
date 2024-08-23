class Pedido
{
    private int nro;
    private string obs;
    private string cliente;
    private Estado estado;

}

enum Estado {
    Entregado,
    Cancelado,
    Pendiente,
    Robado,

}