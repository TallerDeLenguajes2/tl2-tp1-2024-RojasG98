using System.ComponentModel;
using System.IO;

Sistema program = new Sistema();
int numero;
bool seguir = true;
Cadeteria sucursal = program.cargarSucursal();
do
{
    numero = program.menuPrincipal();
    if (numero > 0 && numero < 7)
    {
        program.hacerTarea(numero,sucursal);
    }
} while (seguir);





