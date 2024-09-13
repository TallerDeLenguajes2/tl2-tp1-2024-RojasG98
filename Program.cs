using System.IO;
Sistema program = new Sistema();
int numero;
bool seguir = true;
Cadeteria sucursal = program.cargarSucursal();
do
{
    numero = program.menuPrincipal();
    if (numero > 0 && numero <6)
    {
        seguir = program.hacerTarea(numero,sucursal);
    }
} while (seguir);



