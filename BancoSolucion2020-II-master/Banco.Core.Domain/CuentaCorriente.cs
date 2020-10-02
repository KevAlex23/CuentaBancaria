using System;
using System.Collections.Generic;
using System.Text;

namespace Banco.Core.Domain
{
    public class CuentaCorriente : CuentaBancaria
    {
        public decimal Cupo  { get; set; }
        public CuentaCorriente(decimal cupo, string numero, string nombre, string ciudad) : base(numero, nombre, ciudad)
        {
            Cupo = cupo;
        }

        public override string Consignar(decimal valorConsignar, string ciudadEnvio, string fecha)
        {
            if (valorConsignar <= 0)
                return "El valor a consignar es incorrecto";

            if (valorConsignar < 100000)
                return "El valor mínimo de la primera consignación debe ser de $100.000 mil pesos. Su nuevo saldo es $0 pesos";
            var saldoAnterior = Saldo;
            Saldo += valorConsignar+Cupo;

            _movimientos.Add(new CuentaBancariaMovimiento(saldoAnterior, valorConsignar, 0, "CONSIGNACION", fecha));


            return $"Su Nuevo Saldo es de ${Saldo:n2} pesos m/c";
        }

        public override string Retirar(decimal valorRetirar, string fecha)
        {
            if (valorRetirar > Saldo || Saldo - (valorRetirar +((valorRetirar/1000)*4)) < Cupo)
                return $"Saldo insuficiente. Su saldo es de ${Saldo:n2} pesos m/c";
            else
                Saldo -= (valorRetirar + ((valorRetirar / 1000) * 4));
            return $"Saldo retirado. Su Nuevo Saldo es de ${Saldo:n2} pesos";
        }
    }
}
