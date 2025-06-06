﻿using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Domain.Vehiculos;

public sealed class Vehiculo : Entity
{
    public Modelo? Modelo { get; private set; }
    public Vin? Vin { get; private set; }
    public Direccion? Direccion { get; private set; }
    public Moneda Precio { get; private set; }
    public Moneda Mantenimineto { get; private set; }
    public DateTime? FechaUltimaAlquiler { get; internal set; }
    public List<Accesorio> Accesorios { get; private set; } = [];

    public Vehiculo(
        Guid id,
        Modelo modelo,
        Direccion direccion,
        Vin vin,
        Moneda precio,
        Moneda mantenimiento,
        DateTime? fechaUltimaAlquiler,
        List<Accesorio> accesorios
        ) : base(id)
    {
        Modelo = modelo;
        Direccion = direccion;
        Vin = vin;
        Precio = precio;
        Mantenimineto = mantenimiento;
        FechaUltimaAlquiler = fechaUltimaAlquiler;
        Accesorios = accesorios;
    }
}
