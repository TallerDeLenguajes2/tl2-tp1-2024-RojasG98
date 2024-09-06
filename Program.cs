using System.ComponentModel;
using System.IO;

Sistema program = new Sistema();
int numero;
program.LeerSucursales();
Cadeteria sucursal = program.elegirSucursal();
do
{
    numero = program.menuPrincipal();
    if (numero != 6)
    {
        program.hacerTarea(numero,sucursal);
    }
} while (numero != 6);





