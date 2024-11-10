/*
*Presupuestos
○ int IdPresupuesto
○ string nombreDestinatario
○ List<PresupuestoDetalle> detalle
○ Metodos
■ MontoPresupuesto ()
■ MontoPresupuestoConIva()
■ CantidadProductos ()
*/
using System;

namespace EspacioTp5;

public class Presupuestos
{
    public Presupuestos(int idPresupuesto,string nombreDestinatario,List<PresupuestosDetalle> detalles = null)
    {
        IdPresupuesto = idPresupuesto;
        NombreDestinatario = nombreDestinatario;
        Detalle = detalles ?? new List<PresupuestosDetalle>();
    }

    public int IdPresupuesto { get; private set; }
    public string NombreDestinatario { get; private set; }
    public List<PresupuestosDetalle> Detalle { get; private set; }

    public double MontoPresupuesto()
    {
        double monto = 0.0;
        foreach (var item in Detalle)
        {
            monto += (item.Producto.Precio * item.Cantidad);
        }
        return monto;
    }

    public double MontoPresupuestoConIva()
    {
        const double IVA = 0.21;
        return MontoPresupuesto() * (1 + IVA);
    }

    public int CantidadProductos()
    {
        int contador = 0;
        foreach (var item in Detalle)
        {
            contador += item.Cantidad;
        }
        return contador;
    }
}
