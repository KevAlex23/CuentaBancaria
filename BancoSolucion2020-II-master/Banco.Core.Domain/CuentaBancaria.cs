using System;
using System.Collections.Generic;

namespace Banco.Core.Domain
{
    public abstract class CuentaBancaria : IServicioFinanciero
    {

        protected CuentaBancaria(string numero, string nombre, string ciudad)
        {
            Numero = numero;
            Nombre = nombre;
            Ciudad = ciudad;
            Saldo = 0;
            _movimientos = new List<CuentaBancariaMovimiento>();
        }

        public string Numero { get; }
        public string Nombre { get; }
        public string Ciudad { get; }
        public decimal Saldo { get; protected set; }

        public abstract string Consignar(decimal valorConsignar, String ciudad, string fecha);
        public abstract string Retirar(decimal valorRetirar, string fecha);

        protected readonly List<CuentaBancariaMovimiento> _movimientos;

    }



    public class CuentaBancariaMovimiento 
    {
        public CuentaBancariaMovimiento(decimal cupoAnterior, decimal cupoActual, decimal saldoAnterior, decimal saldoActual, string tipo, string fecha)
        {
            ValorCreditoAnterior = saldoAnterior;
            ValorDebitoAnterior = cupoAnterior;
            ValorCredito = saldoActual;
            ValorDebito = cupoActual;
            Tipo = tipo;
            Fecha = fecha;
        }

        public CuentaBancariaMovimiento(decimal saldoAnterior, decimal valorCredito, decimal valorDebito, string tipo, string fecha)
        {
            SaldoAnterior = saldoAnterior;
            ValorCredito = valorCredito;
            ValorDebito = valorDebito;
            Tipo = tipo;
            Fecha = fecha;
        }

        public decimal ValorDebitoAnterior { get; private set; }
        public decimal ValorCreditoAnterior { get; private set; }
        public decimal SaldoAnterior { get; private set; }
        public decimal ValorCredito { get; private set; }
        public decimal ValorDebito { get; private set; }
        public string Tipo { get; private set; }
        public string Fecha { get; set; }
    }
}
