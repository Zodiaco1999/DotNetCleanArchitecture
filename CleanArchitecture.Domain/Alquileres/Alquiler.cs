﻿using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Alquileres.Events;
using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Domain.Alquileres;

public sealed class Alquiler : Entity
{
    public Guid VehiculoId { get; private set; }
    public Guid UserId { get; private set; }
    public Moneda? PrecioPorPeriodo { get; private set; }
    public Moneda? Mantenimiento { get; private set; }
    public Moneda? Accesorios { get; private set; }
    public Moneda? PrecioTotal { get; private set; }
    public AlquilerStatus Status { get; private set; }
    public DateRange? Duracion { get; private set; }
    public DateTime? FechaCreacion { get; private set; }
    public DateTime? FechaConfirmacion { get; private set; }
    public DateTime? FechaDenegacion{ get; private set; }
    public DateTime? FechaCompletado{ get; private set; }
    public DateTime? FechaCancelacion{ get; private set; }

    private Alquiler(
        Guid id,
        Guid vehiculoId,
        Guid userId,
        DateRange duracion,
        Moneda precioPorPerido,
        Moneda mantenimiento,
        Moneda accesorios,
        Moneda precioTotal,
        AlquilerStatus status,
        DateTime? fechaCreacion
        ) : base(id)
    {
        VehiculoId = vehiculoId;
        UserId = userId;
        Duracion = duracion;
        PrecioPorPeriodo = precioPorPerido;
        Mantenimiento = mantenimiento;
        Accesorios = accesorios;
        PrecioTotal = precioTotal;
        Status = status;
        FechaCreacion = fechaCreacion;
    }

    public static Alquiler Reservar(
        Vehiculo vehiculo,
        Guid userId,
        DateRange duracion,
        DateTime? fechaCreacion,
        PrecioService precioService
    )
    {
        var precioDetalle = precioService.CalcularPrecio(vehiculo, duracion);

        var alquiler = new Alquiler(
            Guid.NewGuid(),
            vehiculo.Id,
            userId,
            duracion,
            precioDetalle.PrecioPorPeriodo,
            precioDetalle.Mantenimiento,
            precioDetalle.Accesorios,
            precioDetalle.PrecioTotal,
            AlquilerStatus.Reservado,
            fechaCreacion
        );

        alquiler.RaiseDomainEvent(new AlquilerReservadoDomainEvent(alquiler.Id));

        vehiculo.FechaUltimaAlquiler = fechaCreacion;

        return alquiler;
    }

    public Result Confirmar(DateTime utcNow)
    {
        if (Status != AlquilerStatus.Reservado)
        {
            return Result.Failure("El alquiler no está reservado.");
        }


    }
}
