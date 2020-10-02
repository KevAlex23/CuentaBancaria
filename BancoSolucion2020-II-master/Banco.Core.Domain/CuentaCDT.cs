using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Banco.Core.Domain
{
    public class CuentaCDT : IServicioFinanciero
    {
        public String Termino { get; set; }
        public string Numero { get; }
        public string Ciudad { get; }
        public decimal Interes { get; set; }
        public decimal Saldo { get; protected set; }

        protected readonly List<CuentaBancariaMovimiento> _movimientos;

        public CuentaCDT(string numero, string ciudad, decimal interes, string termino)
        {
            Termino = termino;
            Interes = interes;
            Numero = numero;
            Ciudad = ciudad;
            Saldo = 0;
            _movimientos = new List<CuentaBancariaMovimiento>();
        }

        public string Consignar(decimal valorConsignar, string ciudad, string fecha)
        {
            if (SegundaConsignacion())
                return "Ya no puedes consignar hasta el termino definido";
            else if (valorConsignar < 1000000)
            {
                return "El valor de consignación inicial debe ser de mínimo 1 millón de pesos.";
            }
            else
            {
                var saldoAnterior = Saldo;
                Saldo += valorConsignar;
                _movimientos.Add(new CuentaBancariaMovimiento(saldoAnterior, valorConsignar, 0, "CONSIGNACION", fecha));
                return $"Apertura valida. Su consignacion fue de ${Saldo:n2} pesos m/c";
            }
        }

        private bool SegundaConsignacion()
        {
            return _movimientos.Any(t => t.Tipo == "CONSIGNACION");
        }

        public string Retirar(decimal valorRetirar, string fecha)
        {
            decimal valorAnterior = Saldo;

            if (Termino == "TRIMESTRAL" && ValidarFecha(_movimientos[0].Fecha,fecha,3))
            {
                Saldo *= 1 + Interes;
                Saldo -= valorRetirar;
                return $"El total retirado al trimestre fue de ${Saldo:n2}";
            }
            else if (Termino == "SEMESTRAL" && ValidarFecha(_movimientos[0].Fecha, fecha, 6))
            {
                Saldo *= 1 + Interes;
                Saldo -= valorRetirar;
                return $"El total retirado al semestre fue de ${Saldo:n2}";
            }
            else if(Termino=="ANUAL" && ValidarFecha(_movimientos[0].Fecha, fecha, 12))
            {
                Saldo *= 1 + Interes;
                Saldo -= valorRetirar;
                return $"El total retirado al anio fue de ${Saldo:n2}";
            }
            else
            {
                return "Aun no puede retirar.";
            }
        }

        private bool ValidarFecha(string fechaCreacion, string fechaRetiro, int meses)
        {
            int mes = int.Parse(fechaCreacion.Substring(0, 2));
            int anio = int.Parse(fechaCreacion.Substring(3, 7));
            for (int i = 0; i < meses; i++)
            {
                if (mes <= 12)
                {
                    mes++;
                } else if (anio < int.Parse(fechaRetiro.Substring(3, 7)))
                {
                    mes = 1;
                }
            }

            if (mes+ anio == int.Parse(fechaRetiro.Substring(0, 2)) + int.Parse(fechaRetiro.Substring(3, 7)))
                return true;
            else
                return false;

        }

    }
}
