using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Banco.Core.Domain
{
    public class CuentaAhorro : CuentaBancaria
    {
        public CuentaAhorro(string numero, string nombre, string ciudad) : base(numero, nombre, ciudad)
        {
        }

        public override string Consignar(decimal valorConsignar, string ciudadEnvio, string fecha)
        {
            String resultado = "";

            if (valorConsignar <= 0)
                return "El valor a consignar es incorrecto.";

            var saldoAnterior = Saldo;

            if (valorConsignar < 50000 && NoTieneConsignacion())
            {
                resultado = "El valor mínimo de la primera consignación debe ser de $50.000 mil pesos. Su nuevo saldo es $0 pesos";

            }
            else if (ConsignacionNacional(ciudadEnvio))
            {
                Saldo += valorConsignar;
                resultado = $"Su Nuevo Saldo es de ${Saldo:n2} pesos m/c";
            }
            else
            {
                if (valorConsignar <= 10000)
                {
                    resultado = "Las consignaciones a otra ciudad deben ser mayores a 10.000 por su costo adicional";
                }
                else
                {
                    Saldo += valorConsignar-10000;
                    resultado = $"Su Nuevo Saldo es de ${Saldo:n2} pesos m/c";
                }
                    

                
            }

            _movimientos.Add(new CuentaBancariaMovimiento(saldoAnterior, valorConsignar, 0, "CONSIGNACION", fecha));

            return resultado;
        }

        public override string Retirar(decimal valorRetirar, string fecha)
        {
            if (Saldo < valorRetirar + 5000)
                return $"No hay saldo suficiente para retirar, su Saldo es de ${ Saldo: n2} pesos";

            var saldoAnterior = Saldo;
            String resultado;

            if (Saldo < 20000)
            {
                resultado = "el saldo de la cuenta debe ser mayor de $20,000.00";
            }
            else if (RetiroCosto())
            {
                Saldo -= valorRetirar;
                resultado = $"Su nuevo Saldo es de ${Saldo:n2} pesos.";
            }
            else
            {
                Saldo -= valorRetirar+5000;
                resultado = $"Su nuevo Saldo es de ${Saldo:n2} pesos.";
                _movimientos.Add(new CuentaBancariaMovimiento(saldoAnterior, valorRetirar, 0, "COMISION", fecha));
            }

            _movimientos.Add(new CuentaBancariaMovimiento(saldoAnterior, valorRetirar, 0, "RETIRO", fecha));

            return resultado;//_movimientos[0].Tipo+"--"+_movimientos[1].Tipo+"--";
        }

        private bool RetiroCosto()
        {
            int contador = 0;

            contador =_movimientos.FindAll((t) => t.Tipo == "RETIRO").Count(u=> u.Fecha==DateTime.Now.Month+"-"+DateTime.Now.Year);

            bool resultado = contador > 2 ? false : true;
            return resultado;
        }

        private bool ConsignacionNacional(String ciudad)
        {
            bool respuesta = false;
            respuesta = Ciudad == ciudad ? true : false;
            return respuesta;
        }

        private bool NoTieneConsignacion()
        {
            return !_movimientos.Any(t => t.Tipo == "CONSIGNACION");
        }
    }
}
