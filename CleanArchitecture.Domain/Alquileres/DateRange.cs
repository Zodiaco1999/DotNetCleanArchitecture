﻿namespace CleanArchitecture.Domain.Alquileres;

public sealed record DateRange
{
    public DateOnly Inicio { get; init; }
    public DateOnly Fin { get; init; }

    private DateRange()
    {
    }

    public int CantidadDias => Fin.DayNumber - Inicio.DayNumber;

    public static DateRange Create(DateOnly inicio, DateOnly fin)
    {
        if (inicio > fin)
            throw new ArgumentException("La fecha de inicio no puede ser mayor que la fecha de fin.");

        return new DateRange
        {
            Inicio = inicio,
            Fin = fin
        };
    }
}