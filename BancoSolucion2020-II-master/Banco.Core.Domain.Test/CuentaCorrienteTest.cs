using NUnit.Framework;
using System;

namespace Banco.Core.Domain.Test
{
    public class TestsCorriente
    {
        [SetUp]
        public void Setup()
        {
        }

        //Escenario: Valor de consignaci�n negativo o cero 
        //H3: Como Usuario quiero realizar consignaciones a una cuenta corriente para salvaguardar el dinero
        //Criterio de Aceptaci�n:
        //3.0
        //Ejemplo
        //Dado El cliente tiene una cuenta de ahorro                                       //A =>Arrange /Preparaci�n
        //N�mero 10001, Nombre �Cuenta ejemplo�, Saldo de 0 , ciudad Valledupar, cupo de 500                            
        //Cuando Va a consignar un valor menor o igual a cero  (0)                            //A =>Act = Acci�n
        //Entonces El sistema presentar� el mensaje. "El valor a consignar es incorrecto"  //A => Assert => Validaci�n
        [Test]
        public void NoPuedeConsignacionValorNegativoTest()
        {
            //Preparar
            var cuentaCorriente = new CuentaCorriente(cupo: 500, numero: "10001", nombre:"Cuenta Ejemplo", ciudad:"Valledupar");
            //Acci�n
            var resultado=cuentaCorriente.Consignar(0,"", DateTime.Now.Month + "-" + DateTime.Now.Year);
            //Verificaci�n
            Assert.AreEqual("El valor a consignar es incorrecto", resultado);
        }


        //Escenario: Valor de consignaci�n valido
        //H3: Como Usuario quiero realizar consignaciones a una cuenta corriente para salvaguardar el dinero
        //Criterio de Aceptaci�n:
        //3.2 El valor consignado debe ser adicionado al saldo de la cuenta.
        //Ejemplo
        //Dado El cliente tiene una cuenta de ahorro                                       //A =>Arrange /Preparaci�n
        //N�mero 10001, Nombre �Cuenta Ejemplo�, cupo de 500 , ciudad Valledupar                               
        //Cuando Va a consignar un valor menor o igual a cero  (0)                            //A =>Act = Acci�n
        //Entonces El sistema presentar� el mensaje. "Su Nuevo Saldo es de $600,000.00 pesos m/c"  //A => Assert => Validaci�n
        [Test]
        public void ConsignacionInicialCorrectaTest()
        {
            //Preparar
            var cuentaCorriente = new CuentaCorriente(cupo: 500, numero: "10001", nombre: "Cuenta Ejemplo", ciudad: "Valledupar");
            //Acci�n
            var resultado = cuentaCorriente.Consignar(100000, "", DateTime.Now.Month + "-" + DateTime.Now.Year);
            //Verificaci�n
            Assert.AreEqual("Su Nuevo Saldo es de $100,500.00 pesos m/c", resultado);
        }

        //Escenario: Valor de consignaci�n negativo o cero 
        //H3: Como Usuario quiero realizar consignaciones a una cuenta corriente para salvaguardar el dinero
        //Criterio de Aceptaci�n:
        //3.1 La consignaci�n inicial debe ser de m�nimo 100 mil pesos. 
        //Ejemplo
        //Dado El cliente tiene una cuenta de ahorro                                       //A =>Arrange /Preparaci�n
        //N�mero 10001, Nombre �Cuenta ejemplo�, Saldo de 0 , ciudad Valledupar, cupo de 500                            
        //Cuando Va a consignar un valor menor o igual a cero  (0)                            //A =>Act = Acci�n
        //Entonces El sistema presentar� el mensaje. "El valor a consignar es incorrecto"  //A => Assert => Validaci�n
        [Test]
        public void NoPuedeConsignarMenosDe100MilTest()
        {
            //Preparar
            var cuentaCorriente = new CuentaCorriente(cupo: 500, numero: "10001", nombre: "Cuenta Ejemplo", ciudad: "Valledupar");
            //Acci�n
            var resultado = cuentaCorriente.Consignar(99999,"", DateTime.Now.Month + "-" + DateTime.Now.Year);
            //Verificaci�n
            Assert.AreEqual("El valor m�nimo de la primera consignaci�n debe ser de $100.000 mil pesos. Su nuevo saldo es $0 pesos", resultado);
        }
        ///////////////////////////////////////
        //Pruebas Retiros
        //////////////////////////////////////

        [Test]
        public void NoPuedeRetitrarMenosDeLoQueHayTest()
        {
            //Preparar
            var cuentaCorriente = new CuentaCorriente(cupo: 500, numero: "10001", nombre: "Cuenta Ejemplo", ciudad: "Valledupar");
            //Acci�n
            cuentaCorriente.Consignar(200000, "", DateTime.Now.Month + "-" + DateTime.Now.Year);
            var retirar = cuentaCorriente.Retirar(300000, DateTime.Now.Month + "-" + DateTime.Now.Year);
            //Verificaci�n
            Assert.AreEqual("Saldo insuficiente. Su saldo es de $200,500.00 pesos m/c", retirar);
        }

        [Test]
        public void SaldoMinimoMayorCupoTest()
        {
            //Preparar
            var cuentaCorriente = new CuentaCorriente(cupo: 500, numero: "10001", nombre: "Cuenta Ejemplo", ciudad: "Valledupar");
            //Acci�n
            cuentaCorriente.Consignar(500000, "", DateTime.Now.Month + "-" + DateTime.Now.Year);
            var retirar = cuentaCorriente.Retirar(500100, DateTime.Now.Month + "-" + DateTime.Now.Year);
            //Verificaci�n
            Assert.AreEqual("Saldo insuficiente. Su saldo es de $500,500.00 pesos m/c", retirar);
        }
       
        [Test]
        public void ValorRetirarDescontarDelSaldoTest()
        {
            //Preparar
            var cuentaCorriente = new CuentaCorriente(cupo: 500, numero: "10001", nombre: "Cuenta Ejemplo", ciudad: "Valledupar");
            //Acci�n
            cuentaCorriente.Consignar(500000,"", DateTime.Now.Month + "-" + DateTime.Now.Year);
            var retirar = cuentaCorriente.Retirar(300000, DateTime.Now.Month + "-" + DateTime.Now.Year);
            //Verificaci�n
            Assert.AreEqual("Saldo retirado. Su Nuevo Saldo es de $199,300.00 pesos", retirar);
        }



    }
}