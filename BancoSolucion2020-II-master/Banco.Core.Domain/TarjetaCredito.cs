using System.Collections.Generic;

namespace Banco.Core.Domain
{
    public class TarjetaCredito : IServicioFinanciero
    {

        public TarjetaCredito(string numero, string nombre, string ciudad, decimal cupo)
        {
            Nombre = nombre;
            Numero = numero;
            Ciudad = ciudad;
            Saldo = 0;
            Cupo = cupo;
            _movimientos = new List<CuentaBancariaMovimiento>();
        }

        public string Nombre { get; }
        public string Numero { get; }
        public string Ciudad { get; }
        public decimal Saldo { get; private set;}
        public decimal Cupo { get; private set; }
        
        
        public string Consignar(decimal valorConsignacion, string ciudadRemitente, string fecha)
        {
            if (valorConsignacion<=0)
                return "Valor del abono no valido";

            if (valorConsignacion > Saldo)
                return "el valor del abono debe ser igual o menor al de la deuda actual";

            var CupoAnterior = Cupo;
            var SaldoAnterior = Saldo;
            
            Cupo += valorConsignacion;
            Saldo -= valorConsignacion;
            
            _movimientos.Add(new CuentaBancariaMovimiento(CupoAnterior, Cupo, SaldoAnterior, Saldo, "ABONO", fecha));
            return $"Abono valido. Su Nuevo Cupo es de ${Cupo:n2} pesos m/c";
        }

        public string Retirar(decimal valorRetirar, string fecha)
        {
            if (valorRetirar<=0)
                return "avance no valido, verifique el valor a retirar";
            
            if (valorRetirar > Cupo)
                return "avance no valido, el valor a retirar es mayor al cupo disponible en su tarjeta";
            
            decimal CupoAnterior = Cupo;
            decimal SaldoAnterior = Saldo;
            
            Cupo -= valorRetirar;
            Saldo += valorRetirar;
            
            _movimientos.Add(new CuentaBancariaMovimiento(CupoAnterior, Cupo, SaldoAnterior, Saldo, "ABONO", fecha));
            return $"Avance valido. Su Nuevo Cupo es de ${Cupo:n2} pesos m/c";
        }
        
        private readonly List<CuentaBancariaMovimiento> _movimientos;
    }
}
