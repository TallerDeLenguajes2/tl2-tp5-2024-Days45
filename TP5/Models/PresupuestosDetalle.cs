/*
PresupuestosDetalle
○ Productos producto
○ int cantidad
*/
using System;
namespace EspacioTp5;
public class PresupuestosDetalle
{
    public PresupuestosDetalle(Productos producto, int cantidad)
    {
        Producto = producto;
        Cantidad = cantidad;
    }

    public Productos Producto {get;private set;}
    public int Cantidad{get;private set;}
}
