using System.ComponentModel;
using System.IO;

Sistema program = new Sistema();
int numero;
Cadeteria sucursal = program.cargarSucursal();
do
{
    numero = program.menuPrincipal();
    if (numero != 7)
    {
        program.hacerTarea(numero,sucursal);
    }
} while (numero != 7);





