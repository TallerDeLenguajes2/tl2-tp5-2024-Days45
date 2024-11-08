/*Productos
○ int idProducto
○ string descripcion
○ int precio*/
using System;
namespace EspacioTp5;
public class Productos
{
    public Productos(int idProducto, string descripcion, int precio)
    {
        IdProducto = idProducto;
        Descripcion = descripcion;
        Precio = precio;
    }

    public int IdProducto {get;private set;}
    public string Descripcion{get;private set;}
    public int Precio {get;private set;}

}
